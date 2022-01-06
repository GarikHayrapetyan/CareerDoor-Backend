using Application.Interfaces;
using Application.Resumes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Resumes
{
    public class ResumeAccessor : IResumeAccessor
    {
        public Task<ResumeUploadResult> AddResume(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteResume(string publicId)
        {
            throw new NotImplementedException();
        }
    }
}
