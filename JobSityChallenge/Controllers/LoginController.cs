using JobSityChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobSityChallenge.DataAccess;
using System.Security.Cryptography;

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
                return RedirectToAction("Register");
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Login.User user)
        {

            DataAccess.User loginUser = new User();
            loginUser.UserName = user.UserName;
            loginUser.Password = user.Pwd;
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Pwd))
                return Content("User Name and Password can not be empty");

            bool valid = loginUser.CreateUser();
            if (valid)
            {
                TempData["userId"] = loginUser.UserId;
                return RedirectToAction("ChatRoom", "ChatRoom");
            }
            else
            {
                return Content("Unable to create a new User");
            }                

        }
    }
}