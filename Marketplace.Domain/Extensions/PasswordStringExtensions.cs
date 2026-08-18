using System;
using System.Security.Cryptography;

namespace Marketplace.Domain.Extensions
{
	/// <summary>
	/// Password hashing for stored employee credentials: PBKDF2-HMAC-SHA256 with a
	/// per-password random salt, stored as "iterations.salt.hash" in base64. The
	/// iteration count travels with the hash so it can be raised later without
	/// invalidating credentials already in the database.
	/// </summary>
	public static class PasswordStringExtensions
	{
		private const int SaltSize = 16;
		private const int HashSize = 32;
		private const int Iterations = 100_000;

		public static string ToHash(this string password)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
			byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);

			return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
		}

		public static bool VerifyHash(this string password, string storedHash)
		{
			if (string.IsNullOrEmpty(storedHash)) return false;

			string[] parts = storedHash.Split('.', 3);

			if (parts.Length != 3 || !int.TryParse(parts[0], out int iterations)) return false;

			byte[] salt;
			byte[] expectedHash;

			try
			{
				salt = Convert.FromBase64String(parts[1]);
				expectedHash = Convert.FromBase64String(parts[2]);
			}
			catch (FormatException)
			{
				return false;
			}

			byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expectedHash.Length);

			// Constant-time comparison - a plain SequenceEqual leaks how many leading
			// bytes matched through its timing.
			return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
		}
	}
}
