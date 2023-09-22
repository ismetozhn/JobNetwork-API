using JobNetworkAPI.Application.Repositories;
using JobNetworkAPI.Application.RequestParameters;
//using JobNetworkAPI.Application.Services;
using JobNetworkAPI.Application.ViewModels.JobPosts;
using JobNetworkAPI.Application.ViewModels.Users;
using JobNetworkAPI.Domain.Entities;
using JobNetworkAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using JobNetworkAPI.Application.Abstractions.Storage;
using MediatR;
using System.Net;
using JobNetworkAPI.Application.Features.Commands.JobPost.CreateJobPost;
using JobNetworkAPI.Application.Features.Queries.JobPost.GetAllJobPost;
using JobNetworkAPI.Application.Features.Queries.JobPost.GetByIdJobPost;
using JobNetworkAPI.Application.Features.Commands.JobPost.UpdateJobPost;
using JobNetworkAPI.Application.Features.Commands.JobPost.RemoveJobPost;
using JobNetworkAPI.Application.Features.Commands.JobPostImageFile.UploadJobPostImage;
using JobNetworkAPI.Application.Features.Commands.JobPostImageFile.RemoveJobPostImage;
using JobNetworkAPI.Application.Features.Queries.JobPostImageFile.GetJobPostImages;
using Microsoft.AspNetCore.Authorization;
using JobNetworkAPI.Application.Features.Commands.JobPostImageFile.ChangeShowCaseImage;
using JobNetworkAPI.Application.Consts;
using JobNetworkAPI.Application.Enums;
using JobNetworkAPI.Application.CustomAttributes;

namespace JobNetworkAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class JobPostsController : ControllerBase
    {
        readonly IMediator _mediator;


        public JobPostsController(IMediator mediator)
        {
        
            _mediator = mediator;
        }



        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllJobPostQueryRequest getAllJobPostQueryRequest)
        {
            
            GetAllJobPostQueryResponse response = await _mediator.Send(getAllJobPostQueryRequest);
            return Ok(response);

           
        }


        [HttpGet("{Id}")]

        public async Task<IActionResult> Get([FromRoute]GetByIdJobPostQueryRequest getByIdJobPostQueryRequest)
        {
           GetByIdJobPostQueryResponse response = await _mediator.Send(getByIdJobPostQueryRequest);
            return Ok(response);
        }




        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.JobPosts, ActionType = ActionType.Writing, Definition = "Create JobPost")]
        public async Task<ActionResult> Post(CreateJobPostCommandRequest createJobPostCommandRequest)
        {
            CreateJobPostCommandResponse response = await _mediator.Send(createJobPostCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);

           

        }



        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.JobPosts, ActionType = ActionType.Updating, Definition = "Update JobPost")]
        public async Task<IActionResult> Put([FromBody]UpdateJobPostCommandRequest updateJobPostCommandRequest)
        {
            UpdateJobPostCommandResponse response = await _mediator.Send(updateJobPostCommandRequest);
            return Ok();
        }




        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.JobPosts, ActionType = ActionType.Deleting, Definition = "Delete JobPost")]
        public async Task<IActionResult> Delete([FromRoute]RemoveJobPostCommandRequest removeJobPostCommandRequest )
        {

            RemoveJobPostCommandResponse response = await _mediator.Send(removeJobPostCommandRequest);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.JobPosts, ActionType = ActionType.Writing, Definition = "Upload JobPost File")]
        public async Task<IActionResult> Upload([FromQuery]UploadJobPostImageCommandRequest uploadJobPostImageCommandRequest)
        {
            uploadJobPostImageCommandRequest.Files = Request.Form.Files;
            UploadJobPostImageCommandResponse response = await _mediator.Send(uploadJobPostImageCommandRequest);
            return Ok();



            //storage yapısı ile ekleme
            //var datas = await _storageService.UploadAsync("files", Request.Form.Files);
            ////var datas = await _fileService.UploadAsync("resource/files", Request.Form.Files);
            //await _cvFileWriteRepository.AddRangeAsync(datas.Select(d => new CvFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.pathOrContainerName,
            //    Storage = _storageService.StorageName
            //}).ToList());
            //await _cvFileWriteRepository.SaveAsync();

            //await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //    Price = new Random().Next()
            //}).ToList());
            //await _invoiceFileWriteRepository.SaveAsync();

            //await _fileWriteRepository.AddRangeAsync(datas.Select(d => new ETicaretAPI.Domain.Entities.File()
            //{
            //    FileName = d.fileName,
            //    Path = d.path
            //}).ToList());
            //await _fileWriteRepository.SaveAsync();

            //var d1 = _fileReadRepository.GetAll(false);
            //var d2 = _invoiceFileReadRepository.GetAll(false);
            //var d3 = _productImageFileReadRepository.GetAll(false);

        }

         [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.JobPosts, ActionType = ActionType.Reading, Definition = "Get JobPosts Image")]
        public async Task<IActionResult> GetImages([FromRoute]GetJobPostImagesQueryRequest getJobPostImagesQueryRequest)
        {
            List<GetJobPostImagesQueryResponse> response = await _mediator.Send(getJobPostImagesQueryRequest);
            return Ok(response);

        }

        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.JobPosts, ActionType = ActionType.Deleting, Definition = "Delete JobPost Image")]

        public async Task<IActionResult> DeleteJobPostImage([FromRoute]RemoveJobPostImageCommandRequest removeJobPostImageCommandRequest, [FromQuery] string imageId)
        {
            removeJobPostImageCommandRequest.ImageId = imageId;
            RemoveJobPostImageCommandResponse response = await _mediator.Send(removeJobPostImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.JobPosts, ActionType = ActionType.Updating, Definition = "Change Showcase Image")]
        public async Task<IActionResult> ChangeShowCaseImage([FromQuery] ChangeShowCaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            ChangeShowCaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);
            return Ok(response);
        }
    }
}