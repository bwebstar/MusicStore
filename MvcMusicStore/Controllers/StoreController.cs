using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            //var genres = db.Genres.ToList();

            //return View(genres);

            var albums = db.Albums.ToList();

            return View(albums);
        }

        public ActionResult Details(int id)
        {
            var album = db.Albums.Find(id);
            return View(album);
        }

        public ActionResult Browse(string genre)
        {
            // Retrieve Genre and its Associated Albums from database
            var genreModel = db.Genres.Include("Albums")
            .Single(g => g.Name == genre);
            return View(genreModel);
        }

        //
        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = db.Genres.ToList();
            return PartialView(genres);
        }

        // Quick Search Funtionality
        public ActionResult ArtistSearch(string q)
        {
            var artists = GetArtists(q);

            return Json(artists, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuickSearch(string term)
        {
            var artists = GetArtists(term).Select(a => new { value = a.Name });

            return Json(artists, JsonRequestBehavior.AllowGet);
        }

        private List<Artist> GetArtists(string searchString)
        {
            return db.Artists
            .Where(a => a.Name.Contains(searchString))
            .ToList();
        }
    }
}