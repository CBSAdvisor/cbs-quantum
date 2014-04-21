using Android.App;
using Android.Content;
using Android.Database;
using com.origon.wikinotes.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        /**
         * Create an intent to start the WikiNoteEditor using the current title
         * and body information (if any).
         */
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
    }
}
