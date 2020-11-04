using GreenTea.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GreenTea.Service
{
    public class HtmlService : IHtmlService
    {
        private static HtmlServiceConfig _serviceConfig;

        public HtmlService(HtmlServiceConfig serviceConfig, ILogger<HtmlService> logger)
        {
            _serviceConfig = serviceConfig ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Return a static file to the user.
        /// </summary>
        public FileStreamResult GetFile(Request request)
        {
            var absolutePath = System.IO.Path.Combine(_serviceConfig.ContentDirectory, request.GetUriAddress());
            var fullPath = System.IO.Path.GetFullPath(absolutePath);
            
            // This will prevent people from viewing fikes outside of the content directory.
            if (!fullPath.StartsWith(_serviceConfig.ContentDirectory))
            {
                throw new ArgumentException();
            }

            var fs = System.IO.File.OpenRead(absolutePath);
            var fsr = new FileStreamResult(fs, request.GetMimeType());
            return fsr;
        }

        /// <summary>
        /// Return a view to the user.
        /// </summary>
        public IActionResult GetView(Request request)
        {
            var absolutePath = "";
            var mime = MimeMapping.MimeUtility.GetMimeMapping(request.GetFileName());
            var fs = System.IO.File.OpenRead(absolutePath);
            var fsr = new FileStreamResult(fs, "");

            return new ContentResult
            {
                ContentType = "text/html",
                Content = "asdasdasdasd"
            };
        }
    }
}
