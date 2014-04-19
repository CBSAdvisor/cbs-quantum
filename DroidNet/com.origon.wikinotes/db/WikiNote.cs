using Android.Content;
using Android.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidUri = Android.Net.Uri;

namespace com.origon.wikinotes.db
{
    public class WikiNote
    {
        public static string WIKINOTES_BASE_PATH = "wikinotes";

        /// <summary>
        /// The root authority for the WikiNotesProvider
        /// </summary>
        public static string WIKINOTES_AUTHORITY = "com.origon.wikinotes.db.WikiNotesProvider";
        public static readonly Android.Net.Uri WIKINOTES_CONTENT_URI = Android.Net.Uri.Parse("content://" + WIKINOTES_AUTHORITY + "/" + WIKINOTES_BASE_PATH);

        public static string[] WIKI_EXPORT_PROJECTION = { "title", "body", "created", "modified" };
        public static string[] WIKI_NOTES_PROJECTION = { "_id", "title", "body", "modified" };
        public static string LOG_TAG = "WikiNotes";

        public class Notes
        {
            public const string Count = "_count";

            public const string TABLE_WIKINOTES = "wikinotes";
            public const string Id = "_id";
            public const string TITLE = "title";
            public const string BODY = "body";
            public const string CREATED_DATE = "created";
            public const string MODIFIED_DATE = "modified";

            // MIME types used for getting a list, or a single vegetable
            public static readonly string NOTES_MIME_TYPE = ContentResolver.CursorDirBaseType + "/vnd.com.origon.wikinotes.db.Notes";
            public static readonly string NOTE_MIME_TYPE = ContentResolver.CursorItemBaseType + "/vnd.com.origon.wikinotes.db.Notes";

            public static AndroidUri ALL_NOTES_URI = AndroidUri.Parse(WIKINOTES_CONTENT_URI.ToString());
            public const string BY_TITLE_SORT_ORDER = "title ASC";
            public static AndroidUri LIVE_FOLDER_URI = AndroidUri.Parse("content://" + WIKINOTES_AUTHORITY + "/wiki/live_folder");
            public const string MOST_RECENTLY_MODIFIED_SORT_ORDER = "modified DESC";
            public static AndroidUri SEARCH_URI = AndroidUri.Parse("content://" + WIKINOTES_AUTHORITY + "/wiki/search");
        }
    }
}
