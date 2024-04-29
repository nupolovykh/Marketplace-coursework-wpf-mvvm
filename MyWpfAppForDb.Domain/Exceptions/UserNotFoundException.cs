using System;

namespace MyWpfAppForDb.Domain.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public string UserName { get; set; }

		public UserNotFoundException(string username) 
		{
			UserName = username;
		}

		public UserNotFoundException(string message, string username) : base(message)
		{
			UserName = username;
		}

		public UserNotFoundException(string message, Exception exception, string username) : base(message, exception)
		{
			UserName = username;
		}

	}
}
