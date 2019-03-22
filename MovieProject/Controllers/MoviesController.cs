using MovieProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieProject.Controllers
{
    public class AlbumsController : Controller
    {
        private movieDBEntities1 db = new movieDBEntities1();

        // GET: Movies
        public ActionResult Index(string searchString, string movieGenre)
        {
            List<string> genreList = new List<string>();
            var genreQuery = from g in db.movies
                             orderby g.genre
                             select g.genre;
            genreList.AddRange(genreQuery.Distinct());
            ViewBag.movieGenre = new SelectList(genreList);

                        var movies = from m in db.movies
                         select m;
            if(!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.genre == movieGenre);
            }

            if(!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(x => x.movietitle.Contains(searchString));
            }

            //return View(db.movies.ToList());
            return View(movies);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "movieid, movietitle, releasedate, genre, price, img, video")]movies mov)
        {
            if (ModelState.IsValid)
            {
                db.movies.Add(mov);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mov);
        }

        //The details method to retreieve individual row/record
        public ActionResult Details(int? movieid)
        {
            if (movieid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            movies mov = db.movies.Find(movieid);
            if (mov == null)
            {
                return HttpNotFound();
            }
            return View(mov);
        }

        public ActionResult Edit(int? movieid)
        {
            if (movieid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            movies mov = db.movies.Find(movieid);
            if (mov == null)
            {
                return HttpNotFound();
            }
            return View(mov);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "movieid, movietitle, releasedate, genre, price, img, video")]movies mov)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mov).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mov);
        }

        public ActionResult Delete(int? movieid)
        {
            if (movieid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            movies mov = db.movies.Find(movieid);
            if (mov == null)
            {
                return HttpNotFound();
            }
            return View(mov);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int movieid)
        {
            movies mov = db.movies.Find(movieid);
            db.movies.Remove(mov);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}