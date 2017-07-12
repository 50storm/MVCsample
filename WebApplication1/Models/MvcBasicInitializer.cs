using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Data.Entity;


namespace MvcBasic.Models
{
    /// <summary>
    /// テーブルがない場合自動生成
    /// 
    /// </summary>
    public class MvcBasicInitializer : DropCreateDatabaseAlways<MvcBasicContext>
    {
        protected override void Seed(MvcBasicContext context)
        {
            var members = new List<Member> {
            new Member{
                LastName = "テスト",
                FirstName = "太郎",
                Email = "test@test.co.jp",
                Birth = DateTime.Parse("1999-11-11"),
                Married = false,
                Memo = "MvcBasicInitializerで自動生成されました。"

            },
            new Member{
                LastName = "テスト",
                FirstName = "次郎",
                Email = "test2@test.co.jp",
                Birth = DateTime.Parse("2002-11-11"),
                Married = false,
                Memo = "MvcBasicInitializerで自動生成されました"
            }
         };
            members.ForEach(m => context.Members.Add(m));
            context.SaveChanges();

        }

    }
}