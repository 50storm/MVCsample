using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBasic.Models;
using System.Net;

namespace WebApplication1.Controllers
{
    public class BeginController : Controller
    {

        private MvcBasicContext db = new MvcBasicContext();

        // GET: Begin
        public ActionResult Index()
        {
            //return View();

            return Content("<string>こんにちは！世界！</strong>");
        }

        public ActionResult Show() {
            ViewBag.Message = "テスト";
            return View();
        }

        public ActionResult Detail(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            return View(member);


        }

        public ActionResult List() {

            var q = db.Members.OrderByDescending(m=>m.Id);

            return View(q);
            //return View(db.Members.OrderByDescending());

        }

    
    }
}