namespace App.Comments.Common.Interfaces.Services
{
    public interface ITestsService
    {
		double ApplyGausBlur();
		double FindStringInText(string allText, string findingText);
		double ZipFiles();
		double ParseJsonObject();
		(double, double, double) CountSQLQueriesTime();
	}
}
