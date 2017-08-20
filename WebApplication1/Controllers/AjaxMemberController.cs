using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcBasic.Models;
using System.Web.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.ComponentModel;

namespace WebApplication1.Controllers
{
    public static class Extentions
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(anonymousObject))
            {
                var obj = propertyDescriptor.GetValue(anonymousObject);
                expando.Add(propertyDescriptor.Name, obj);
            }

            return (ExpandoObject)expando;
        }
    }

    public class AjaxMemberController : Controller
    {
        private MvcBasicContext db = new MvcBasicContext();

        private Setting dbSetting = new Setting();


        // GET: AjaxMember
        public ActionResult Index()
        {
            var connStr = WebConfigurationManager.ConnectionStrings["MvcBasicContext"].ToString();
            using (IDbConnection dbDapper = new SqlConnection(connStr)) {
                dbDapper.Open();
                var sql = "SELECT * FROM Settings WHERE RecCd = 'H1'";
                IEnumerable<dynamic> res = dbDapper.Query(sql, null);
                dbSetting.SettingH1 = res;

                sql = "SELECT * FROM Settings WHERE RecCd = 'I1'";
                res = dbDapper.Query(sql, null);
                dbSetting.SettingI1 = res;

                sql = "SELECT * FROM Settings WHERE RecCd = 'I2'";
                res = dbDapper.Query(sql, null);
                dbSetting.SettingI2 = res;


                //HMTL表示用にH1,I1,I2を一つにする。
                var dataH1_I1_I2 = dbSetting.SettingH1
                                        .Zip(dbSetting.SettingI1, (H1,   I1) => new { H1,I1})
                                        .Zip(dbSetting.SettingI2, (H1I1, I2) => new {H1I1.H1, H1I1.I1, I2 });

                dbSetting.dataH1_I1_I2 = dataH1_I1_I2;

                foreach (var item in dataH1_I1_I2)
                {
                    //Debug.WriteLine(item.H1);
                    //Debug.WriteLine(item.I1);

                    var h1KbnCd = item.H1.KbnCd;
                    var i1KbnCd = item.I1.KbnCd;
                    var i2KbnCd = item.I2.KbnCd;

                    //var h1KbnCd = item.H1.KbnCd.ToString() ;
                    //var i1KbnCd = item.I1.KbnCd.ToString();
                    //var i2KbnCd = item.I2.KbnCd.ToString();
                    ////Debug.WriteLine(h1KbnCd as string);
                    //Debug.WriteLine(i1KbnCd as string);
                    //Debug.WriteLine(i2KbnCd as string);

                }


                foreach (var H1 in dbSetting.SettingH1) {
                    var RecCd = H1.RecCd;
                    var KbnCd = H1.KbnCd;

                }


                //dbSetting.SettingH1 = res as IEnumerable<string>;
                dbDapper.Close();

            }


            return View("Test", dbSetting);
            //return View("Test", dbSetting.ToExpando());
            //return View(db.Members.ToList());
        }

        // GET: AjaxMember/Details/5
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

        // GET: AjaxMember/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AjaxMember/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create (Member member, String mode)
        {

            return Json(member);

            //switch (mode)
            //{
            //    case "0":
            //        //登録
            //        ViewBag.mode = "登録";

            //    break;

            //    case "1":
            //        //修正
            //        ViewBag.mode = "修正";

            //        break;

            //    case "2":
            //        //削除

            //        ViewBag.mode = "削除";
            //    break;
                    
            //}

            //if (ModelState.IsValid)
            //{
            //    db.Members.Add(member);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(member);
        }

        // GET: AjaxMember/Edit/5
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

        // POST: AjaxMember/Edit/5
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

        // GET: AjaxMember/Delete/5
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

        // POST: AjaxMember/Delete/5
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
