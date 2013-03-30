// ***********************************************************************
// Assembly         : VfSpeaker
// Author           : Cameleer
// Created          : 12-17-2012
//
// Last Modified By : Cameleer
// Last Modified On : 01-12-2013
// ***********************************************************************
// <copyright file="VfWebClienBase.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Net;
using VfSpeaker.Extentions;

namespace VfSpeaker.Services
{
    /// <summary>
    /// Class VfWebClienBase
    /// </summary>
    class VfWebClienBase : WebClient
    {
        /// <summary>
        /// The _timeout
        /// </summary>
        private int _timeout = 300 * 1000;
        /// <summary>
        /// The _character set
        /// </summary>
        private string _characterSet = "utf-8";
        /// <summary>
        /// The _cookie
        /// </summary>
        private CookieContainer _cookie = new CookieContainer();

        /// <summary>
        /// Constructor
        /// </summary>
        public VfWebClienBase()
            : base()
        {
            //Proxy = WebRequest.DefaultWebProxy;
            //UseDefaultCredentials = true;

            Headers.Set(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            Headers.Set(HttpRequestHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
            Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
            Headers.Add(HttpRequestHeader.AcceptCharset, "windows-1251,utf-8;q=0.7,*;q=0.3");
            Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.52 Safari/537.17");
            Headers.Set(HttpRequestHeader.CacheControl, "max-age=0");
            Headers.Set(HttpRequestHeader.KeepAlive, "true");
            //Headers.Set(HttpRequestHeader.Host, "http://cards.voicefabric.ru");
        }

        /// <summary>
        /// A CookieContainer that contains the cookies associated with this request.
        /// </summary>
        /// <value>The cookie.</value>
        public CookieContainer Cookie
        {
            get { return _cookie; }
        }

        /// <summary>
        /// The length of time, in milliseconds, until the request times out,
        /// or the value Timeout.Infinite to indicate that the request does not time out
        /// </summary>
        /// <value>The timeout.</value>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// A string that contains the character set of the response.
        /// </summary>
        /// <value>The character set.</value>
        public string CharacterSet
        {
            get { return _characterSet; }
            internal set 
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _characterSet = value;
                    Encoding = Encoding.GetEncoding(_characterSet);
                }
            }
        }

        /// <summary>
        /// The Referer header, which specifies the URI of the
        /// resource from which the request URI was obtained.
        /// </summary>
        /// <value>The referer.</value>
        public Uri Referer
        {
            get { return new Uri(Headers[HttpRequestHeader.Referer]); } 
            set { Headers.Set(HttpRequestHeader.Referer, value.AbsoluteUri); } 
        }

        /// <summary>
        /// Number that indicates the status of the HTTP response.
        /// </summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Copies the existing Headers, Credentials, and method to the newly created WebRequest object.
        /// </summary>
        /// <param name="address"><see cref="T:System.Uri" />, который идентифицирует запрашиваемый ресурс.</param>
        /// <returns>A new WebRequest object for the specified resource.</returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            //Headers.Set(HttpRequestHeader.Host, address.Host);
            
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            if (request.CookieContainer == null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.SetCookies(address, Cookie.GetCookieHeader(address));
            }
            //request.CookieContainer = _cookie;
            request.Timeout = this.Timeout;
            
            return request;
        }

        /// <summary>
        /// The object returned by this method is obtained by calling the GetResponse method on the specified WebRequest object
        /// </summary>
        /// <param name="request">A WebRequest that is used to obtain the response.</param>
        /// <returns>A WebResponse containing the response for the specified WebRequest.</returns>
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            ((HttpWebRequest)request).KeepAlive = true;

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)base.GetWebResponse(request);

                LogRequest((HttpWebRequest)request);
                LogResponse((HttpWebResponse)response);

                CharacterSet = response.CharacterSet;
                StatusCode = response.StatusCode;
                SetCookie(response.ResponseUri);
                //SetCookie(response.ResponseUri.Host);
            }
            catch (Exception ex)
            {
                Log4.DeveloperLog.ErrorFormat("{0}", ex);
            }

            return response;
        }

        /// <summary>
        /// The object returned by this method is obtained by calling the EndGetResponse method on the specified WebRequest object.
        /// </summary>
        /// <param name="request">A WebRequest that is used to obtain the response.</param>
        /// <param name="result">An IAsyncResult object obtained from a previous call to BeginGetResponse .</param>
        /// <returns>A WebResponse containing the response for the specified WebRequest.</returns>
        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            ((HttpWebRequest)request).KeepAlive = true;

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)base.GetWebResponse(request, result);

                LogRequest((HttpWebRequest)request);
                LogResponse((HttpWebResponse)response);

                CharacterSet = response.CharacterSet;
                StatusCode = response.StatusCode;
                SetCookie(response.ResponseUri);
                //SetCookie(response.ResponseUri.Host);
            }
            catch (Exception ex)
            {
                Log4.DeveloperLog.ErrorFormat("{0}", ex);
            }

            return response;
        }

        /// <summary>
        /// Set cookies from response header Set-Cookie
        /// </summary>
        /// <param name="host">The host.</param>
        private void SetCookie(string host)
        {
            //Headers.Get(
            string responseCookie = ResponseHeaders.Get("Set-Cookie");

            if (!String.IsNullOrEmpty(responseCookie))
            {
                Cookie.Add(responseCookie.FromResponseCookie(host));
            }
        }

        /// <summary>
        /// Set cookies from response header Set-Cookie
        /// </summary>
        /// <param name="uri">The host.</param>
        private void SetCookie(Uri uri)
        {
            //Headers.Get(
            string responseCookie = ResponseHeaders.Get("Set-Cookie");

            if (!String.IsNullOrEmpty(responseCookie))
            {
                Cookie.SetCookies(uri, responseCookie);
            }
        }

        private void LogRequest(HttpWebRequest request)
        {
            Log4.DeveloperLog.InfoFormat("Request: {0} {1}", request.Method, request.RequestUri);

            Log4.DeveloperLog.InfoFormat("Request headers: {0}", request.Headers.Count);
            foreach (string header in request.Headers)
            {
                Log4.DeveloperLog.InfoFormat("  {0}: {1}", header, request.Headers.Get(header));
            }

            Log4.DeveloperLog.InfoFormat("Cookies: {0}", request.CookieContainer.Count);
            foreach (Cookie cookie in request.CookieContainer.GetCookies(request.Address))
            {
                Log4.DeveloperLog.InfoFormat("  {0}={1}; path={2}", cookie.Name, cookie.Value, cookie.Path);
            }
            Log4.DeveloperLog.Info("-------------------------------------------------------------------------------\n");
        }

        private void LogResponse(HttpWebResponse response)
        {
            Log4.DeveloperLog.InfoFormat("Response: {0} {1}", response.Method, response.ResponseUri);

            Log4.DeveloperLog.InfoFormat("Response headers: {0}", response.Headers.Count);
            foreach (string header in response.Headers)
            {
                Log4.DeveloperLog.InfoFormat("  {0}: {1}", header, response.Headers.Get(header));
            }

            Log4.DeveloperLog.InfoFormat("Cookies: {0}", response.Cookies.Count);
            foreach (Cookie cookie in response.Cookies)
            {
                Log4.DeveloperLog.InfoFormat("  {0}={1}; path={2}", cookie.Name, cookie.Value, cookie.Path);
            }
            Log4.DeveloperLog.Info("-------------------------------------------------------------------------------\n");
        }
    }
}
