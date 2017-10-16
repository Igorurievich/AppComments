using App.Comments.Common.Entities;
using System;

public class CommentDto {

        public int Id { get; set; }

        public string Title { get; set; }

        public string CommentText { get; set; }

        public string Autor { get; set; }

        public DateTime PostTime { get; set; }
}