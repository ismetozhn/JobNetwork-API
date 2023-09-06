using FluentValidation;
using JobNetworkAPI.Application.ViewModels.JobPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JobNetworkAPI.Application.Validators.JobPosts
{
    public class CreateJobPostValidator : AbstractValidator<VM_Create_JobPost>
    {
        public CreateJobPostValidator() {


            RuleFor(j => j.Title)
                .NotEmpty()
                .NotNull()
                   .WithMessage("Lütfen başlık kısmını boş bırakmayınız.")
                .MaximumLength(200)
                .MinimumLength(5)
                    .WithMessage("Lütfen başlığı 5 ile 200 karakter arasında girin.");


            RuleFor(j => j.CompanyName)
                .NotEmpty()
                .NotNull()
                   .WithMessage("Lütfen şirket kısmını boş bırakmayınız.")
                .MaximumLength(500)
                .MinimumLength(3)
                    .WithMessage("Lütfen şirket adını 3 ile 500 karakter arasında girin.");

            RuleFor(j => j.Description)
                .NotEmpty()
                .NotNull()
                   .WithMessage("Lütfen açıklama kısmını boş bırakmayınız.");

            RuleFor(j => j.StartDate)
                .NotEmpty()
                .NotNull()
                   .WithMessage("Lütfen başlangıç tarihini kısmını boş bırakmayınız.");
            RuleFor(j => j.EndDate)
                .NotEmpty()
                .NotNull()
                   .WithMessage("Lütfen bitiş tarihini kısmını boş bırakmayınız.");
            RuleFor(j => j.ImagePath)
               .NotEmpty()
               .NotNull()
                  .WithMessage("Lütfen görsel  kısmını boş bırakmayınız.");



        }
    }
}
