using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CheckflixApp.Application.Common.FileStorage;

public class FileUploadCommand
{
    public string Name { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public string Data { get; set; } = default!;
}

public class FileUploadRequestValidator : AbstractValidator<FileUploadCommand>
{
    public FileUploadRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
                .WithMessage("Image Name cannot be empty!")
            .MaximumLength(150);

        RuleFor(p => p.Extension)
            .NotEmpty()
                .WithMessage("Image Extension cannot be empty!")
            .MaximumLength(5);

        RuleFor(p => p.Data)
            .NotEmpty()
                .WithMessage("Image Data cannot be empty!");
    }
}