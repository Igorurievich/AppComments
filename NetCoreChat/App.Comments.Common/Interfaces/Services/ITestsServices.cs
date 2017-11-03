using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Comments.Common.Interfaces.Services
{
    interface ITestsServices
    {
		double ResizeImages(Image image, uint count);
		double FindStringInText(string allText, string findingText);
		

	}
}
