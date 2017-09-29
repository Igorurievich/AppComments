using System;

namespace App.Comments.Common.Entities
{
    public class Comment
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public DateTime PostTime { get; set; }

    }
}
