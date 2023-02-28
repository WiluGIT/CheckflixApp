using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckflixApp.Application.Identity.Common;
public class UserRoleDto
{
    public string? RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; }
}