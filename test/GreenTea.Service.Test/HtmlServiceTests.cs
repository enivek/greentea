using GreenTea.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace GreenTea.Service.Test
{
    [TestClass]
    public class HtmlServiceTests
    {
        private readonly IServiceProvider _provider;

        public HtmlServiceTests()
        {
            IServiceCollection services = new ServiceCollection();
            GreenTea.Service.Main.Startup(services);
            services.AddLogging();
            _provider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Happy Path Test
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var contentDirectory = Path.Join(Directory.GetCurrentDirectory(), "content");

            var config = _provider.GetService<HtmlServiceConfig>();
            config.ContentDirectory = contentDirectory;

            var htmlService = _provider.GetService<IHtmlService>();
            var request = new Request("readme.txt");

            var result = htmlService.GetFile(request);
            
            string fileContents;
            using (var reader = new StreamReader(result.FileStream))
            {
                fileContents = reader.ReadToEnd();
            }

            Assert.IsNotNull(fileContents);
            Assert.IsTrue(fileContents.IndexOf("README") >= 0);
        }

        /// <summary>
        /// Test that prevents access outside of content directory.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod2()
        {
            var contentDirectory = Path.Join(Directory.GetCurrentDirectory(), "content");

            var config = _provider.GetService<HtmlServiceConfig>();
            config.ContentDirectory = contentDirectory;

            var htmlService = _provider.GetService<IHtmlService>();
            var request = new Request("/../../readme.txt");

            var result = htmlService.GetFile(request);
        }
    }
}