using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;
using AndroidUri = Android.Net.Uri;
using Java.Lang;
using Android.Database;
using Android.Text;

namespace com.origon.wikinotes.db
{
    /// <summary>
    /// The Wikinotes Content Provider is the guts of the wikinotes application. It
    /// handles Content URLs to list all wiki notes, retrieve a note by name, or
    /// return a list of matching notes based on text in the body or the title of
    /// the note. It also handles all set up of the database, and reports back MIME
    /// types for the individual notes or lists of notes to help Android route the
    /// data to activities that can display that data (in this case either the
    /// WikiNotes activity itself, or the WikiNotesList activity to list multiple
    /// notes).
    /// </summary>
    [ContentProvider(new string[] { "com.origon.wikinotes.db.WikiNotesProvider" })]
    public class WikiNotesProvider : ContentProvider
    {
        private static int LIVE_FOLDER = 4;
        private static int LIVE_FOLDER_ID = 5;
        private static Dictionary<string, string> LIVE_FOLDER_PROJECTION_MAP;

        private const int NOTES = 1;
        private static Dictionary<string, string> NOTES_LIST_PROJECTION_MAP;
        private const int NOTE_NAME = 2;
        private const int NOTE_SEARCH = 3;
        private static int SUGGESTION = 6;
        private static Dictionary<string, string> SUGGESTION_PROJECTION_MAP;

        private static UriMatcher URI_MATCHER = new UriMatcher(-1) { };
        private DatabaseHelper _dbHelper;

        static WikiNotesProvider()
        {
            URI_MATCHER.AddURI(WikiNote.WIKINOTES_AUTHORITY, WikiNote.WIKINOTES_BASE_PATH, NOTES);
            URI_MATCHER.AddURI(WikiNote.WIKINOTES_AUTHORITY, WikiNote.WIKINOTES_BASE_PATH + "/*", NOTE_NAME);
            URI_MATCHER.AddURI(WikiNote.WIKINOTES_AUTHORITY, "wiki/search/*", NOTE_SEARCH);

            NOTES_LIST_PROJECTION_MAP = new Dictionary<string, string>() 
            { 
                { WikiNote.Notes.Id, WikiNote.Notes.Id },
                { WikiNote.Notes.TITLE, WikiNote.Notes.TITLE},
                { WikiNote.Notes.BODY, WikiNote.Notes.BODY},
                { WikiNote.Notes.CREATED_DATE, WikiNote.Notes.CREATED_DATE},
                { WikiNote.Notes.MODIFIED_DATE, WikiNote.Notes.MODIFIED_DATE} 
            };

            LIVE_FOLDER_PROJECTION_MAP = new Dictionary<string, string>() 
            {
                {"_id", "_id AS _id"},
                {"name", "title AS name"}
            };

            SUGGESTION_PROJECTION_MAP = new Dictionary<string, string>()
            {
                {"_id", "_id AS _id"},
                {"suggest_text_1", "title AS suggest_text_1"},
                {"suggest_intent_data_id", "title AS suggest_intent_data_id"}
            };
        }

        public WikiNotesProvider()
            : base()
        {

        }

        public override bool OnCreate()
        {
            this._dbHelper = new DatabaseHelper(Context);
            return true;
        }

        /// <summary>
        /// Figure out which query to run based on the URL requested and any other
        /// parameters provided.
        /// </summary>
        /// <param name="paramUri"></param>
        /// <param name="?"></param>
        /// <param name="projection"></param>
        /// <param name="selection"></param>
        /// <param name="selectionArgs"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public override ICursor Query(AndroidUri paramUri, string[] projection, string selection,	string[] selectionArgs, string sort)
        {
            // Query the database using the arguments provided
            SQLiteQueryBuilder qb = new SQLiteQueryBuilder();
            string[] whereArgs = null;

            // What type of query are we going to use - the URL_MATCHER
            // defined at the bottom of this class is used to pattern-match
            // the URL and select the right query from the switch statement
            switch (URI_MATCHER.Match(paramUri))
            {
                case NOTES:
                    // this match lists all notes
                    qb.Tables = WikiNote.Notes.TABLE_WIKINOTES;
                    qb.SetProjectionMap(NOTES_LIST_PROJECTION_MAP);
                    break;

                case NOTE_SEARCH:
                    // this match searches for a text match in the body of notes
                    qb.Tables = WikiNote.Notes.TABLE_WIKINOTES;
                    qb.SetProjectionMap(NOTES_LIST_PROJECTION_MAP);
                    qb.AppendWhere("body like ? or title like ?");
                    whereArgs = new string[2];
                    whereArgs[0] = whereArgs[1] = "%" + paramUri.LastPathSegment + "%";
                    break;

                case NOTE_NAME:
                    // this match searches for an exact match for a specific note name
                    qb.Tables = WikiNote.Notes.TABLE_WIKINOTES;
                    qb.AppendWhere("title=?");
                    whereArgs = new string[] { paramUri.LastPathSegment };
                    break;

                default:
                    // anything else is considered and illegal request
                    throw new IllegalArgumentException("Unknown URL " + paramUri);
            }

            // If no sort order is specified use the default
            string orderBy;
            if (TextUtils.IsEmpty(sort))
            {
                orderBy = WikiNote.Notes.MOST_RECENTLY_MODIFIED_SORT_ORDER;
            }
            else
            {
                orderBy = sort;
            }

            // Run the query and return the results as a Cursor
            SQLiteDatabase mDb = _dbHelper.ReadableDatabase;
            ICursor c = qb.Query(mDb, projection, null, whereArgs, null, null, orderBy);
            c.SetNotificationUri(Context.ContentResolver, paramUri);
            return c;
        }

