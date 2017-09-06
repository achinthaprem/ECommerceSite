using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace ECommerce.Tables.Content
{
	class CommonHelper
	{
		public static void SaveImage(HttpPostedFileBase Image, string _Path, string _FileName)
		{
			_Path                                           = $@"~\Filestore\{_Path}";
			
			Directory.CreateDirectory(HostingEnvironment.MapPath(_Path));
			
			Image.SaveAs(HostingEnvironment.MapPath($@"{_Path}\{_FileName}"));
		}
	}
}
