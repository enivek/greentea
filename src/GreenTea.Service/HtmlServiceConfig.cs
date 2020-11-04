using System;

namespace GreenTea.Service.Models
{
    public sealed class HtmlServiceConfig
    {
        public HtmlServiceConfig()
        {
            ContentDirectory = Environment.GetEnvironmentVariable("GREEN_TEA_STATIC_FILE");
        }

        public string ContentDirectory { get; set; }
    }
}
