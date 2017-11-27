using App.Comments.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Comments.Common.Entities
{
	public class Comment : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CommentId { get; set; }

		public string Title { get; set; }

		public string CommentText { get; set; }

		public ApplicationUser ApplicationUser { get; set; }

		public DateTime PostTime { get; set; }

		public CommentData CommentData { get; set; }
	}
}
