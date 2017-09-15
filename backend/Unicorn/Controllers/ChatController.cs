using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Providers;
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
        [Route("{dialogId}/{ownerId}")]
        public async Task<HttpResponseMessage> Get(long dialogId, long ownerId)
        {
            try
            {
                var result = await _chatService.GetDialog(dialogId, ownerId);
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
        [HttpDelete]
        [Route("dialogs/{id}")]
        public async Task<HttpResponseMessage> DeleteDialog(int id)
        {
            try
            {
                await _chatService.RemoveDialog(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [Route("messages/{id}")]
        public async Task<HttpResponseMessage> DeleteMessage(long id)
        {
            try
            {
                await _chatService.RemoveMessage(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<HttpResponseMessage> UploadFileAsync()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/uploadedfiles");
            var provider = new CustomMultipartFormDataStreamProvider(root);
            List<ChatFileDTO> uploadedFiles = new List<ChatFileDTO>();

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {
                    string originalName = provider.GetOriginalName(file.Headers);
                    string serverName = Path.GetFileName(file.LocalFileName);

                    uploadedFiles.Add(new ChatFileDTO
                    {
                        OriginalName = originalName,
                        ServerPathName = serverName
                    });

                }
                return Request.CreateResponse(HttpStatusCode.OK, uploadedFiles);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
