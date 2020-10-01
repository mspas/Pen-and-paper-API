using RPG.Api.Domain.Models;
using RPG.Api.Domain.Services.Communication;
using RPG.Api.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.SForum
{
    public interface IMessageForumService
    {
        Task<MessageForum> GetMessageAsync(int messageId);
        Task<MessageForumPaginatedResponse> GetMessageListAsync(int topicId, int pageNumber, int pageSize);
        Task<MessageForumResponse> AddMessageAsync(MessageForum message);
        Task<MessageForumResponse> EditMessageAsync(MessageForum message);
        Task<BaseResponse> DeleteMessageAsync(int messageId);
    }
}
