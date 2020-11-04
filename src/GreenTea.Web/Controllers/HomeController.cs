using GreenTea.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GreenTea.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHtmlService _htmlService;

        public HomeController(IHtmlService htmlService, ILogger<HomeController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException();
            _htmlService = htmlService ?? throw new ArgumentNullException();
        }

        public IActionResult Index(string permalink)
        {
            var request = new Request(permalink);
            if( request.IsFile() )
            {
                Response.Headers["Content-Disposition"] = "inline; filename=" + request.GetFileName();
                return _htmlService.GetFile(request);
            }
            else
            {
                return _htmlService.GetView(request);
            }
        }
    }
}
