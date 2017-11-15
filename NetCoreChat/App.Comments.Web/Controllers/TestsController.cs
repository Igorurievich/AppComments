﻿using App.Comments.Common.Interfaces.Services;
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
		public IEnumerable<double> CountSQLQueriesGeneratingTime()
		{
			var result = _testsService.CountSQLQueriesTime();
			return new List<double>()
			{
				result.Item1,
				result.Item2,
				result.Item3
			};
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
			return _testsService.ParseJsonObject();
		}

		[HttpGet]
		public double ApplyGausFilter()
		{
			return _testsService.ApplyGausBlur();
		}

		[HttpGet]
		public double ZipFiles()
		{
			return _testsService.ZipFiles();
		}
	}
}
