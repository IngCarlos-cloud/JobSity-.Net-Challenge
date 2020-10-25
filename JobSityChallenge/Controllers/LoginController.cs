using JobSityChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSityChallenge.DataAccess;

namespace JobSityChallenge.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult SignIn(Login.User user)
        {

            DataAccess.User loginUser = new User();
            loginUser.UserName = user.UserName;
            loginUser.Password = user.Pwd;
            bool valid = loginUser.AuthenticateUSer();
            if (valid)
            {
                TempData["userId"] = loginUser.UserId;
                return RedirectToAction("ChatRoom", "ChatRoom");
            }
            else                
            return Content($"{user.UserName}  is an Invalid user");
        }
    }
}