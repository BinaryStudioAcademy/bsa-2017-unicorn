using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Chat;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("chat")]
    public class ChatController : ApiController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            try
            {
                var result = await _chatService.GetDialog(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("dialogs/{id}")]
        public async Task<HttpResponseMessage> GetAllDialogs(int id)
        {
            try
            {
                var result = await _chatService.GetAllDialogs(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("dialog/find/{participantOneId}/{participantTwoId}")]
        public async Task<HttpResponseMessage> FindDialog(int participantOneId, int participantTwoId)
        {
            try
            {
                var result = await _chatService.FindDialog(participantOneId, participantTwoId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("dialog")]
        public async Task<HttpResponseMessage> CreateDialog(ChatDialogDTO dialog)
        {
            try
            {
                var createdDialog = await _chatService.CreateDialog(dialog);
                return Request.CreateResponse(HttpStatusCode.OK, createdDialog);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("messages/update/{dialogId}")]
        public async Task<HttpResponseMessage> ReadNotReadedMessages(long dialogId, [FromBody] long ownerId)
        {
            try
            {
                await _chatService.UpdateNotReadedMessage(dialogId, ownerId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("send")]
        public async Task<HttpResponseMessage> SendMessage(ChatMessageDTO msg)
        {
            try
            {
                await _chatService.CreateMessage(msg);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
