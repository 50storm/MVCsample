using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBasic.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcBasic.Models
{
    public class Auth
    {
        /// <summary>
        /// URLの文字列を作って返す
        /// </summary>
        /// <param name="action">アクション</param>
        /// <param name="controller">コントローラー</param>
        /// <param name="scheme">スキーム</param>
        /// <param name="reqContext">RequestContext</param>
        /// <returns></returns>
        public static string createUrl(Member member , string action , string controller , string scheme , RequestContext reqContext) {
            string url = null;
            
            var helper = new UrlHelper(reqContext);
            var indexPath = helper.Action("Index", "Home"); // => "/Home"
            url = helper.Action(action, controller, null, scheme); // => 		indexUrl	"http://localhost:50195/Members/Activate"	string
             //url = helper.Action(action, controller, member.Email, scheme); // => 		"http://localhost:50195/Members/Activate?Length=13"

            //Emailでハッシュを作り
            string hashedText = Utilities.MyTool.GetHashedTextString(member.Email);

            url =url  +"/" + hashedText;


            return url;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="url"></param>
        public static void sendEmall(Member member, String url) {

            string mailTitle = "テストメールです。";
            string mailText = member.LastName + "  " + member.FirstName + "さん" + Environment.NewLine +
			                  "このメールは、テストです。" + Environment.NewLine + url;

            //MailMessageの作成
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(member.Email, member.Email, mailTitle, mailText);
            System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();
            
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

        }
    }
}