using System;
using System.IO;

namespace App.Comments.Common.Helpers
{
    public static class FilesHelper
    {
		public static void CreateRandomFiles()
		{
			if (Directory.GetFiles(@".\Media\FilesForZip\").Length == 0)
			{
				for (int i = 0; i < 500; i++)
				{
					Random random = new Random();
					long randomNumber = random.Next(0, 1000);

					FileInfo fi = new FileInfo(@".\Media\FilesForZip\file" + i);
					if (!fi.Exists)
					{
						using (FileStream fs = fi.Create())
						{
							fs.SetLength(randomNumber);
						}
					}
				}
			}
		}

		public static void CleanResultDirectory()
		{
			DirectoryInfo di = new DirectoryInfo(@".\Media\ZipedFile\");

			foreach (FileInfo file in di.GetFiles())
			{
				file.Delete();
			}
			foreach (DirectoryInfo dir in di.GetDirectories())
			{
				dir.Delete(true);
			}
		}
	}
}
