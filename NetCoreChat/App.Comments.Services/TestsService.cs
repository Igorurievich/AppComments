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
			text.LastIndexOf("Lorem");
			watch.Stop();
			return TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).TotalSeconds;
		}

		public double ParseJsonObject(object objectForParsing)
		{
			List<string> files = new List<string>();
			for (int i = 0; i < 500; i++)
			{
				files.Add(i.ToString());
			}
			var watch = Stopwatch.StartNew();
			JsonConvert.SerializeObject(files);
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
