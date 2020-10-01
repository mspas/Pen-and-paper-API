using RPG.Api.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Communication
{
    public class MessageForumPaginatedResponse : SearchResponse
    {
        public MessageForumPaginatedResponse(List<MessageForumResource> messagesResult, int count, int maxPages, string previousPage, string nextPage) : base(count, maxPages, previousPage, nextPage)
        {
            this.messagesResult = messagesResult;
        }

        public List<MessageForumResource> messagesResult { get; set; }
    }
}
