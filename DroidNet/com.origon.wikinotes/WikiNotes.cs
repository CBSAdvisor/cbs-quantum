using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net;
using AndroidUri = Android.Net.Uri;
using com.origon.wikinotes.db;
using Android.Database;
using Android.Util;
using Android.Text.Util;

namespace com.origon.wikinotes
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    //[IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { Intent.CategoryLauncher })]
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = WikiNote.Notes.NOTE_MIME_TYPE)]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataMimeType = WikiNote.Notes.NOTE_MIME_TYPE)]
    public class WikiNotes : Activity
    {
        private static string KEY_URL = "wikiNotesURL";

        #region Menu Item ids
        public const int EDIT_ID = Menu.First;
        public const int HOME_ID = Menu.First + 1;
        public const int LIST_ID = Menu.First + 3;
        public const int DELETE_ID = Menu.First + 4;
        public const int ABOUT_ID = Menu.First + 5;
        public const int EXPORT_ID = Menu.First + 6;
        public const int IMPORT_ID = Menu.First + 7;
        public const int EMAIL_ID = Menu.First + 8;
        #endregion

        #region Private data
        private TextView _noteView;
        private ICursor _cursor;
        private AndroidUri _uri;
        private static Java.Util.Regex.Pattern WIKI_WORD_MATCHER;
        private WikiActivityHelper _helper;
        private string _noteName;
        #endregion

        static WikiNotes()
        {
            // Compile the regular expression pattern that will be used to
            // match WikiWords in the body of the note
            WIKI_WORD_MATCHER = Java.Util.Regex.Pattern.Compile("\\b[A-Z]+[a-z0-9]+[A-Z][A-Za-z0-9]+\\b");
        }

        #region Public overrided methods
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);

            menu.Add(0, EDIT_ID, 0, Resource.String.menu_edit)
                .SetShortcut('1', 'e')
                .SetIcon(Resource.Drawable.icon_delete);

            //menu.add(0, HOME_ID, 0, R.string.menu_start).setShortcut('4', 'h')
            //    .setIcon(R.drawable.icon_start);

            menu.Add(0, LIST_ID, 0, Resource.String.menu_recent)
                .SetShortcut('3', 'r')
                .SetIcon(Resource.Drawable.icon_recent);

            menu.Add(0, DELETE_ID, 0, Resource.String.menu_delete)
                .SetShortcut('2', 'd')
                .SetIcon(Resource.Drawable.icon_delete);
            //menu.add(0, ABOUT_ID, 0, R.string.menu_about).setShortcut('5', 'a')
            //    .setIcon(android.R.drawable.ic_dialog_info);
            //menu.add(0, EXPORT_ID, 0, R.string.menu_export).setShortcut('7', 'x')
            //    .setIcon(android.R.drawable.ic_dialog_info);
            //menu.add(0, IMPORT_ID, 0, R.string.menu_import).setShortcut('8', 'm')
            //    .setIcon(android.R.drawable.ic_dialog_info);	
            //menu.add(0, EMAIL_ID, 0, R.string.menu_email).setShortcut('6', 'm')
            //    .setIcon(android.R.drawable.ic_dialog_info);
            return true;
        }
        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {
                case EDIT_ID:
                    _helper.EditNote(_noteName, _cursor);
                    return true;
                case HOME_ID:
                    //mHelper.goHome();
                    return true;
                case DELETE_ID:
                    _helper.DeleteNote(_cursor);
                    return true;
                case LIST_ID:
                    _helper.ListNotes();
                    return true;
                case WikiNotes.ABOUT_ID:
                    //Eula.showEula(this);
                    return true;
                case WikiNotes.EXPORT_ID:
                    //mHelper.exportNotes();
                    return true;
                case WikiNotes.IMPORT_ID:
                    //mHelper.importNotes();
                    return true;
                case WikiNotes.EMAIL_ID:
                    //mHelper.mailNote(mCursor);
                    return true;
                default:
                    return false;
            }
        }
        #endregion

        #region Protected overrided methods
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            this._noteView = FindViewById<TextView>(Resource.Id.noteview);
            AndroidUri localUri = Intent.Data;

            if ((localUri == null) && (bundle != null))
            {
                localUri = AndroidUri.Parse(bundle.GetString(KEY_URL));
            }

            if ((localUri == null) || (localUri.PathSegments.Count < 2))
            {
                localUri = AndroidUri.WithAppendedPath(WikiNote.Notes.ALL_NOTES_URI, Resources.GetString(Resource.String.start_page));
            }

            ICursor localCursor = ManagedQuery(localUri, WikiNote.WIKI_NOTES_PROJECTION, null, null, null);

            bool newNote = false;
            if ((localCursor == null) || (localCursor.Count == 0))
            {
                try
                {
                    // no matching wikinote, so create it
                    localUri = ContentResolver.Insert(localUri, null);
                    if (localUri == null)
                    {
                        Log.Error("WikiNotes", "Failed to insert new wikinote into " + Intent.Data);
                        Finish();
                        return;
                    }
                    // make sure that the new note was created successfully, and
                    // select it
                    localCursor = ManagedQuery(localUri, WikiNote.WIKI_NOTES_PROJECTION, null, null, null);
                    if ((localCursor == null) || (localCursor.Count == 0))
                    {
                        Log.Error("WikiNotes", "Failed to open new wikinote: " + Intent.Data);
                        Finish();
                        return;
                    }
                    newNote = true;
                }
                catch (Exception ex)
                {
                    Log.Error(WikiNote.LOG_TAG, ex.Message);
                }
            }

            _uri = localUri;
            _cursor = localCursor;
            localCursor.MoveToFirst();
            _helper = new WikiActivityHelper(this);

            // get the note name
            String noteName = _cursor.GetString(_cursor
                .GetColumnIndexOrThrow(WikiNote.Notes.TITLE));
            _noteName = noteName;

            // set the title to the name of the page
            Title = Resources.GetString(Resource.String.wiki_title, noteName);

            // If a new note was created, jump straight into editing it
            if (newNote)
            {
                _helper.EditNote(noteName, null);
            }

            // Set the menu shortcut keys to be default keys for the activity as well
            SetDefaultKeyMode(DefaultKey.Shortcut);
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            // Put the URL currently being viewed into the icicle
            outState.PutString(KEY_URL, _uri.ToString());
        }
        protected override void OnResume()
        {
            base.OnResume();
            ICursor c = _cursor;
            if (c.Count < 1)
            {
                // if the note can't be found, don't try to load it -- bail out
                // (probably means it got deleted while we were frozen;
                // thx to joe.bowbeer for the find)
                Finish();
                return;
            }
            c.Requery();
            c.MoveToFirst();
            ShowWikiNote(c.GetString(c.GetColumnIndexOrThrow(WikiNote.Notes.BODY)));
        }
        /// <summary>
        /// If the note was edited and not canceled, commit the update to the
        /// database and then refresh the current view of the linkified note.
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="resultCode"></param>
        /// <param name="result"></param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent result)
        {
            base.OnActivityResult(requestCode, resultCode, result);
            if ((requestCode == WikiActivityHelper.ACTIVITY_EDIT) &&
                (resultCode == Result.Ok))
            {
                // edit was confirmed - store the update
                ICursor c = _cursor;
                c.Requery();
                c.MoveToFirst();
                AndroidUri noteUri = ContentUris.WithAppendedId(WikiNote.Notes.ALL_NOTES_URI, c.GetInt(0));
                ContentValues values = new ContentValues();
                values.Put(WikiNote.Notes.BODY, result.GetStringExtra(WikiNoteEditor.ACTIVITY_RESULT));
                values.Put(WikiNote.Notes.MODIFIED_DATE, DateTime.Now.ToFileTimeUtc());

                ContentResolver.Update(noteUri, values, WikiNote.Notes.Id + " = " + c.GetInt(0), null);
                ShowWikiNote(c.GetString(c.GetColumnIndexOrThrow(WikiNote.Notes.BODY)));
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Show the wiki note in the text edit view with both the default Linkify
        /// options and the regular expression for WikiWords matched and turned
        /// into live links.
        /// </summary>
        /// <param name="body"></param>
        private void ShowWikiNote(string body)
        {
            TextView noteView = _noteView;
            noteView.Text = body;

            // Add default links first - phone numbers, URLs, etc.
            Linkify.AddLinks(noteView, MatchOptions.All);

            // Now add in the custom linkify match for WikiWords
            Linkify.AddLinks(noteView, WIKI_WORD_MATCHER, WikiNote.Notes.ALL_NOTES_URI.ToString() + "/");
        }
        #endregion
    }
}

