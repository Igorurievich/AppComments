using App.Comments.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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
			return _testsService.CountSQLQueriesTime();
		}

		[HttpGet]
		public double FindStringInText()
		{
			return _testsService.FindStringInText();
		}

		[HttpGet]
		public double ParseJsonObject()
		{
			return _testsService.ParseJsonObject();
		}

		[HttpGet]
		public string ResizeImagesTests()
		{
			return JsonConvert.SerializeObject(_testsService.ResizeImagesTests());
		}

		[HttpGet]
		public string RunGausTests()
		{
			return JsonConvert.SerializeObject(_testsService.ApplyGausBlur());
		}

		[HttpGet]
		public double ZipFiles()
		{
			return _testsService.ZipFiles();
		}
	}
}
