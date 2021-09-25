﻿using RPG.Api.Domain.Models;
using RPG.Api.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Api.Domain.Services.Profile
{
    public interface INotificationService
    {
        Task<NotificationData> GetNotificationDataAsync(int userId);
        Task<BaseResponse> UpdateNotificationDataAsync(int id, NotificationData notificationData);
    }
}
