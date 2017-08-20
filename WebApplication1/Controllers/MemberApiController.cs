using MvcBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    //http://www.atmarkit.co.jp/ait/articles/1311/06/news070_2.html


    public class MemberApiController : ApiController
    {
        //参照するデータベース
        private MvcBasicContext db = new MvcBasicContext();

        // GET: api/MemberApi
        public IEnumerable<Member> Get()
        {
            return db.Members;
        }

        // GET: api/MemberApi/5
        public IEnumerable<Member> Get(int id)
        {
           
            return db.Members.Where(x => x.Id == id);
        }

        // POST: api/MemberApi
        //public bool Post([FromBody]String Sei, String Mei, String Mail)
        //public bool Post([FromBody]Member member)
         public bool Post( [FromBody]Member member)
        {
            try
            {
                db.Members.Add(member);
                db.SaveChanges();
                return true;

            }
            catch(Exception e) {
                return false;
            }

        }

        // PUT: api/MemberApi/5
//        public bool Put(String id, [FromBody]Member member)
         public bool Put([FromBody]Member member)
        {
            // URLの絶対パスを取得
            string absolutePath = this.Request.RequestUri.AbsolutePath;
            // URLパラメータ「category」の値を取得
           // string routeCategory = this.Request.GetRouteData().Values["category"].ToString();
            // クエリ文字列「number」の値の取得
            //string queryNumber = this.Request.GetQueryNameValuePairs().First(q => q.Key == "number").Value;
            // ヘッダ「Accept」の値の取得
            string acceptEncoding = this.Request.Headers.Accept.First().MediaType;
            // Bodyに格納されたバイナリ値を（同期で）取得
            System.IO.Stream stream = ((StreamContent)this.Request.Content).ReadAsStreamAsync().Result;


            var r = Request.Properties.Values;

            var rct = RequestContext;

           

            var ctr = ControllerContext.RequestContext;
            var rvalues = ctr.RouteData.Values;


            try
            {
                db.Members.Add(member);
                db.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        // DELETE: api/MemberApi/5
        public void Delete(int id)
        {
        }
    }
}
