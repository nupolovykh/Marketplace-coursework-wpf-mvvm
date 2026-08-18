using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Exceptions
{
	public class IsNotAvailableException : Exception
	{
		public int Id { get; set; }

		public IsNotAvailableException(int id)
		{
			Id = id;
		}
	}
}
