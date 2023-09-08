﻿using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Application.RequestParameters;
using JobNetworkAPI.Application.Services;
using JobNetworkAPI.Application.ViewModels.JobPosts;
using JobNetworkAPI.Application.ViewModels.Users;
using JobNetworkAPI.Domain.Entities;
using JobNetworkAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace JobNetworkAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostsController : ControllerBase
    {

        readonly private IJobPostsReadRepository _jobPostsReadRepository;
        readonly    private IJobPostsWriteRepository _jobPostsWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileService _fileService;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IJobPostImageFileReadRepository _jobPostImageFileReadRepository;
        readonly IJobPostImageFileWriteRepository _jobPostImageFileWriteRepository;
        readonly ICvFileWriteRepository _cvFileWriteRepository;
        readonly ICvFileReadRepository _cvFileReadRepository;

       

        public JobPostsController(IJobPostsReadRepository jobPostsReadRepository, IJobPostsWriteRepository jobPostsWriteRepository,
           IWebHostEnvironment webHostEnvironment, IFileService fileService, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IJobPostImageFileReadRepository jobPostImageFileReadRepository, IJobPostImageFileWriteRepository jobPostImageFileWriteRepository, ICvFileWriteRepository cvFileWriteRepository, ICvFileReadRepository cvFileReadRepository)
        {
            _jobPostsReadRepository = jobPostsReadRepository;
            _jobPostsWriteRepository = jobPostsWriteRepository;
            this._webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _jobPostImageFileReadRepository = jobPostImageFileReadRepository;
            _jobPostImageFileWriteRepository = jobPostImageFileWriteRepository;
            _cvFileWriteRepository = cvFileWriteRepository;
            _cvFileReadRepository = cvFileReadRepository;
        }

  

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {

            var totalCount = _jobPostsReadRepository.GetAll(false).Count();

            var jobPosts = _jobPostsReadRepository.GetAll().Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(j => new
            {
                j.Id,
                j.JobTypeId,
                j.CompanyName,
                j.Description,
                j.StartDate,
                j.EndDate,
                j.Title,
                j.ImagePath
            }).ToList();

            return Ok(new
            {
                jobPosts,
                totalCount
            });
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _jobPostsReadRepository.GetByIdAsync(id, false));
        }


        [HttpPost]
        public async Task<ActionResult> Post(VM_Create_JobPost model)
        {

           
            await _jobPostsWriteRepository.AddAsync(new JobPosts
            {
                Title = model.Title,
                CompanyName = model.CompanyName,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ImagePath = model.ImagePath,
                JobTypeId = model.JobTypeId


            }) ;

            await _jobPostsWriteRepository.SaveAsync();
            return Ok();
        }



        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_JobPost model)
        {
            JobPosts jobpost = await _jobPostsReadRepository.GetByIdAsync(model.Id);

            jobpost.Title = model.Title;
            jobpost.Description = model.Description;
            jobpost.StartDate = model.StartDate;
            jobpost.EndDate = model.EndDate;
            jobpost.CompanyName = model.CompanyName;
            jobpost.ImagePath = model.ImagePath;
            
            await _jobPostsWriteRepository.SaveAsync();


            return Ok();
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           
            await _jobPostsWriteRepository.RemoveAsync(id);
            await _jobPostsWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
       
        public async Task<IActionResult> Upload()
        {
             var datas= await _fileService.UploadAsync("resource/cv-files", Request.Form.Files);
            //await _jobPostImageFileWriteRepository.AddRangeAsync(datas.Select(d => new Domain.Entities.JobPostImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.path

            //}).ToList());
            //await _jobPostImageFileWriteRepository.SaveAsync();

            //await _fileService.UploadAsync("resource/jobpost-images", Request.Form.Files);

            await _cvFileWriteRepository.AddRangeAsync(datas.Select(d => new CvFile()
            {
                FileName = d.fileName,
                Path = d.path

            }).ToList());
            await _cvFileWriteRepository.SaveAsync();


            return Ok();
        }


    }
}

