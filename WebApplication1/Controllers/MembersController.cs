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
                /*
                db.Members.Add(member);
                db.SaveChanges();
                string email = member.Email;

                //登録したE-Mailで引く
                //var m = db.Members.Where(x => x.Email == email);
          

                //メール
                string url=null;

                //string hashedText = Utilities.MyTool.GetHashedTextString(DateTime.Now.ToString());

                 string controllerName = this.RouteData.Values["controller"].ToString();
                
                
                var helper = new UrlHelper(ControllerContext.RequestContext);

                var indexPath = helper.Action("Index", "Home"); // => "/Home"
                var activationUrl = helper.Action("Activate", controllerName, null, Request.Url.Scheme); // => 		indexUrl	"http://localhost:50195/Members/Activate"	string

                //Emailでハッシュを作り
                string hashedText = Utilities.MyTool.GetHashedTextString(member.Email);

                //RouteValueDictionary parameters = new RouteValueDictionary {{ "Axtivate", hashedText  } };
                //var ActivationKey = parameters.Where(x => x.Key == "Activate");

                //VirtualPathData vpd = RouteTable.Routes.GetVirtualPath(
                  //null,
                  //"Members",
                  //parameters);

                ////url = vpd.VirtualPath;


                //Hotmailでメールを送信する


                string mailTitle = "テストメールです。";
                string mailText = "このメールは、テストです。" + Environment.NewLine + activationUrl+"?"+ hashedText;


                //MailMessageの作成
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(member.Email, member.Email, mailTitle , mailText);

                System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();
                ////SMTPサーバーなどを設定する
                //sc.Host = "smtp.live.com";
                //sc.Port = 587;
                //sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                ////ユーザー名とパスワードを設定する
                //sc.Credentials = new System.Net.NetworkCredential("iga1128@msn.com", "xyzhiro0415");

                //SMTPサーバーなどを設定する
                sc.Host = "smtp.gmail.com";
                sc.Port = 587;
                sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //ユーザー名とパスワードを設定する
                sc.Credentials = new System.Net.NetworkCredential("iga1128@gmail.com", "Narita1981");
   
                //SSLを使用する
                sc.EnableSsl = true;
                //メッセージを送信する
                sc.Send(msg);

                //後始末
                msg.Dispose();
                //後始末（.NET Framework 4.0以降）
                sc.Dispose();
                //return RedirectToAction("Index");
                return RedirectToAction("Message");
                */


     
                //TempData["member"] = member;
                //return RedirectToAction("CreateConfirm");

                ViewBag.member = member;
                return View("CreateConfirm");


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
