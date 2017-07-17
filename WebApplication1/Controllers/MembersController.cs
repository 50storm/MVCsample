using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcBasic.Models;
using System.Web.Routing;

namespace WebApplication1.Controllers
{
    public class MembersController : Controller
    {
        private MvcBasicContext db = new MvcBasicContext();

        // GET: Members
        public ActionResult Index()
        {
            try
            {
                return View(db.Members.ToList());
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
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

        public ActionResult Message() {
            return View();
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            ViewBag.Enable = true;

            return View();
        }

        // POST: Members/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,Email,Birth,Married,Memo")] Member member)
        {
            if (ModelState.IsValid)
            {
                //保存
                db.Members.Add(member);
                db.SaveChanges();
                
                string controllerName = this.RouteData.Values["controller"].ToString();
                String url =  Auth.createUrl(member, "Activate", controllerName, Request.Url.Scheme, ControllerContext.RequestContext);

                Auth.sendEmall(member, url);


                // var activationUrl = Action("Activate", controllerName, null, Request.Url.Scheme); // => 		indexUrl	"http://localhost:50195/Members/Activate"	string


                //       ViewBag.member = member;
                //        return View("CreateConfirm");


            }
            return View(member);
        }

        public ActionResult CreateConfirm() {

            ViewBag.member = TempData["member"];

            return View();

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Register([Bind(Include = "Id,LastName,FirstName,Email,Birth,Married,Memo")] Member member)
        {
            //http://qiita.com/kosei/items/d04f794278fba08b949c
            //falseで登録できない
            //
            //if (ModelState.IsValid)
            //{
            db.Members.Add(member);
            //db.SaveChanges()呼び出されたときにバリデーションしない。
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            //}

            //ViewBag.member = member;

            return View();

        }


        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
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

        // POST: Members/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,Email,Birth,Married,Memo")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
