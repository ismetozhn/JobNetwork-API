using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Application.RequestParameters;
using JobNetworkAPI.Application.ViewModels.JobPosts;
using JobNetworkAPI.Application.ViewModels.Users;
using JobNetworkAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobNetworkAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostsController : ControllerBase
    {

        readonly private IJobPostsReadRepository _jobPostsReadRepository;
        readonly    private IJobPostsWriteRepository _jobPostsWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public JobPostsController(IJobPostsReadRepository jobPostsReadRepository,IJobPostsWriteRepository jobPostsWriteRepository,
           IWebHostEnvironment webHostEnvironment )
        {
            _jobPostsReadRepository = jobPostsReadRepository;
            _jobPostsWriteRepository = jobPostsWriteRepository;
            this._webHostEnvironment = webHostEnvironment;
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
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,"resource/jobpost-images");

            if(!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            Random r = new();

            foreach(IFormFile file in Request.Form.Files)
            {
               string fullPath=Path.Combine(uploadPath,$"{r.Next()}{Path.GetExtension(file.FileName)}");


                using FileStream fileStream = new(fullPath,FileMode.Create,FileAccess.Write,FileShare.None,1024*1024,useAsync:false);
                await file.CopyToAsync(fileStream);
               await  fileStream.FlushAsync();
            }

            return Ok();
        }


    }
}

