using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckflixApp.Domain.Entities;
public class FollowedPeople : BaseAuditableEntity
{
    public string ObserverId { get; set; }
    public string TargetId { get; set; }
}