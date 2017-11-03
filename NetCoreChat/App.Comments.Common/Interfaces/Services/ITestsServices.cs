using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App.Comments.Common.Interfaces.Services
{
    interface ITestsServices
    {
		double ResizeImages(Stream image, uint count);
		double FindStringInText(string allText, string findingText);
		double ZipFiles(IEnumerable<FileInfo> files);
		double ParseJsonFile(object objectForParsing);
		double CountSQLQueriesGeneratingTime();
	}
}
