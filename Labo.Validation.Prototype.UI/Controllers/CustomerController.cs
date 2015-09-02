namespace Labo.Validation.Prototype.UI.Controllers
{
    using System.Web.Mvc;

    using Labo.Validation.Prototype.UI.Models;

    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Insert()
        {
            return View(new CustomerInsertViewModel());
        }

        [HttpPost]
        public ActionResult Insert(CustomerInsertViewModel model)
        {
            return View(model);
        }

        public ActionResult InsertWithClientValidation()
        {
            return View(new CustomerInsertViewModel());
        }

        [HttpPost]
        public ActionResult InsertWithClientValidation(CustomerInsertViewModel model)
        {
            return View(model);
        }
    }
}
