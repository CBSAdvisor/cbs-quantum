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
using com.origon.wikinotes.db;
using Android.Text;

namespace com.origon.wikinotes
{
    [Activity(Label = "Edit note")]
    [IntentFilter(new[] { Intent.ActionEdit },
        Categories = new[] { Intent.CategoryDefault },
        DataMimeType = WikiNote.Notes.NOTE_MIME_TYPE)]
    public class WikiNoteEditor : Activity
    {
        public const string ACTIVITY_RESULT = "com.origon.wikinotes.EDIT";
        private EditText _noteEdit;
        private string _wikiNoteTitle;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.WikiNoteEditor);

            _noteEdit = (EditText)FindViewById(Resource.Id.noteEdit);

            string wikiNoteText = bundle == null ? null : bundle.GetString(WikiNote.Notes.BODY);
            string wikiNoteTitle = bundle == null ? null : bundle.GetString(WikiNote.Notes.TITLE);

            if (wikiNoteTitle == null)
            {
                Bundle extras = Intent.Extras;
                wikiNoteText = extras == null ? null : extras.GetString(WikiNote.Notes.BODY);
                wikiNoteTitle = extras == null ? null : extras.GetString(WikiNote.Notes.TITLE);
            }

            // If we have no title information, this is an invalid intent request
            if (TextUtils.IsEmpty(wikiNoteTitle))
            {
                // no note title - bail
                SetResult(Result.Canceled);
                Finish();
                return;
            }

            _wikiNoteTitle = wikiNoteTitle;
            // set the title so we know which note we are editing
            Title = GetString(Resource.String.wiki_editing, wikiNoteTitle);

            // but if the body is null, just set it to empty - first edit of this note
            wikiNoteText = wikiNoteText == null ? "" : wikiNoteText;
            // set the note body to edit
            _noteEdit.Text = wikiNoteText;

            // set listeners for the confirm and cancel buttons
            ((Button)FindViewById(Resource.Id.confirmButton))
                .Click += delegate {
                    Intent i = new Intent();
                    i.PutExtra(ACTIVITY_RESULT, _noteEdit.Text);
                    SetResult(Result.Ok, i);
                    Finish();
                };

            ((Button)FindViewById(Resource.Id.confirmButton))
                .Click += delegate {
                    SetResult(Result.Canceled);
                    Finish();
                };
        }

        void WikiNoteEditor_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}