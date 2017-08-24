using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Tables.Utility.System
{
	public static class TableDateTimeUtility
	{
		public static DateTime GetDateTimeNow()
		{
			return DateTime.UtcNow;
		}
	}
}
