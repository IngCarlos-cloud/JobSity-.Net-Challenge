using JobSityChallenge.Bussiness;
using JobSityChallenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobSityChallenge.Controllers
{
    public class ChatRoomController : Controller
    {
        List<ChatRoomModel.Messages> messageDetails = new List<ChatRoomModel.Messages>();
        // GET: ChatRoom
        public ActionResult ChatRoom()
        {
            
            ChatRoomBussiness bussiness = new ChatRoomBussiness();

            string jsonmessages = bussiness.RetriveMessages();
            
            messageDetails = JsonConvert.DeserializeObject<List<ChatRoomModel.Messages>>(jsonmessages);
            


            ViewData["messageList"] = messageDetails;
            return View();
        }

        public ActionResult postMessage(ChatRoomModel.Messages msg)
        {
            

            ChatRoomBussiness bussiness = new ChatRoomBussiness();
            bussiness._userId = TempData["userId"]?.ToString()??"0";
            bussiness._message = msg.Message;
            bussiness.PostMessage();
            TempData.Keep("userId");

            return RedirectToAction("ChatRoom", "ChatRoom");
        }
    }
}