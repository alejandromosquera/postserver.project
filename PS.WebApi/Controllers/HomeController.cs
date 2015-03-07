using System.Web.Mvc;
using PS.Bussiness.Managers;

namespace PS.WebApi.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCount()
        {
            var postsManager = new PostsManager();

            ViewBag.Posts = postsManager.Count();            

            return View();
        }
    }
}