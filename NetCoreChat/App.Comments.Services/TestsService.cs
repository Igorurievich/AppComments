﻿using System;
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

			isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
		}

		public double CountSQLQueriesTime()
		{
			_commentRepository.DeleteAllComments();
			var userId = _commentsContext.Users.FirstOrDefault(x => x.UserId > 0);
			var insertTime = Stopwatch.StartNew();

			Comment comment = null;
			List<Comment> comments = new List<Comment>();
			for (int i = 0; i < 50000; i++)
			{
				comment = new Comment()
				{
					ApplicationUser = userId,
					CommentText = $"This is test comment with number {i}",
					PostTime = DateTime.Now,
					Title = $"This is test Title with number {i}",
				};
				comments.Add(comment);
				comment = null;
			}
			_commentRepository.AddComments(comments);
			insertTime.Stop();

			var selectTime = Stopwatch.StartNew();
			var selectedComments = _commentRepository.GetAll();
			selectTime.Stop();

			var deleteTime = Stopwatch.StartNew();
			_commentRepository.DeleteComments(comments);
			deleteTime.Stop();

			return TimeSpan.FromMilliseconds(selectTime.ElapsedMilliseconds).TotalSeconds;
		}

		public double FindStringInText()
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
			for (int i = 0; i < 10000; i++)
			{
				comments.Add(new Comment()
				{
					CommentText = i.ToString(),
					CommentId = i,
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

		public (double, double) ApplyGausBlur()
		{
			string pathToBigImage;
			string pathToBigSavedImage;
			pathToBigImage = isLinux ? "Media/Images/earth.jpg" : ".\\Media\\Images\\earth.jpg";
			pathToBigSavedImage = isLinux ? "Media/Images/FilteredImage/filteredEarth.jpg" : ".\\Media\\Images\\FilteredImage\\filteredEarth.jpg";

			string pathToLittleImage;
			string pathToLittleSavedImage;
			pathToLittleImage = isLinux ? "Media/Images/california.jpg" : ".\\Media\\Images\\california.jpg";
			pathToLittleSavedImage = isLinux ? "Media/Images/FilteredImage/filteredCalifornnia.jpg" : ".\\Media\\Images\\FilteredImage\\filteredCalifornnia.jpg";

			var bigImageTime = Stopwatch.StartNew();
			using (Image<Rgba32> image = Image.Load(pathToBigImage))
			{
				image.Mutate(x => x.GaussianBlur(10));
				image.Save(pathToBigSavedImage);
			}
			bigImageTime.Stop();

			var littleImageTime = Stopwatch.StartNew();
			using (Image<Rgba32> littleimage = Image.Load(pathToLittleImage))
			{
				littleimage.Mutate(x => x.GaussianBlur(10));
				littleimage.Save(pathToLittleSavedImage);
			}
			littleImageTime.Stop();
			return (TimeSpan.FromMilliseconds(bigImageTime.ElapsedMilliseconds).TotalSeconds,
				   TimeSpan.FromMilliseconds(littleImageTime.ElapsedMilliseconds).TotalSeconds);
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

		public (double, double) ResizeImagesTests()
		{
			string pathToBigImage;
			string pathToBigSavedImage;
			pathToBigImage = isLinux ? "Media/Images/earth.jpg" : ".\\Media\\Images\\earth.jpg";
			pathToBigSavedImage = isLinux ? "Media/Images/ResizedImages/filteredEarth.jpg" : ".\\Media\\Images\\ResizedImages\\filteredEarth.jpg";

			string pathToLittleImage;
			string pathToLittleSavedImage;
			pathToLittleImage = isLinux ? "Media/Images/california.jpg" : ".\\Media\\Images\\california.jpg";
			pathToLittleSavedImage = isLinux ? "Media/Images/ResizedImages/filteredCalifornnia.jpg" : ".\\Media\\Images\\ResizedImages\\filteredCalifornnia.jpg";

			var bigImageTime = Stopwatch.StartNew();
			using (Image<Rgba32> image = Image.Load(pathToBigImage))
			{
				image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
				image.Save(pathToBigSavedImage);
			}
			bigImageTime.Stop();

			var littleImageTime = Stopwatch.StartNew();
			using (Image<Rgba32> littleimage = Image.Load(pathToLittleImage))
			{
				littleimage.Mutate(x => x.Resize(littleimage.Width / 2, littleimage.Height / 2));
				littleimage.Save(pathToLittleSavedImage);
			}
			littleImageTime.Stop();
			return (TimeSpan.FromMilliseconds(bigImageTime.ElapsedMilliseconds).TotalSeconds,
				   TimeSpan.FromMilliseconds(littleImageTime.ElapsedMilliseconds).TotalSeconds);
		}
	}
}