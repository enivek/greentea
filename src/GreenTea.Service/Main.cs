using GreenTea.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GreenTea.Service
{
    public static class Main
    {
        public static void Startup(IServiceCollection services)
        {
            services.AddSingleton(s => new HtmlServiceConfig()); 

            services.AddTransient<IHtmlService, HtmlService>();
        }
    }
}
