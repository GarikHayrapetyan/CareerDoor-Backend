using Application.Interfaces;
using Application.Resumes;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Cloud;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Resumes
{
    public class ResumeAccessor : IResumeAccessor
    {
        private readonly Cloudinary _cloudinary;
        public ResumeAccessor(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ResumeUploadResult> AddResume(IFormFile file)
        {
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                return new ResumeUploadResult
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.ToString(),
                    FileName = uploadResult.OriginalFilename
                };
            };

            return null;
        }


        public async Task<string> DeleteResume(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok" ? result.Result : null;
        }

        

        
    }
}
