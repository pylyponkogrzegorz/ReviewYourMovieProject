using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReviewYourMovie.Server.Controllers
{
    public class DiscoverMoviesController : Controller
    {
        // GET: DiscoverMoviesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DiscoverMoviesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DiscoverMoviesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiscoverMoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DiscoverMoviesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DiscoverMoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DiscoverMoviesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DiscoverMoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
