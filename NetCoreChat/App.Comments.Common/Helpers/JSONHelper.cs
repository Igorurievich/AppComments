using Newtonsoft.Json;

namespace App.Comments.Common.Helpers
{
    public static class JSONHelper
    {
		public static string ToJSON(this object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
