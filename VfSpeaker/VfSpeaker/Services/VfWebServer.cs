using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using VfSpeaker.Extentions;
using HtmlAgilityPack;
using System.Xml.XPath;
using System.IO;
using ScrapySharp.Extensions;

namespace VfSpeaker.Services
{
    class VfWebServer
    {
        private static Uri WEBSITE_URL = new Uri("http://cards.voicefabric.ru");
        private static Uri GET_EDIT_URL = new Uri("/?act=edit&ImageID=99", UriKind.Relative);
        private static Uri POST_PREVIEW_URL = new Uri("/?act=preview", UriKind.Relative);
        private static string GET_SOUND_URL = "/?act=getsound&tempid=";

        private VfWebClienBase _webClient = new VfWebClienBase();

        private Uri _refererUrl = new Uri(WEBSITE_URL.ToString());

        public VfWebServer()
        {
            _webClient.Encoding = Encoding.UTF8;
        }

        public CookieContainer Cookie
        {
            get { return _webClient.Cookie; }
        }

        public WebHeaderCollection Headers
        {
            get { return _webClient.Headers; }
            set { _webClient.Headers = value; }
        }

        public Uri Referer
        {
            get { return _refererUrl; }
            set { _refererUrl = value; }
        }

        public string GetTempId()
        {
            string result = string.Empty;
            byte[] bytes = null;
            Uri url = new Uri(WEBSITE_URL, GET_EDIT_URL);

            _webClient.Referer = Referer;

            //WebClient client = new WebClient();
            //byte[] data = client.DownloadData(url); 

            try
            {
                bytes = _webClient.DownloadData(url);
                Log4.DeveloperLog.InfoFormat("Bytes: {0} Url: {1}", bytes.Length, url);

                Referer = url;
            }
            catch (Exception ex)
            {
                Log4.DeveloperLog.ErrorFormat("{0}", ex);
            }

            HtmlDocument doc = new HtmlDocument();

            Encoding docEncoding = _webClient.Encoding;
            doc.OptionDefaultStreamEncoding = docEncoding;

            doc.LoadHtml(docEncoding.GetString(bytes));

            HtmlNode html = doc.DocumentNode;
            IEnumerable<HtmlNode> tempIdMNode = html.CssSelect("input[name=tempid]");
            result = tempIdMNode.ElementAt(0).Attributes["value"].Value;

            Log4.DeveloperLog.InfoFormat("tempid: {0}", result);
            return result;
        }

        public byte[] PostPreview(string tempId, string text)
        {
            byte[] result = null;
            Uri url = new Uri(WEBSITE_URL, POST_PREVIEW_URL);

            _webClient.Referer = Referer;
            _webClient.CharacterSet = "utf-8";
            _webClient.Encoding = Encoding.UTF8;
            _webClient.Headers.Set(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            //_webClient.Headers.Set(HttpRequestHeader.Pragma, "no-cache");

            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("Text", text);
            postValues.Add("cardtype", "mail");
            postValues.Add("FromName", "");
            postValues.Add("FromMail", "");
            postValues.Add("ToName", "");
            postValues.Add("ToMail", "");
            postValues.Add("Voice", "Vladimir");
            //postValues.Add("Voice", "Alexander");
            //postValues.Add("Voice", "Anna");
            //postValues.Add("Voice", "Maria");
            //postValues.Add("Voice", "Lidia");
            //postValues.Add("Voice", "Victoria");
            postValues.Add("MusicID", "0");
            postValues.Add("ImageID", "99");
            postValues.Add("Type", "mail");
            postValues.Add("tempid", tempId);

            //WebClient client = new WebClient();
            //byte[] data = client.UploadValues(url, "POST", postValues);

            try
            {
                result = _webClient.UploadValues(url, "POST", postValues);
                Log4.DeveloperLog.InfoFormat("Bytes: {0} Url: {1}", result.Length, url);
            }
            catch (Exception ex)
            {
                Log4.DeveloperLog.ErrorFormat("{0}", ex);
            }

            Referer = url;

            Log4.DeveloperLog.InfoFormat("tempid: {0}\n Text: {1}", tempId, text);
            return result;
        }

        public byte[] GetSound(string tempId)
        {
            byte[] result = null;
            Uri url = new Uri(WEBSITE_URL, new Uri(GET_SOUND_URL + tempId, UriKind.Relative));

            _webClient.Referer = Referer;
            //_webClient.CharacterSet = "utf-8";
            //_webClient.Encoding = Encoding.UTF8;
            _webClient.Headers.Set(HttpRequestHeader.Accept, "*/*");
            //_webClient.Headers.Set(HttpRequestHeader.Pragma, "no-cache");

            try
            {
                result = _webClient.DownloadData(url);
                Log4.DeveloperLog.InfoFormat("Bytes: {0} Url: {1}", result.Length, url);

                Referer = url;
            }
            catch (Exception ex)
            {
                Log4.DeveloperLog.ErrorFormat("{0}", ex);
            }

            return result;
        }

        /// <summary>
        /// Get encoding from WebClient response
        /// </summary>
        /// <returns>Encoding class instance</returns>
        private Encoding GetEncodingFromResponse()
        { 
            return GetEncodingFromContentType(_webClient.ResponseHeaders != null ? 
                _webClient.ResponseHeaders[HttpResponseHeader.ContentType] : String.Empty);
        }

        /// <summary>
        /// Get encodin from Content-Type header
        /// </summary>
        /// <param name="contentType">Contenet-Type header value</param>
        /// <returns>Encoding class instance</returns>
        private Encoding GetEncodingFromContentType(string contentType)
        {
            Encoding result = Encoding.GetEncoding("ISO-8859-1");

            string[] parsedList = contentType.Split(new char[] { ';', '=' });
            bool nextItem = false;

            foreach (string item in parsedList)
            {
                if (item.Trim() == "charset")
                {
                    nextItem = true;
                }
                else if (nextItem)
                {
                    result = Encoding.GetEncoding(item.Trim());
                }
            }

            return result;
        }
    }
}
