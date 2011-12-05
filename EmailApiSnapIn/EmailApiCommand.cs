using System;
using System.Management.Automation;
using System.Net;
using System.Collections.Specialized;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Base Cmdlet for all ReST API calls.  This function provides a template for all operations, for which most functionality can 
    /// be extended.
    /// </summary>
    abstract public class EmailApiCommand : PSCmdlet
    {
        public static IEmailApiFactory Factory = new EmailApiFactory();

        public const string AccessIdVariable = "EmailApiAccessId";
        public const string SecretKeyVariable = "EmailApiSecretKey";
        public const string BaseUrlVariable = "EmailApiBaseUrl";

        private DefaultValue<string> accessId;
        private DefaultValue<string> baseUrl;
        private DefaultValue<string> secretKey;
        private IApiConstraint constraint;
        private IOutputWriter requestWriter;
        private IOutputWriter responseWriter;
        private string method;

        public EmailApiCommand(string _method)
        {
            method = _method;
            accessId = new DefaultValue<string>(() => (string)this.SessionState.PSVariable.GetValue(AccessIdVariable, null));
            secretKey = new DefaultValue<string>(() => (string)this.SessionState.PSVariable.GetValue(SecretKeyVariable, null));
            baseUrl = new DefaultValue<string>(() => (string)this.SessionState.PSVariable.GetValue(BaseUrlVariable, null));
        }

        [Parameter(Position = 0, Mandatory = true)]
        public string ResourceUrl;

        [Parameter]
        public string AccessId
        {
            get
            {
                return accessId.Value;
            }
            set
            {
                accessId.Value = value;
            }
        }

        [Parameter]
        public string SecretKey
        {
            get
            {
                return secretKey.Value;
            }
            set
            {
                secretKey.Value = value;
            }
        }

        [Parameter]
        public string BaseUrl
        {
            get
            {
                return baseUrl.Value;
            }
            set
            {
                baseUrl.Value = value;
            }
        }

        [Parameter]
        public Formats Format = Formats.Xml;

        protected override void BeginProcessing()
        {
            ValidateNonPsRequiredParams();

            if (Format == Formats.Xml)
            {
                responseWriter = new XmlOutputWriter();
                constraint = AcceptConstraint.Xml();
            }
            else
            {
                responseWriter = new StringOutputWriter();
                constraint = AcceptConstraint.Json();
            }

            responseWriter = new MacroOutputWriter()
            {
                Writers = new IOutputWriter[] { new VerboseOutputWriter(), responseWriter }
            };
            constraint = new MacroConstraint()
            {
                Constraints = new IApiConstraint[] { new AuthorizationConstraint(AccessId, SecretKey), constraint }
            };

            requestWriter = new VerboseOutputWriter();
        }

        protected override void ProcessRecord()
        {
            var client = Factory.CreateWebClient();
            constraint.ApplyConstraint(client);

            try
            {
                string url = BuildUrl();

                requestWriter.Write(this, FormatThis.Request(client, url, method));

                if (ShouldProcess(method + " " + url))
                {
                    string response = MakeRequest(client, url);

                    //If there isn't a response, there isn't much point to writing anything.
                    if (!string.IsNullOrEmpty(response))
                    {
                        responseWriter.Write(this, response);
                    }
                }
            }
            catch (WebException e)
            {
                throw new ApiException(e);
            }
        }

        private string MakeRequest(IWebClient client, string url)
        {
            if (method == "GET")
            {
                return client.DownloadString(url);
            }
            else
            {
                return client.UploadValues(method, url, GetData());
            }
        }

        protected virtual string BuildUrl()
        {
            return BaseUrl + ResourceUrl;
        }
        protected virtual NameValueCollection GetData()
        {
            return new NameValueCollection();
        }

        private void ValidateNonPsRequiredParams()
        {
            //Validate runtime constraints
            if (string.IsNullOrEmpty(AccessId))
            {
                throw new ArgumentNullException("AccessId");
            }
            if (string.IsNullOrEmpty(SecretKey))
            {
                throw new ArgumentNullException("SecretKey");
            }
            if (string.IsNullOrEmpty(BaseUrl))
            {
                throw new ArgumentNullException("BaseUrl");
            }
        }
    }
}
