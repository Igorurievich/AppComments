using App.Comments.Common.Entities;
using System;

public class CommentDto {

        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public DateTime PostTime { get; set; }

    public CommentDto()
    {
        
    }
}