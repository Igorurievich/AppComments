using System;
using System.IO;

namespace App.Comments.Common.Helpers
{
	public static class FilesHelper
	{
		public static void CreateRandomFiles(bool isLinux)
		{
			string path = isLinux ? "Media/FilesForZip/" : ".\\Media\\FilesForZip\\";
			string filePath = isLinux ? "Media/FilesForZip/file" : ".\\Media\\FilesForZip\\file";

			if (Directory.GetFiles(path).Length == 0)
			{
				for (int i = 0; i < 500; i++)
				{
					Random random = new Random();
					long randomNumber = random.Next(0, 1000);

					FileInfo fi = new FileInfo(filePath + i);
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

		public static void CleanResultDirectory(bool isLinux)
		{
			string path = isLinux ? "Media/ZipedFile/" : ".\\Media\\ZipedFile\\";
			DirectoryInfo di = new DirectoryInfo(path);

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
