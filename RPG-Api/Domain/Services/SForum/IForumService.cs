﻿using RPG.Api.Domain.Models;
using RPG.Api.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.SForum
{
    public interface IForumService
    {
        Task<Forum> GetForumAsync(int forumId);
        Task<BaseResponse> AddForumAsync(Forum forum);
        Task<BaseResponse> EditForumAsync(Forum forum);
        Task<BaseResponse> DeleteForumAsync(int forumId);
    }
}
