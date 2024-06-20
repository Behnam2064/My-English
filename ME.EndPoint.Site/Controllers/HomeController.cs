using ME.EndPoint.Site.Models;
using ME.Entities.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ME.EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IDataBaseContext db)
        {
            _logger = logger;




            string address = @"C:\Users\USA\Pictures\pexels-abdullah-ghatasheh-1631677.jpg";
            var binery = System.IO.File.ReadAllBytes(address);
            db.Wfiles.Add(new Entities.Database.WFile()
            {
                FileName = System.IO.Path.GetFileNameWithoutExtension(address),
                FileType = System.IO.Path.GetExtension(address),
                AddedTime = DateTime.Now,
                Category = 0,
                MyWordId = 1,
                UserId = 1,
                FileData = binery,
            });

            db.SaveChanges();
            var wf = db.Wfiles.FirstOrDefault();




            /*string address = @"C:\Users\USA\Pictures\02.jpg";
            var binery = System.IO.File.ReadAllBytes(address);
            db.Wfiles.Add(new Entities.Database.WFile()
            {
                FileName = System.IO.Path.GetFileNameWithoutExtension(address) + "2",
                FileType = System.IO.Path.GetExtension(address),
                AddedTime = DateTime.Now,
                Category = 0,
                MyWordId = 1,
                UserId = 1,
                FileData = binery,
            });

            db.SaveChanges();
            var wf = db.Wfiles.FirstOrDefault();*/


        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
