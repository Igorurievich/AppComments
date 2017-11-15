namespace App.Comments.Common.Interfaces.Services
{
    public interface ITestsService
    {
		(double, double) ApplyGausBlur();
		double FindStringInText();
		double ZipFiles();
		double ParseJsonObject();
		(double, double, double) CountSQLQueriesTime();
		(double, double) ResizeImagesTests();
	}
}
