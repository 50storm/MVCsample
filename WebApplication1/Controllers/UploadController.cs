using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class UploadController : Controller
    {
        // GET:ビューを表示するだけ
        public ActionResult Index()
        {
            return View();
        }


        //POSTの時に受け取る
        [HttpPost]
        public ActionResult Index(HttpPostedFileWrapper uploadFile)
        {
            if (uploadFile != null)
            {
                //日付つける
                string fileName = DateTime.Now.ToString() +  uploadFile.FileName;

                uploadFile.SaveAs(Server.MapPath("~/uploads/") + fileName);

            }

            return View();
        }
    }
}