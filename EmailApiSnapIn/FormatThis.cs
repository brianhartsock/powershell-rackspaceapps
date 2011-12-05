using System;
using System.Text;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Helper class to format output written to the console.
    /// </summary>
    public static class FormatThis
    {
        public static string Request(IWebClient client, string url, string method)
        {
            var builder = new StringBuilder();
            builder.Append(method);
            builder.Append(" ");
            builder.Append(url);
            builder.Append(Environment.NewLine);

            foreach (var header in client.Headers)
            {
                builder.Append(header.ToString());
                builder.Append(":");
                builder.Append(client.Headers[header.ToString()]);
                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
