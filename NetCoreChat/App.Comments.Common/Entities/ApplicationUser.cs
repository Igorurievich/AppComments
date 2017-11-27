using App.Comments.Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Comments.Common.Entities
{
    public class ApplicationUser : IEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public byte[] UserPhoto { get; set; }
    }
}
