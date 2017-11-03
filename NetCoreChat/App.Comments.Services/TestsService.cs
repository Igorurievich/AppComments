using System;
using System.Collections.Generic;
using System.IO;

namespace App.Comments.Services
{
	public class TestsService : TestsService
	{
		public double CountSQLQueriesGeneratingTime()
		{
			using (AdventureWorks adventureWorks = new AdventureWorks())
			{
				adventureWorks.Database.Log = log => Trace.Write(log);
				IQueryable<ProductCategory> source = adventureWorks.ProductCategories; // Define query.
				source.ForEach(); // Execute query.
			}
		}

		public double FindStringInText(string allText, string findingText)
		{
			throw new NotImplementedException();
		}

		public double ParseJsonFile(object objectForParsing)
		{
			throw new NotImplementedException();
		}

		public double ResizeImages(Stream image, uint count)
		{
			throw new NotImplementedException();
		}

		public double ZipFiles(IEnumerable<FileInfo> files)
		{
			throw new NotImplementedException();
		}
	}
}
