using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ChatController : ApiController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [Route("{id}/history")]
        public HttpResponseMessage GetMessages(long senderId, long receiverId)
        {
            var chatHistory = _chatService.GetChat(senderId, receiverId);

            if (chatHistory == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, chatHistory);
            }
        }

        [Route("{id}/history/date")]
        public HttpResponseMessage GetMessagesByDate(long senderId, long receiverId, DateTime dateMin)
        {
            var chatHistory = _chatService.GetChatByDate(senderId, receiverId, dateMin);

            if (chatHistory == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, chatHistory);
            }
        }

        [Route("{id}/send")]
        [HttpPost]
        public async Task<HttpResponseMessage> SendMessage(ChatDTO msg)
        {
            try
            {
                await _chatService.Create(msg);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
