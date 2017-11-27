using App.Comments.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Comments.Common.Entities
{
	public class CommentData : IEntity
	{
		[Key, ForeignKey("Comment")]
		public int CommentDataId { get; set; }

		public string CommentDescription { get; set; }

		public int Likes { get; set; }

		public int Dislikes { get; set; }

		public Comment Comment { get; set; }
	}
}
