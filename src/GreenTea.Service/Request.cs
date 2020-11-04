using System;
using System.Linq;

namespace GreenTea.Service
{
    public sealed class Request
    {
        private readonly string _uriAddress;
        private readonly string _fileName;

        public Request()
        {
            _uriAddress = "/";
            _fileName = string.Empty;
        }

        public Request(string uriAddress)
        {
            if (string.IsNullOrEmpty(uriAddress))
            {
                _uriAddress = "/";
                _fileName = string.Empty;
            }
            else
            {
                _uriAddress = uriAddress.ToLower();

                var splitComponents = uriAddress.Split("/");
                var lastComponent = splitComponents.Last();
                var mimeMapping = MimeMapping.MimeUtility.GetMimeMapping(lastComponent);
                if (mimeMapping.IndexOf("application/") < 0)
                {
                    _fileName = lastComponent;
                }
                else
                {
                    _fileName = string.Empty;
                }
            }
        }

        public string GetUriAddress()
        {
            return _uriAddress;
        }

        public string GetMimeType()
        {
            return MimeMapping.MimeUtility.GetMimeMapping(_fileName);
        }

        public string GetFileName()
        {
            return _fileName;
        }

        public bool IsFile()
        {
            return _fileName != string.Empty;
        }
    }
}
