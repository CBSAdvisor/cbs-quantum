using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uri = Android.Net.Uri;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using com.origon.wikinotes.db;
using Android.Database;

namespace com.origon.wikinotes
{
    [Activity(Label = "Notes list")]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault },
        DataMimeType = WikiNote.Notes.NOTES_MIME_TYPE)]
    [IntentFilter(new[] { Intent.ActionSearch },
        Categories = new[] { Intent.CategoryDefault })]
    public class WikiNotesList : ListActivity
    {
        private ICursor _cursor;
        private WikiActivityHelper _helper;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Intent intent = Intent;
            Uri uri = null;
            String query = null;

            if (Intent.ActionSearch.Equals(intent.Action))
            {
                query = intent.GetStringExtra(SearchManager.Query);
            }
            else if (bundle != null)
            {
                query = bundle.GetString(SearchManager.Query);
            }

            if (query != null && query.Length > 0)
            {
                uri = Uri.WithAppendedPath(WikiNote.Notes.SEARCH_URI, Uri.Encode(query));
            }

            if (uri == null)
            {
                // somehow we got called w/o a query so fall back to a reasonable
                // default (all notes)
                uri = WikiNote.Notes.ALL_NOTES_URI;
            }

            // Do the query
            ICursor c = ManagedQuery(uri, WikiNote.WIKI_LISTNOTES_PROJECTION, null, null,
                        WikiNote.Notes.BY_TITLE_SORT_ORDER);
            _cursor = c;

            _helper = new WikiActivityHelper(this);

            // Bind the results of the search into the list
            ListAdapter = new SimpleCursorAdapter(
                                      this,
                                      Android.Resource.Layout.SimpleListItem1,
                                      _cursor,
                                      new String[] { WikiNote.Notes.TITLE },
                                      new int[] { Android.Resource.Id.Text1 });
            SetDefaultKeyMode(DefaultKey.Shortcut);
        }
    }
}