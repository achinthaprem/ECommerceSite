using ECommerce.Tables.Content;
using ECommerce.Tables.Content.Helpers;
using ECommerceWeb.Common;
using ECommerceWeb.Models.Home;
using Syncfusion.XlsIO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ECommerceWeb.Controllers
{
	public class HomeController : Controller
	{
		CategoryHelper              CategoryHelper          = new CategoryHelper();
		ProductHelper               ProductHelper           = new ProductHelper();
		OrderHelper                 OrderHelper             = new OrderHelper();
		ContactHelper               ContactHelper           = new ContactHelper();

		public async Task<ActionResult> Index(int? TopSellingFilterBy, int? SelectedProduct)
		{
			ActionResult            result                  = null;

			if (Common.Session.IsAdmin)
			{
				ViewBag.FilterList                          = ShopController.FilterActiveCategoryList(await CategoryHelper.GetCategoryListAsync());
				ViewBag.IsAdmin                             = true;

				var                 list                    = await GetTopSellingProductsAsync(TopSellingFilterBy, false);
				ViewBag.listCount                           = list.Count;

				ViewBag.selectedCategoryID                  = (TopSellingFilterBy == null) ? 0 : TopSellingFilterBy;

				ViewData["ProductSearchList"]               = ProductSelectList(await GetTopSellingProductsAsync(null, true));

				ViewData["SelectedProduct"]                 = await SearchProduct(SelectedProduct);

				if (TopSellingFilterBy == null || TopSellingFilterBy == 0)
				{
					ViewBag.CategoryReportName              = "All Categories";
				}
				else
				{
					ViewBag.CategoryReportName              = (await CategoryHelper.GetCategoryAsync(TopSellingFilterBy ?? default(int))).Name;
				}

				result                                      = View(list);
			}
			else if (Common.Session.Authorized)
			{
				ViewBag.IsAdmin                             = false;
				result                                      = View();
			}
			else
			{
				result                                      = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ExportToExcel(string DataName, string ReportName)
		{
			ActionResult                result                  = null;

			if (Common.Session.IsAdmin)
			{
				if (DataName == null)
				{
					result                                      = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				List<TopSellingProductViewModel> list           = TempData[DataName] as List<TopSellingProductViewModel>;

				if (list != null && list.Count > 0)
				{
					GenerateReport(list, ReportName);

					result                                      = RedirectToAction(Request.Url.PathAndQuery);
				}
				else
				{
					result                                      = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}
			else
			{
				result                                          = GetAdminAuthorizeRedirect(Request.Url.PathAndQuery);
			}

			return result;
		}

		private void GenerateReport(List<TopSellingProductViewModel> list, string ReportName)
		{
			using (ExcelEngine excelEngine = new ExcelEngine())
			{
				excelEngine.Excel.DefaultVersion								= ExcelVersion.Excel2016;

				IWorkbook						workbook						= excelEngine.Excel.Workbooks.Create(1);

				IWorksheet						worksheet						= workbook.Worksheets[0];

				#region Cell Styles

				IStyle							headerStyle						= workbook.Styles.Add("HeaderStyle");
				headerStyle.Font.Bold											= true;
				headerStyle.Font.Size											= 12;
				headerStyle.HorizontalAlignment									= ExcelHAlign.HAlignCenter;
				headerStyle.Color												= Color.FromArgb(255, 174, 33);

				IStyle                          contentStyle                    = workbook.Styles.Add("ContentStyle");
				contentStyle.VerticalAlignment                                  = ExcelVAlign.VAlignTop;

				#endregion

				#region Header Cells

				worksheet.Range[1, 1].Text                                      = "ID";
				worksheet.Range[1, 1].CellStyle                                 = headerStyle;
				worksheet.SetColumnWidth(1, 4.0);

				worksheet.Range[1, 2].Text                                      = "Name";
				worksheet.Range[1, 2].CellStyle                                 = headerStyle;
				worksheet.SetColumnWidth(2, 28.0);

				worksheet.Range[1, 3].Text                                      = "Description";
				worksheet.Range[1, 3].CellStyle                                 = headerStyle;
				worksheet.SetColumnWidth(3, 38.0);

				worksheet.Range[1, 4].Text                                      = "Price";
				worksheet.Range[1, 4].CellStyle                                 = headerStyle;
				worksheet.SetColumnWidth(4, 16.0);

				worksheet.Range[1, 5].Text                                      = "Image";
				worksheet.Range[1, 5].CellStyle                                 = headerStyle;
				worksheet.SetColumnWidth(5, 27.86);

				worksheet.Range[1, 6].Text                                      = "Category";
				worksheet.Range[1, 6].CellStyle                                 = headerStyle;
				worksheet.SetColumnWidth(6, 24.0);

				worksheet.Range[1, 7].Text                                      = "Status";
				worksheet.Range[1, 7].CellStyle                                 = headerStyle;
				worksheet.SetColumnWidth(7, 8.0);

				worksheet.Range[1, 8].Text                                      = "Sellings";
				worksheet.Range[1, 8].CellStyle                                 = headerStyle;
				worksheet.SetColumnWidth(8, 8.0);

				#endregion

				int								startRowIndex					= 2;

				for (int i = startRowIndex; i < (list.Count + startRowIndex); i++) // rows
				{
					worksheet.Range[i, 1].Text                                  = list[i-startRowIndex].ID.ToString();
					worksheet.Range[i, 1].CellStyle                             = contentStyle;
					worksheet.Range[i, 1].CellStyle.HorizontalAlignment			= ExcelHAlign.HAlignCenter;

					worksheet.Range[i, 2].Text                                  = list[i-startRowIndex].Name;
					worksheet.Range[i, 2].CellStyle                             = contentStyle;

					worksheet.Range[i, 3].Text                                  = list[i-startRowIndex].Description;
					worksheet.Range[i, 3].CellStyle                             = contentStyle;

					worksheet.Range[i, 4].Text                                  = Func.Currencyfy(list[i-startRowIndex].Price);
					worksheet.Range[i, 4].CellStyle                             = contentStyle;
					worksheet.Range[i, 4].CellStyle.HorizontalAlignment			= ExcelHAlign.HAlignCenter;

					System.Drawing.Image		image							= System.Drawing.Image.FromFile(HostingEnvironment.MapPath(list[i-startRowIndex].ImageSrc));
					System.Drawing.Image        image_r                         = Imager.Resize(image, 200, 150, true);
					IPictureShape				shape							= worksheet.Pictures.AddPicture(i, 5, image_r);
					worksheet.SetRowHeightInPixels(i, image_r.Height);

					worksheet.Range[i, 6].Text									= list[i-startRowIndex].Category;
					worksheet.Range[i, 6].CellStyle                             = contentStyle;

					worksheet.Range[i, 7].Text									= (list[i-startRowIndex].Status) ? "Active" : "Inactive";
					worksheet.Range[i, 7].CellStyle                             = contentStyle;
					worksheet.Range[i, 7].CellStyle.HorizontalAlignment         = ExcelHAlign.HAlignCenter;

					worksheet.Range[i, 8].Text									= list[i-startRowIndex].Sellings.ToString();
					worksheet.Range[i, 8].CellStyle                             = contentStyle;
					worksheet.Range[i, 8].CellStyle.HorizontalAlignment         = ExcelHAlign.HAlignCenter;

				}

				workbook.SaveAs($"Report - {ReportName}.xlsx", HttpContext.ApplicationInstance.Response, ExcelDownloadType.Open);
			}
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Contact(CreateContactFormViewModel model)
		{
			ActionResult                    result                      = Redirect(Request.UrlReferrer.ToString());

			if (ModelState.IsValid)
			{
				if (await ContactHelper.CreateContactAsync(
							model.Name,
							model.Email,
							model.ContactNo,
							model.Subject,
							model.Message,
							ECommerce.Tables.Content.Contact.STATUS_UNREAD))
				{
					TempData["alert-success"]                           = "Thank you for contacting us, we'll be in touch :)";
				}
				else
				{
					TempData["alert-fail"]                              = "Oh Snap! Something went wrong, try again later :/";
				}
			}
			else
			{
				result                                                  = View(model);
			}

			return result;
		}

		private async Task<TopSellingProductViewModel> SearchProduct(int? selectedProduct)
		{
			TopSellingProductViewModel      result                      = null;

			if (selectedProduct != null)
			{
				Product                     product                     = await ProductHelper.GetProductAsync(selectedProduct ?? default (int));

				if (product != null)
				{
					result                                              = new TopSellingProductViewModel();
					result.ID                                           = product.ID;
					result.Name                                         = product.Name;
					result.Description                                  = product.Description;
					result.Price                                        = product.Price;
					result.ImageSrc                                     = $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
					result.Category                                     = product.ExecuteCreateCategoryByCategoryID().Name;
					result.CategoryID                                   = product.CategoryID;
					result.Status                                       = (product.Status == Product.STATUS_ACTIVE) ? true : false;
					result.Sellings                                     = await CountSellsAsync(product);
				}
			}

			return result;
		}

		private List<SelectListItem> ProductSelectList(List<TopSellingProductViewModel> modelList)
		{
			List<SelectListItem>            result                      = new List<SelectListItem>();

			foreach (TopSellingProductViewModel model in modelList)
			{
				result.Add(new SelectListItem { Value = model.ID.ToString(), Text = model.Name });
			}

			return result;
		}

		private async Task<List<TopSellingProductViewModel>> GetTopSellingProductsAsync(int? FilterBy, bool IncludeNotSold)
		{
			List<TopSellingProductViewModel>        result                  = new List<TopSellingProductViewModel>();
			List<Product>                           productList             = null;

			if (FilterBy == null || FilterBy == 0)
			{
				productList                                                 = await ProductHelper.GetProductListAsync();
			}
			else
			{
				productList                                                 = await ProductHelper.GetProductListByCategoryAsync(FilterBy ?? default(int));
			}

			foreach (Product product in productList)
			{
				int                                 sellings                = await CountSellsAsync(product);

				if (sellings == 0 && !IncludeNotSold)
				{
					continue;
				}

				TopSellingProductViewModel          model                   = new TopSellingProductViewModel();
				model.ID                                                    = product.ID;
				model.Name                                                  = product.Name;
				model.Description                                           = product.Description;
				model.Price                                                 = product.Price;
				model.ImageSrc                                              = $@"~/Filestore/Images/Product/{product.ID}/{product.ImageName}";
				model.Category                                              = product.ExecuteCreateCategoryByCategoryID().Name;
				model.CategoryID                                            = product.CategoryID;
				model.Status                                                = (product.Status == Product.STATUS_ACTIVE) ? true : false;
				model.Sellings                                              = sellings;

				result.Add(model);
			}

			result                                                          = result.OrderByDescending(o => o.Sellings).ToList();

			return result;
		}

		private async Task<int> CountSellsAsync(Product product)
		{
			List<OrderItem>                         orderItemList           = await OrderHelper.GetOrderItemsByProductIDAsync(product.ID);
			int                                     sellings                = 0;

			foreach (OrderItem item in orderItemList)
			{
				if (item.ExecuteCreateOrderByOrderID().Status == Order.STATUS_COMPLETED)
				{
					sellings                                                += item.Quantity;
				}
			}

			return sellings;
		}

		private ActionResult GetAdminAuthorizeRedirect(string returnUrl)
		{
			TempData[Constants.CONST_ADMIN_ONLY_LOGIN]                  = Constants.MSG_ADMIN_ONLY_LOGIN;

			return RedirectToAction(Constants.ACTION_LOGIN, Constants.CONTROLLER_ACCOUNT, new { returnUrl = returnUrl });
		}
	}
}