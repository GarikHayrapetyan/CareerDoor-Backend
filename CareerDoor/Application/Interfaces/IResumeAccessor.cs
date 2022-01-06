using Application.Resumes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IResumeAccessor
    {
        Task<ResumeUploadResult> AddResume(IFormFile file);
        Task<string> DeleteResume(string publicId);
    }
}
