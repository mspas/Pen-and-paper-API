using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using RPG.Api.Domain.Services;
using RPG.Api.Domain.Repositories.Profile;
using System.IO;
using RPG.Api.Domain.Services.Communication;
using RPG.Api.Domain.Models;
using RPG.Api.Domain.Repositories;
using RPG.Api.Domain.Repositories.RGame;
using RPG.Api.Domain.Repositories.RForum;
using System.Collections.Generic;
using RPG.Api.Domain.Services.Profile;

namespace RPG.Api.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPersonalDataRepository _personalDataRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMessageForumRepository _messageForumRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IPersonalDataService _personalDataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _host;

        public PhotoService(IPersonalDataRepository personalDataRepository, IGameRepository gameRepository, IMessageForumRepository messageForumRepository, IMessageRepository messageRepository, IPhotoRepository photoRepository, IPersonalDataService personalDataService, IUnitOfWork unitOfWork, IHostingEnvironment host)
        {
            _personalDataRepository = personalDataRepository;
            _gameRepository = gameRepository;
            _messageForumRepository = messageForumRepository;
            _messageRepository = messageRepository;
            _photoRepository = photoRepository;
            _personalDataService = personalDataService;
            _unitOfWork = unitOfWork;
            _host = host;
        }

        public async Task<List<Photo>> GetPhotoListAsync(int sourceId)
        {
            return await _photoRepository.GetPhotoListAsync(sourceId);
        }

        public async Task<BaseResponse> UploadProfilePhotoAsync(int id, bool isBgPhoto, IFormFile file)
        {
            var profile = await _personalDataRepository.GetProfileById(id);
            if (profile == null)
                return new BaseResponse(false, "Account not found.");

            if (profile.photoName != "unknown.png" || profile.photoName != "" || profile.photoName != null)
            {
                var res = await DeletePhotoAsync(1, id, profile.photoName);
                if (!res.Success)
                {
                    return new BaseResponse(false, "Deleting current profile photo erorr!");
                }
            }

            var uploadFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName, sourceId = id };

            if (isBgPhoto)
            {
                profile.BackgroundPhoto = photo;
                profile.bgPhotoName = fileName;
            }
            else
            {
                profile.ProfilePhoto = photo;
                profile.photoName = fileName;
                profile.isPhotoUploaded = true;
            }

            var response = await _personalDataRepository.UpdateProfile(profile);

            await _unitOfWork.CompleteAsync();

            return new BaseResponse(true, fileName);
        }

        public async Task<BaseResponse> UploadGamePhotoAsync(int id, bool isBgPhoto, IFormFile file)
        {
            var game = await _gameRepository.GetGameAsync(id);
            if (game == null)
                return new BaseResponse(false, "Game not found.");

            var uploadFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };

            if (isBgPhoto)
            {
                game.BackgroundPhoto = photo;
                game.bgPhotoName = fileName;
            }
            else
            {
                game.ProfilePhoto = photo;
                game.photoName = fileName;
            }

            var response = _gameRepository.EditGame(game);
            
            await _unitOfWork.CompleteAsync();

            return new BaseResponse(true, fileName);
        }

        public async Task<BaseResponse> UploadPostPhotoAsync(int id, IFormFile file)
        {
            var uploadFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new BaseResponse(true, fileName);
        }

        public async Task<BaseResponse> UploadMessagePhotoAsync(int id, IFormFile file)
        {
            var msg = await _messageRepository.GetMessageAsync(id);
            if (msg == null)
                return new BaseResponse(false, "Message not found.");

            var uploadFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var filePath = Path.Combine(uploadFolderPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = file.FileName, sourceId = id };

            await _photoRepository.AddPhotoAsync(photo);
            await _unitOfWork.CompleteAsync();

            return new BaseResponse(true, null);
        }

        public async Task<BaseResponse> DeletePhotoAsync(int photoType, int ownerId, string fileName)
        {
            BaseResponse response = null;
            switch (photoType)
            {
                case 1:
                    var profile = await _personalDataRepository.GetProfileById(ownerId);
                    profile.photoName = "unknown.png";
                    response = await _personalDataService.EditProfileDataAsync(ownerId, profile);
                    if (response.Success)
                    {
                        var photo = await _photoRepository.GetPhotobyNameAsync(fileName);
                        response = _photoRepository.DeletePhoto(photo);
                    }
                    break;
                case 2:
                    return new BaseResponse(false, "Not implemented");
                case 3:
                    break;
                default:
                    return new BaseResponse(false, "Delete photo type error.");
            }

            var uploadFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
                return new BaseResponse(false, "Uploads directory not found!");

            var filePath = Path.Combine(uploadFolderPath, fileName);

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return new BaseResponse(true, "File deleted!");
                }
                else
                    return new BaseResponse(false, "File not found!");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
                return new BaseResponse(false, ioExp.Message);
            }
        }
        
        public string GetMimeType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".docx": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".csv": return "text/csv";
                default: return "";
            }
        }
    }
}
