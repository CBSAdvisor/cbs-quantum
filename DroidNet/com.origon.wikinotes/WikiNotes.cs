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
    public class WikiNotes : Activity
    {
        private static string KEY_URL = "wikiNotesURL";

        private TextView _noteView;
        private ICursor _cursor;
        private AndroidUri _uri;
        private static Java.Util.Regex.Pattern WIKI_WORD_MATCHER;

        static WikiNotes()
        {
            // Compile the regular expression pattern that will be used to
            // match WikiWords in the body of the note
            WIKI_WORD_MATCHER = Java.Util.Regex.Pattern.Compile("\\b[A-Z]+[a-z0-9]+[A-Z][A-Za-z0-9]+\\b");
        }

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
                catch(Exception ex)
                {
                    Log.Error(WikiNote.LOG_TAG, ex.Message);
                }
            }

            _uri = localUri;
            _cursor = localCursor;
            localCursor.MoveToFirst();
            //mHelper = new WikiActivityHelper(this);
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
        protected override void OnActivityResult(int requestCode, Result resultCode,  Intent result)
        {
            base.OnActivityResult(requestCode, resultCode, result);
        }


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

    }
}

