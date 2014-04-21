using Android.Content;
using Android.Database.Sqlite;
using Android.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.origon.wikinotes.db
{
    /// <summary>
    /// This DatabaseHelper class defines methods to create and upgrade the database from previous versions.
    /// </summary>
    internal class DatabaseHelper : SQLiteOpenHelper
    {
        private static string DATABASE_NAME = "origon.wikinotes.db";
        private static int DATABASE_VERSION = 2;

        private Context _context;

        public DatabaseHelper(Context paramContext)
            : base(paramContext, DATABASE_NAME, null, DATABASE_VERSION)
        {
            this._context = paramContext;
        }

        public override void OnCreate(SQLiteDatabase paramSQLiteDatabase)
        {
            // Create table "wikinotes"
            paramSQLiteDatabase.ExecSQL("CREATE TABLE " + WikiNote.Notes.TABLE_WIKINOTES + " (" + WikiNote.Notes.Id + " INTEGER PRIMARY KEY,"
                + WikiNote.Notes.TITLE + " TEXT COLLATE LOCALIZED NOT NULL,"
                + WikiNote.Notes.BODY + " TEXT,"
                + WikiNote.Notes.CREATED_DATE + " INTEGER,"
                + WikiNote.Notes.MODIFIED_DATE + " INTEGER" + ");");

            // Initialize database
            ContentValues localContentValues = new ContentValues();
            localContentValues.Put(WikiNote.Notes.TITLE, this._context.Resources.GetString(Resource.String.start_page));
            localContentValues.Put(WikiNote.Notes.BODY, this._context.Resources.GetString(Resource.String.top_note));
            long localLong = DateTime.Now.ToFileTimeUtc();
            localContentValues.Put(WikiNote.Notes.CREATED_DATE, localLong);
            localContentValues.Put(WikiNote.Notes.MODIFIED_DATE, localLong);

            paramSQLiteDatabase.Insert(WikiNote.Notes.TABLE_WIKINOTES, "huh?", localContentValues);
            
            localContentValues.Put(WikiNote.Notes.TITLE, "PageFormatting");
            localContentValues.Put(WikiNote.Notes.BODY, this._context.Resources.GetString(Resource.String.page_formatting_note));
            localContentValues.Put(WikiNote.Notes.CREATED_DATE, localLong);
            localContentValues.Put(WikiNote.Notes.MODIFIED_DATE, localLong);

            paramSQLiteDatabase.Insert(WikiNote.Notes.TABLE_WIKINOTES, "huh?", localContentValues);
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            Log.Warn("StyledWikiNotes", "Upgrading database from version " + oldVersion + " to " + newVersion + ", which will destroy all old data");
            db.ExecSQL("DROP TABLE IF EXISTS " + WikiNote.Notes.TABLE_WIKINOTES);
            OnCreate(db);
        }
    }
}