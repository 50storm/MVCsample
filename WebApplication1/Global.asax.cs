﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//追加
using MvcBasic.Models;
using System.Data.Entity;

//WEBAPI
using System.Web.Http;
//using System.Web.Routing;


namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);//WEBAPI
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //テストデータ自動生成する場合
            //Database.SetInitializer<MvcBasicContext>(new MvcBasicInitializer());

        }
    }
}
