using System;

namespace MyWpfAppForDb.Domain.Exceptions
{
	/// <summary>
	/// Thrown when a login attempt supplies the wrong password. The attempted
	/// password is deliberately not carried on the exception: it would end up in
	/// logs and crash dumps in plain text.
	/// </summary>
	public class InvalidPasswordException : Exception
	{
		public string Username { get; }

		public InvalidPasswordException(string username)
		{
			Username = username;
		}

		public InvalidPasswordException(string message, string username) : base(message)
		{
			Username = username;
		}

		public InvalidPasswordException(string message, Exception innerException, string username) : base(message, innerException)
		{
			Username = username;
		}
	}
}
