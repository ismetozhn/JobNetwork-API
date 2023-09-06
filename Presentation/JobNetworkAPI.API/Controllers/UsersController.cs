
using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Application.ViewModels.JobPosts;
using JobNetworkAPI.Application.ViewModels.Users;
using JobNetworkAPI.Domain.Entities;
using JobNetworkAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobNetworkAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

     

        readonly private IUsersReadRepository _usersReadRepository;
        readonly private IUsersWriteRepository _usersWriteRepository;

        //public JobPostsController(IJobPostsWriteRepository jobPostsWriteRepository, IJobPostsReadRepository jobPostsReadRepository)
        //{
        //    _jobPostsReadRepository=jobPostsReadRepository;
        //    _jobPostsWriteRepository=jobPostsWriteRepository;
        //}


        public UsersController(IUsersReadRepository usersReadRepository, IUsersWriteRepository usersWriteRepository)
        {
            _usersReadRepository = usersReadRepository;
            _usersWriteRepository = usersWriteRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(_usersReadRepository.GetAll(false));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _usersReadRepository.GetByIdAsync(id,false));
        }


        [HttpPost]
        public async Task<ActionResult> Post(VM_Create_User model)
        {
            await _usersWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Surname = model.Surname,
                UserTypeId = model.UserTypeId,
                Email = model.Email,
                Password = model.Password,
                ContactNumber = model.ContactNumber,
                Cv = model.Cv
            });

            await _usersWriteRepository.SaveAsync();
            return Ok();
        }



        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_User model)
        {
            Users user = await _usersReadRepository.GetByIdAsync(model.Id);
            user.Surname = model.Surname;
            user.Name = model.Name;
            user.ContactNumber = model.ContactNumber;
            user.Email = model.Email;
            user.Cv = model.Cv;
            user.Password = model.Password;
            await _usersWriteRepository.SaveAsync();


            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usersWriteRepository.RemoveAsync(id);
            await _usersWriteRepository.SaveAsync();
            return Ok();
        }


    }
}
