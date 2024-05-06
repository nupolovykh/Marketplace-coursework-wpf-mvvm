using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.Domain.Exceptions
{
	public class IsNotAvailableExeprion : Exception
	{
		public int Id { get; set; }

		public IsNotAvailableExeprion(int id)
		{
			Id = id;
		}
	}
}
