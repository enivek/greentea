using Microsoft.AspNetCore.Mvc;

namespace GreenTea.Service
{
    public interface IHtmlService
    {
        /// <summary>
        /// Return a static file to the user.
        /// </summary>
        FileStreamResult GetFile(Request request);

        /// <summary>
        /// Return a view to the user.
        /// </summary>
        IActionResult GetView(Request request);
    }
}
