using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace App.Comments.Common.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<Comment> Comments { get; set; }

        public byte[] UserPhoto { get; set; }
    }
}
