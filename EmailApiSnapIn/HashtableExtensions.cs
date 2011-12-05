using System;
using System.Text;
using System.Collections;
using System.Web;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Extension methods for the Hashtable class.
    /// </summary>
    public static class HashtableExtensions
    {
        /// <summary>
        /// Converts a Hashtable to a query string, with URL encoding.
        /// </summary>
        /// <param name="hash">The hashtable to convert.</param>
        /// <returns>The resultant query string.</returns>
        public static string ToQueryString(this Hashtable hash)
        {
            if(hash == null) throw new ArgumentNullException("hash");
            if(hash.Count < 0) return string.Empty;

            var builder = new StringBuilder("?");
            var @enum = hash.GetEnumerator();
            @enum.MoveNext();
            
            while(true)
            {
                builder.Append(HttpUtility.UrlEncode(Convert.ToString(@enum.Entry.Key.ToString())));
                builder.Append("=");

                if (@enum.Entry.Value != null)
                {
                    builder.Append(HttpUtility.UrlEncode(@enum.Entry.Value.ToString()));
                }
                
                if(!@enum.MoveNext()) break;

                builder.Append("&");
            }
 
            return builder.ToString();
        }
    }
}
