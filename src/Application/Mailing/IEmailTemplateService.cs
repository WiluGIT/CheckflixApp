using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckflixApp.Application.Mailing;
public interface IEmailTemplateService
{
    string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel);
}