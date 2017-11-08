using System;
using System.Collections.Generic;
using System.IO;
using App.Comments.Common.Interfaces.Services;
using System.Linq;
using System.Diagnostics;
using App.Comments.Common.Interfaces.Repositories;
using App.Comments.Common.Helpers;
using System.IO.Compression;
using SixLabors.ImageSharp;
using Newtonsoft.Json;
using App.Comments.Data;
using App.Comments.Common.Entities;

namespace App.Comments.Services
{
	public class TestsService : ITestsService
	{
		private readonly ICommentRepository _commentRepository;
		private readonly CommentsContext _commentsContext;

		public TestsService(ICommentRepository commentRepository, CommentsContext commentsContext)
		{
			_commentsContext = commentsContext;
			_commentRepository = commentRepository;
		}

		public (double, double, double) CountSQLQueriesGeneratingTime()
		{
			var userId = _commentsContext.Users.FirstOrDefault(x => x.Id > 0);

			var insertTime = Stopwatch.StartNew();
			for (int i = 0; i < 500; i++)
			{
				_commentRepository.AddComment(new Comment()
				{
					ApplicationUser = userId,
					CommentText = i.ToString(),
					PostTime = DateTime.Now,
					Title = i.ToString()
				});
			}
			insertTime.Stop();

			var selectTime = Stopwatch.StartNew();
			var comments = _commentRepository.GetAll();
			selectTime.Stop();

			var deleteTime = Stopwatch.StartNew();
			_commentRepository.DeleteAllComments(comments);
			deleteTime.Stop();


			return (TimeSpan.FromMilliseconds(insertTime.ElapsedMilliseconds).TotalSeconds,
					TimeSpan.FromMilliseconds(selectTime.ElapsedMilliseconds).TotalSeconds,
					TimeSpan.FromMilliseconds(deleteTime.ElapsedMilliseconds).TotalSeconds);
		}

		public double FindStringInText(string allText, string findingText)
		{
			var watch = Stopwatch.StartNew();
			string text = File.ReadAllText(@".\Media\Text.txt");
			text.LastIndexOf("Lorem");
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}

		public double ParseJsonObject(object objectForParsing)
		{
			List<Comment> comments = new List<Comment>();
			for (int i = 0; i < 500000; i++)
			{
				comments.Add(new Comment()
				{
					CommentText = i.ToString(),
					Id = i,
					PostTime = DateTime.Now,
					Title = i + i.ToString()
				}
				);
			}
			var watch = Stopwatch.StartNew();
			JsonConvert.SerializeObject(comments);
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}

		public double ResizeImage()
		{
			var watch = Stopwatch.StartNew();
			using (Image<Rgba32> image = Image.Load(@".\Media\Images\earth.jpg"))
			{
				image.Mutate(x => x
					 .Resize(image.Width / 2, image.Height / 2)
					 .Grayscale());
				image.Save(@".\Media\Images\ResizedImage\resizedImage.jpg");
			}
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}

		public double ZipFiles()
		{
			FilesHelper.CreateRandomFiles();
			FilesHelper.CleanResultDirectory();

			var watch = Stopwatch.StartNew();
			ZipFile.CreateFromDirectory(@".\Media\FilesForZip", @".\Media\ZipedFile\result.zip");
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}
	}
}