        public override string GetType(AndroidUri uri) 
        {
        	switch (URI_MATCHER.Match(uri)) 
            {
	            case NOTES:
	            case NOTE_SEARCH:
	                // for a notes list, or search results, return a mimetype
	                // indicating
	                // a directory of wikinotes
	                return WikiNote.Notes.NOTES_MIME_TYPE;
            	case NOTE_NAME:
	                // for a specific note name, return a mimetype indicating a single
	                // item representing a wikinote
                    return WikiNote.Notes.NOTE_MIME_TYPE;
            	default:
	                // any other kind of URL is illegal
	                throw new IllegalArgumentException("Unknown URL " + uri);
	        }
        }

        public override AndroidUri Insert(AndroidUri uri, ContentValues initialValues)
        {
            long rowID;
            ContentValues values;
            if (initialValues != null)
            {
                values = new ContentValues(initialValues);
            }
            else
            {
                values = new ContentValues();
            }

            // We can only insert a note, no other URLs make sense here
            if (URI_MATCHER.Match(uri) != NOTE_NAME)
            {
                throw new IllegalArgumentException("Unknown URL " + uri);
            }

            // Update the modified time of the record
            long now = DateTime.Now.ToFileTimeUtc();

            // Make sure that the fields are all set
            if (values.ContainsKey(WikiNote.Notes.CREATED_DATE) == false)
            {
                values.Put(WikiNote.Notes.CREATED_DATE, now);
            }

            if (values.ContainsKey(WikiNote.Notes.MODIFIED_DATE) == false)
            {
                values.Put(WikiNote.Notes.MODIFIED_DATE, now);
            }

            if (values.ContainsKey(WikiNote.Notes.TITLE) == false)
            {
                values.Put(WikiNote.Notes.TITLE, uri.LastPathSegment);
            }

            if (values.ContainsKey(WikiNote.Notes.BODY) == false)
            {
                values.Put(WikiNote.Notes.BODY, "");
            }

            SQLiteDatabase db = _dbHelper.WritableDatabase;
            rowID = db.Insert(WikiNote.Notes.TABLE_WIKINOTES, "body", values);
            if (rowID > 0)
            {
                AndroidUri newUri = AndroidUri.WithAppendedPath(WikiNote.Notes.ALL_NOTES_URI, uri.LastPathSegment);
                Context.ContentResolver.NotifyChange(newUri, null);
                return newUri;
            }

            throw new SQLException("Failed to insert row into " + uri);
        }

        public override int Delete(AndroidUri uri, string where, string[] whereArgs)
        {
            /* This is kind of dangerous: it deletes all records with a single
               Intent. It might make sense to make this ContentProvider
               non-public, since this is kind of over-powerful and the provider
               isn't generally intended to be public.  But for now it's still
               public. */
            if (WikiNote.Notes.ALL_NOTES_URI.Equals(uri))
            {
                return _dbHelper.WritableDatabase.Delete(WikiNote.Notes.TABLE_WIKINOTES, null, null);
            }

            int count;
            SQLiteDatabase db = _dbHelper.WritableDatabase;
            switch (URI_MATCHER.Match(uri))
            {
                case NOTES:
                    count = db.Delete(WikiNote.Notes.TABLE_WIKINOTES, where, whereArgs);
                    break;

                case NOTE_NAME:
                    if (!TextUtils.IsEmpty(where) || (whereArgs != null))
                    {
                        throw new UnsupportedOperationException("Cannot update note using where clause");
                    }
                    string noteId = uri.PathSegments[1];
                    count = db.Delete(WikiNote.Notes.TABLE_WIKINOTES, "_id=?", new string[] { noteId });
                    break;

                default: 
                    throw new IllegalArgumentException("Unknown URL " + uri);
            }

            Context.ContentResolver.NotifyChange(uri, null);
            return count;
        }


        /// <summary>
        /// The update method, which will allow either a mass update using a where
        /// clause, or an update targeted to a specific note name (the latter will
        /// be more common). Other matched URLs will be rejected as invalid. For
        /// updating notes by title we use ReST style arguments from the URI and do
        /// not support using the where clause or args.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="values"></param>
        /// <param name="where"></param>
        /// <param name="whereArgs"></param>
        /// <returns></returns>
        public override int Update(AndroidUri uri, ContentValues values, string where, string[] whereArgs) 
        {
	        int count;
	        SQLiteDatabase db = _dbHelper.WritableDatabase;
	
            switch (URI_MATCHER.Match(uri)) 
            {
	            case NOTES:
	                count = db.Update(WikiNote.Notes.TABLE_WIKINOTES, values, where, whereArgs);
	                break;
        	    case NOTE_NAME:
	                values.Put(WikiNote.Notes.MODIFIED_DATE, DateTime.Now.ToFileTimeUtc());
                    count = db.Update(WikiNote.Notes.TABLE_WIKINOTES, values, where, whereArgs);
	                break;
        	    default:
	                throw new IllegalArgumentException("Unknown URL " + uri);
	        }

	        Context.ContentResolver.NotifyChange(uri, null);
	        return count;
        }


    }
}