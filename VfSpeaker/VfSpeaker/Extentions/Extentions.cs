// ***********************************************************************
// Assembly         : VfSpeaker
// Author           : Cameleer
// Created          : 12-16-2012
//
// Last Modified By : Cameleer
// Last Modified On : 12-16-2012
// ***********************************************************************
// <copyright file="Extentions.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace VfSpeaker.Extentions
{
    /// <summary>
    /// Class CookieExtentions
    /// </summary>
    public static class CookieExtentions
    {
        /// <summary>
        /// To the header string.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        /// <returns>System.String.</returns>
        public static string ToHeaderString(this Cookie cookie)
        {
            StringBuilder result = new StringBuilder( string.Empty );

            result.AppendFormat("{0}={1}", cookie.Name, cookie.Value ?? string.Empty);

            return result.ToString();
        }

        /// <summary>
        /// To the header string.
        /// </summary>
        /// <param name="cookieCollection">The cookie collection.</param>
        /// <returns>System.String.</returns>
        public static string ToHeaderString(this CookieCollection cookieCollection)
        {
            StringBuilder result = new StringBuilder(string.Empty);

            foreach (Cookie item in cookieCollection)
            {
                result.Append((result.Length == 0) ? "" : "; ");
                result.AppendFormat(item.ToHeaderString());
            }

            return result.ToString();
        }
    }

    /// <summary>
    /// Class NameValueCollectionExtentions
    /// </summary>
    public static class NameValueCollectionExtentions
    {
        /// <summary>
        /// To the query string.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>System.String.</returns>
        public static string ToQueryString(this NameValueCollection values)
        {
            StringBuilder result = new StringBuilder(string.Empty);

            foreach (string key in values)
            {
                result.Append((result.Length == 0) ? "" : "&");
                result.AppendFormat("{0}={1}", key, values[key]);
            }

            return result.ToString();
        }
    }

    /// <summary>
    /// Class StringExtentions
    /// </summary>
    public static class StringExtentions
    {
        /// <summary>
        /// Froms the response cookie.
        /// </summary>
        /// <param name="cookieString">The cookie string.</param>
        /// <param name="domain">The domain.</param>
        /// <returns>CookieCollection.</returns>
        public static CookieCollection FromResponseCookie(this string cookieString, string domain)
        {
            CookieCollection result = new CookieCollection();

            //CookieContainer cookieContainer = new CookieContainer();
            //cookieContainer.SetCookies(new Uri("http://www.domain.com"), cookieString);
            //result = cookieContainer.GetCookies(new Uri("http://www.domain.com"));

            foreach (string item in cookieString.Split(','))
            {
                string cookieItem = item.Trim();
                Cookie cookie = new Cookie();

                foreach (string prop in cookieItem.Split(';'))
                {
                    string[] pair = prop.Split('=');

                    if (pair[0].Trim() == "path")
                    {
                        cookie.Path = pair[1].Trim();
                    }
                    else if (pair[0].Trim() == "expires")
                    {
                        cookie.Expires = Convert.ToDateTime(pair);
                    }
                    else
                    {
                        cookie.Name = pair[0].Trim();
                        cookie.Value = pair[1].Trim();
                    }
                }
                cookie.Domain = domain;
                result.Add(cookie);
            }

            return result;
        }

        /// <summary>
        /// Splits the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="length">The length.</param>
        /// <returns>List{String}.</returns>
        public static List<String> Split(this string source, int length)
        {
            List<string> result = new List<string>();

            string[] tokens = source.Split(new char[] { ' ', '\t', '\n', '\r', '\f' }, StringSplitOptions.RemoveEmptyEntries);

            string part = String.Empty;
            foreach (string token in tokens)
            {
                if ((part.Length + token.Length + 1) > length)
                {
                    result.Add(part);
                    part = String.Empty;
                }

                part += token + " ";
            }

            if (!String.IsNullOrEmpty(part))
            {
                result.Add(part);
            }

            return result;
        }
    }
}
