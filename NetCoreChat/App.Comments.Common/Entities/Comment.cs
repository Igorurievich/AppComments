using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Comments.Common.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ApplicationUser Autor { get; set; }

        public short AutorId { get; set; }

        public DateTime PostTime { get; set; }
    }
}
