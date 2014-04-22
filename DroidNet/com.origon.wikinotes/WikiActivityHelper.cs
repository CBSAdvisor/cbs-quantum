using Android.App;
using Android.Content;
using Android.Database;
using com.origon.wikinotes.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uri = Android.Net.Uri;

namespace com.origon.wikinotes
{
    public class WikiActivityHelper
    {
        public static int ACTIVITY_EDIT = 1;
        public static int ACTIVITY_DELETE = 2;
        public static int ACTIVITY_LIST = 3;
        public static int ACTIVITY_SEARCH = 4;

        private Activity _context;

        public WikiActivityHelper(Activity context)
        {
            _context = context;
        }


        /// <summary>
        /// If a list of notes is requested, fire the WikiNotes list Content URI
        /// and let the WikiNotesList activity handle it.
        /// </summary>
        public void ListNotes()
        {
            Intent i = new Intent(Intent.ActionView, WikiNote.Notes.ALL_NOTES_URI);
            _context.StartActivity(i);
        }

        /// <summary>
        /// Create an intent to start the WikiNoteEditor using the current title
        /// and body information (if any).
        /// </summary>
        public void EditNote(string mNoteName, ICursor cursor)
        {
            // This intent could use the android.intent.action.EDIT for a wiki note
            // to invoke, but instead I wanted to demonstrate the mechanism for invoking
            // an intent on a known class within the same application directly. Note
            // also that the title and body of the note to edit are passed in using the extras bundle.
            Intent i = new Intent(_context, typeof(WikiNoteEditor));
            i.PutExtra(WikiNote.Notes.TITLE, mNoteName);
            String body;
            if (cursor != null)
            {
                body = cursor.GetString(cursor.GetColumnIndexOrThrow(WikiNote.Notes.BODY));
            }
            else
            {
                body = string.Empty;
            }
            i.PutExtra(WikiNote.Notes.BODY, body);
            _context.StartActivityForResult(i, ACTIVITY_EDIT);
        }

        public void DeleteNote(ICursor cursor)
        {
            new AlertDialog.Builder(_context)
                .SetTitle(_context.Resources.GetString(Resource.String.delete_title))
                .SetMessage(Resource.String.delete_message)
                .SetPositiveButton(Resource.String.yes_button, (object sender, DialogClickEventArgs e) =>
                {
                    Uri noteUri = ContentUris.WithAppendedId(WikiNote.Notes.ALL_NOTES_URI,
                        cursor.GetInt(0));
                    _context.ContentResolver.Delete(noteUri, null, null);
                    _context.SetResult(Result.Ok);
                    _context.Finish();
                })
                .SetNegativeButton(Resource.String.no_button, delegate { }).Show();
        }
    }
}
