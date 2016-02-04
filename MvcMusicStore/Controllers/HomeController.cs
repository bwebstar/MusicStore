using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(4);
            return View(albums);
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return db.Albums
            .OrderByDescending(a => a.OrderDetails.Count())
            .Take(count)
            .ToList();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Daily Deal
        public ActionResult DailyDeal()
        {
            var album = GetDailyDeal();

            return PartialView("_DailyDeal", album);
        }

        // Select an album randomly and discount it

        private Album GetDailyDeal()
        {
            var album = db.Albums
                .OrderBy(a => System.Guid.NewGuid())
                .First();

            album.Price *= 0.5m;
            return album;
        }
        
    }
}


