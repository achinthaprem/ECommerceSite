using System.IO;
using System.Web;
using System.Web.Hosting;

namespace ECommerce.Tables.Content
{
	public class CommonHelper
	{
		/// <summary>
		/// Saves the Image to the given Path
		/// </summary>
		/// <param name="Image">Uploaded Image file</param>
		/// <param name="_Path">Path to the file</param>
		/// <param name="_FileName">Image file name</param>
		public static void SaveImage(HttpPostedFileBase Image, string _Path, string _FileName)
		{
			_Path                                           = $@"~\Filestore\{_Path}";
			
			Directory.CreateDirectory(HostingEnvironment.MapPath(_Path));
			
			Image.SaveAs(HostingEnvironment.MapPath($@"{_Path}\{_FileName}"));
		}
	}
}
