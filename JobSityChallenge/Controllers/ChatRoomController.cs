using JobSityChallenge.Bussiness;
using JobSityChallenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace JobSityChallenge.Controllers
{
    public class ChatRoomController : Controller
    {
        List<ChatRoomModel.Messages> messageList = new List<ChatRoomModel.Messages>();
        ChatRoomModel.Messages botMessageResponse;
        ChatRoomBussiness bussinessChat;
        // GET: ChatRoom
        public ActionResult ChatRoom(ChatRoomModel.Messages msg)
        {
            bussinessChat = new ChatRoomBussiness();
            if (!string.IsNullOrEmpty(msg.Message))
            {
                bussinessChat._userId = TempData["userId"]?.ToString() ?? "0";
                bussinessChat._message = msg.Message;
                bussinessChat.PostMessage();
                if (bussinessChat.isCommand)
                {
                    botMessageResponse = JsonConvert.DeserializeObject<ChatRoomModel.Messages>(bussinessChat.bootMessage);
                }
                TempData.Keep("userId");

            }

            string jsonmessages = bussinessChat.RetriveMessages();

            if (botMessageResponse != null)
                messageList.Add(botMessageResponse);

            messageList.AddRange(JsonConvert.DeserializeObject<List<ChatRoomModel.Messages>>(jsonmessages));
            
            

            ViewData["messageList"] = messageList;
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