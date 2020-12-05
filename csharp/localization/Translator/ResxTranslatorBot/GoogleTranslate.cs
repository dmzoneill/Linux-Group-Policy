using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace ResxTranslator
{
	public static class GoogleTranslate
	{
		/// <summary>
		/// Translate Text using Google Translate
		/// </summary>
		/// <param name="input">The string you want translated</param>
		/// <param name="languagePair">2 letter Language Pair, delimited by "|". 
		/// e.g. "en|da" language pair means to translate from English to Danish</param>
		/// <param name="encoding">The encoding.</param>
		/// <returns>Translated to String</returns>
		public static string TranslateText(string input, string languagePair)
		{
			string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
			WebClient webClient = new WebClient();
			webClient.Encoding = System.Text.Encoding.UTF8;
			string result = webClient.DownloadStringUsingResponseEncoding(url);
			result = result.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
			result = result.Substring(result.IndexOf(">") + 1);
			result = result.Substring(0, result.IndexOf("</span>"));
			return result.Trim();
		}

		private static void UnitTest()
		{
			string tst = TranslateText("hi", "en|ru");
		}

		private static string DownloadStringUsingResponseEncoding(this WebClient client, string address)
		{
			if (client == null) throw new ArgumentNullException("client");
			return DownloadStringUsingResponseEncodingImpl(client, client.DownloadData(address));
		}

		private static string DownloadStringUsingResponseEncodingImpl(WebClient client, byte[] data)
		{
			Debug.Assert(client != null);
			Debug.Assert(data != null);

			var contentType = client.GetResponseContentType();

			var encoding = contentType == null || string.IsNullOrEmpty(contentType.CharSet)
						 ? client.Encoding
						 : Encoding.GetEncoding(contentType.CharSet);

			return encoding.GetString(data);
		}

		private static ContentType GetResponseContentType(this WebClient client)
		{
			if (client == null) throw new ArgumentNullException("client");

			var headers = client.ResponseHeaders;
			if (headers == null)
				throw new InvalidOperationException("Response headers not available.");

			var header = headers["Content-Type"];

			return !string.IsNullOrEmpty(header)
				 ? new ContentType(header)
				 : null;
		}
	}
}
