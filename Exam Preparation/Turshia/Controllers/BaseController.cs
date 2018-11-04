using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework;


namespace Turshia.Controllers
{
    using Services;

    public class BaseController : Controller
    {
        

        protected BaseController()
        {         


            //this.Model.Data["ShowError"] = "none";
            //this.Model.Data["Guest"] = "flex";
            //this.Model.Data["LoggedIn"] = "none";

           
        }

        protected void SignIn(string username)
        {
            this.Request.Session.AddParameter("username", username);
        }

        protected void SignOut()
        {
            this.Request.Session.ClearParameters();
        }

        //protected void AuthorizeUser()
        //{
        //    //this.Model.Data["Guest"] = "none";
        //    //this.Model.Data["LoggedIn"] = "flex";
        //}


    }
}
