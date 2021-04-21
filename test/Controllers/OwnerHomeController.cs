using System.Web.Mvc;
using System.Web.Security;

namespace test.Controllers
{
    public class OwnerHomeController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();


        public ActionResult Index()
        {

            return View();


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Settings()
        {


            return View();


        }

        public ActionResult About()
        {

            return View();

        }


        public ActionResult Help()
        {

            return View();

        }


        public ActionResult PetMedication()
        {

            return View();

        }

    }
}
