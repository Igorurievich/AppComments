namespace App.Comments.Common.Helpers
{
	public static class ResponseMessagesHelper
	{
		private const string logInError = "Something wrong";
		public static string LogInError => logInError.ToJSON();

		private const string logInPassword = "Login or password incorrect";
		public static string LogInPassword => logInPassword.ToJSON();

		private const string signUpUserExist = "The user with current user name already exist";
		public static string SignUpUserExist => signUpUserExist.ToJSON();

		private const string signUpUserRegistered = "User registered";
		public static string SignUpUserRegistered => signUpUserRegistered.ToJSON();
	}
}
