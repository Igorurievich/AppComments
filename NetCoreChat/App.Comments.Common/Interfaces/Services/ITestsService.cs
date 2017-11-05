using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App.Comments.Common.Interfaces.Services
{
    public interface ITestsService
    {
		double ResizeImages(Stream image, uint count);
		double FindStringInText(string allText, string findingText);
		double ZipFiles();
		double ParseJsonObject(object objectForParsing);
		double CountSQLQueriesGeneratingTime();
	}
}
