using App.Comments.Data;
using System;
using System.Collections.Generic;
using System.IO;
using App.Comments.Common.Interfaces.Services;
using System.Linq;
using System.Diagnostics;
using App.Comments.Common.Entities;
using App.Comments.Common.Interfaces.Repositories;
using App.Comments.Common.Helpers;
using System.IO.Compression;

namespace App.Comments.Services
{
	public class TestsService : ITestsService
	{
		private readonly ICommentRepository _commentRepository;
		public TestsService(ICommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
		}
		public double CountSQLQueriesGeneratingTime()
		{
			var watch = Stopwatch.StartNew();

			_commentRepository.GetAll().AsQueryable();

			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}

		public double FindStringInText(string allText, string findingText)
		{
			var watch = Stopwatch.StartNew();
			string text = File.ReadAllText(@".\Media\Text.txt");
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}

		public double ParseJsonObject(object objectForParsing)
		{
			List<FileInfo> files = new List<FileInfo>();
			for (int i = 0; i < 500; i++)
			{
				files.Add(new FileInfo(i.ToString()));
			}
			var watch = Stopwatch.StartNew();
			files.ToJSON();
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}

		public double ResizeImages(Stream image, uint count)
		{
			var watch = Stopwatch.StartNew();

			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}

		public double ZipFiles()
		{
			FilesHelper.CreateFilesRandomFiles();
			FilesHelper.CleanResultDirectory();

			var watch = Stopwatch.StartNew();
			ZipFile.CreateFromDirectory(@".\Media\FilesForZip", @".\Media\ZipedFile\result.zip");
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}
	}
}
