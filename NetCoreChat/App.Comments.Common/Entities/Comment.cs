using App.Comments.Common.Interfaces;
using System;

namespace App.Comments.Common.Entities
{
    public class Comment : IEntity
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public DateTime PostTime { get; set; }

    }
}
