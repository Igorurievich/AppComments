using App.Comments.Common.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace App.Comments.Web.Controllers
{
	[Route("api/[controller]/[action]")]
	public class TestsController : Controller
    {
		private readonly ITestsService _testsService;
		public TestsController(ITestsService testsService)
		{
			_testsService = testsService;
		}

		[HttpGet]
		public double CountSQLQueriesGeneratingTime()
		{
			return _testsService.CountSQLQueriesGeneratingTime();
		}

		[HttpGet]
		public double FindStringInText()
		{
			string text = "";
			string findedText = "";
			return _testsService.FindStringInText(text, findedText);
		}

		[HttpGet]
		public double ParseJsonObject()
		{
			string testString = "test string";
			return _testsService.ParseJsonObject(testString);
		}

		[HttpGet]
		public double ResizeImages()
		{
			MemoryStream image = new MemoryStream();
			uint count = 5;
			return _testsService.ResizeImages(image, count);
		}

		[HttpGet]
		public double ZipFiles()
		{
			return _testsService.ZipFiles();
		}
	}
}
