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
using System.Runtime.InteropServices;

namespace App.Comments.Services
{
	public class TestsService : ITestsService
	{
		private readonly ICommentRepository _commentRepository;
		private readonly CommentsContext _commentsContext;

		bool isLinux = false;

		public TestsService(ICommentRepository commentRepository, CommentsContext commentsContext)
		{
			_commentsContext = commentsContext;
			_commentRepository = commentRepository;

			isLinux = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
		}

		public (double, double, double) CountSQLQueriesTime()
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
			string path;
			path = isLinux ? "Media/Text.txt" : ".\\Media\\Text.txt";
			var watch = Stopwatch.StartNew();
			string text = File.ReadAllText(path);
			text.LastIndexOf("Lorem");
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}

		public double ParseJsonObject()
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
			string pathToImage;
			string pathToSavedImage;
			pathToImage = isLinux ? "Media/Images/earth.jpg" : ".\\Media\\Images\\earth.jpg";
			pathToSavedImage = isLinux ? "Media/Images/ResizedImage/resizedImage.jpg" : ".\\Media\\Images\\ResizedImage\\resizedImage.jpg";
			var watch = Stopwatch.StartNew();
			using (Image<Rgba32> image = Image.Load(pathToImage))
			{
				image.Mutate(x => x
					 .Resize(image.Width / 2, image.Height / 2)
					 .Grayscale());
				image.Save(pathToSavedImage);
			}
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}

		public double ZipFiles()
		{
			string pathFilesForZip;
			string pathToResultZip;

			pathFilesForZip = isLinux ? "Media/FilesForZip" : ".\\Media\\FilesForZip";
			pathToResultZip = isLinux ? "Media/ZipedFile/result.zip" : ".\\Media\\ZipedFile\\result.zip";

			FilesHelper.CreateRandomFiles(isLinux);
			FilesHelper.CleanResultDirectory(isLinux);

			var watch = Stopwatch.StartNew();
			ZipFile.CreateFromDirectory(pathFilesForZip, pathToResultZip);
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}
	}
}