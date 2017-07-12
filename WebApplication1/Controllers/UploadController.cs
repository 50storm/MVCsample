using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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
            try
            {
                if (uploadFile != null)
                {
                    //日付つける
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(uploadFile.FileName);

                    uploadFile.SaveAs(Server.MapPath("~/uploads/") + fileName);

                }

            }
            catch (Exception e) {
               
            }
            
            return View();
        }
    }
}