using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using ECommerce.Tables.Utility.System;
using ECommerceWeb.Common;
using Syncfusion.XlsIO;
using Volume.Toolkit.Paths;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Home
{
	public class ReportViewModel
	{

		#region Members

		private bool                                isAdmin                 = false;
		private List<SelectListItem>                activeCategoryList      = null;
		private List<TopSellingProductViewModel>    productList             = null;
		private int                                 selectedCategoryID      = Constants.DEFAULT_VALUE_INT;
		private List<SelectListItem>                productOptionList       = null;
		private TopSellingProductViewModel          selectedProduct         = null;
		private string                              reportName              = String.Empty;

		#endregion

		#region Properties

		public bool IsAdmin
		{
			get { return this.isAdmin; }
			set { this.isAdmin = value; }
		}

		public List<SelectListItem> ActiveCategoryList
		{
			get { return this.activeCategoryList; }
			set { this.activeCategoryList = value; }
		}

		public List<TopSellingProductViewModel> ProductList
		{
			get { return this.productList; }
			set { this.productList = value; }
		}

		public int SelectedCategoryID
		{
			get { return this.selectedCategoryID; }
			set { this.selectedCategoryID = value; }
		}

		public List<SelectListItem> ProductOptionList
		{
			get { return this.productOptionList; }
			set { this.productOptionList = value; }
		}

		public TopSellingProductViewModel SelectedProduct
		{
			get { return this.selectedProduct; }
			set { this.selectedProduct = value; }
		}

		public string ReportName
		{
			get { return this.reportName; }
			set { this.reportName = value; }
		}

		#endregion

		#region Constructors

		public ReportViewModel() { }

		public ReportViewModel(int? TopSellingFilterBy, int? SelectedProduct)
		{
			this.activeCategoryList					= Func.FilterActiveCategoryList(ETC.Category.ListByStatus(ETC.Category.STATUS_ACTIVE));
			this.isAdmin                            = true;
			this.productList						= GetTopSellingProducts(TopSellingFilterBy, IncludeNotSold: false);
			this.selectedCategoryID					= TopSellingFilterBy ?? 0;
			this.productOptionList					= ProductSelectList(GetTopSellingProducts(null, true));
			this.selectedProduct                    = SearchProduct(SelectedProduct);


			if (TopSellingFilterBy == null || TopSellingFilterBy == 0)
			{
				this.reportName						= "All Categories";
			}
			else
			{
				this.reportName						= ETC.Category.ExecuteCreate(TopSellingFilterBy ?? default(int)).Name;
			}

		}

		#endregion

		#region Methods

		public void GenerateReport(int? reportMode, HttpApplication application)
		{
			switch (reportMode)
			{
				case 1:
					this.GenerateWorkbook(this.productList, this.reportName, application);
					break;
				case 2:
					List<TopSellingProductViewModel> tempList = new List<TopSellingProductViewModel>();
					tempList.Add(this.selectedProduct);
					this.GenerateWorkbook(tempList, this.selectedProduct.Name.Replace("/", "-"), application);
					break;
				default:
					break;
			}
		}

		private void GenerateWorkbook(List<TopSellingProductViewModel> list, string ReportName, HttpApplication application)
		{
			using (ExcelEngine excelEngine = new ExcelEngine())
			{
				excelEngine.Excel.DefaultVersion                                = ExcelVersion.Excel2016;

				IWorkbook                       workbook                        = excelEngine.Excel.Workbooks.Create(1);

				IWorksheet                      worksheet                       = workbook.Worksheets[0];

				#region Cell Styles

				IStyle                          headerStyle                     = workbook.Styles.Add("HeaderStyle");
				headerStyle.Font.Bold                                           = true;
				headerStyle.Font.Size                                           = 12;
				headerStyle.HorizontalAlignment                                 = ExcelHAlign.HAlignCenter;
				headerStyle.Color                                               = Color.FromArgb(255, 174, 33);

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

				int                             startRowIndex                   = 2;

				for (int i = startRowIndex; i < (list.Count + startRowIndex); i++) // rows
				{
					#region Feeding Content

					worksheet.Range[i, 1].Text                                  = list[i-startRowIndex].ID.ToString();
					worksheet.Range[i, 1].CellStyle                             = contentStyle;
					worksheet.Range[i, 1].CellStyle.HorizontalAlignment         = ExcelHAlign.HAlignCenter;

					worksheet.Range[i, 2].Text                                  = list[i-startRowIndex].Name;
					worksheet.Range[i, 2].CellStyle                             = contentStyle;

					worksheet.Range[i, 3].Text                                  = list[i-startRowIndex].Description;
					worksheet.Range[i, 3].CellStyle                             = contentStyle;

					worksheet.Range[i, 4].Text                                  = Func.Currencyfy(list[i-startRowIndex].Price);
					worksheet.Range[i, 4].CellStyle                             = contentStyle;
					worksheet.Range[i, 4].CellStyle.HorizontalAlignment         = ExcelHAlign.HAlignCenter;

					System.Drawing.Image        image                           = System.Drawing.Image.FromFile(HostingEnvironment.MapPath(list[i-startRowIndex].ImageSrc));
					System.Drawing.Image        image_r                         = Imager.Resize(image, 200, 150, true);
					IPictureShape               shape                           = worksheet.Pictures.AddPicture(i, 5, image_r);
					worksheet.SetRowHeightInPixels(i, image_r.Height);

					worksheet.Range[i, 6].Text                                  = list[i-startRowIndex].Category;
					worksheet.Range[i, 6].CellStyle                             = contentStyle;

					worksheet.Range[i, 7].Text                                  = (list[i-startRowIndex].Status) ? "Active" : "Inactive";
					worksheet.Range[i, 7].CellStyle                             = contentStyle;
					worksheet.Range[i, 7].CellStyle.HorizontalAlignment         = ExcelHAlign.HAlignCenter;

					worksheet.Range[i, 8].Text                                  = list[i-startRowIndex].Sellings.ToString();
					worksheet.Range[i, 8].CellStyle                             = contentStyle;
					worksheet.Range[i, 8].CellStyle.HorizontalAlignment         = ExcelHAlign.HAlignCenter;

					#endregion
				}
				
				workbook.SaveAs($"Report - {ReportName}.xlsx", application.Response, ExcelDownloadType.Open);
			}
		}

		private TopSellingProductViewModel SearchProduct(int? selectedProduct)
		{
			TopSellingProductViewModel      result                      = null;

			if (selectedProduct.HasValue)
			{
				ETC.Product                 product                     = ETC.Product.ExecuteCreate(selectedProduct.Value);

				if (product != null)
				{
					result                                              = new TopSellingProductViewModel();
					result.ID                                           = product.ID;
					result.Name                                         = product.Name;
					result.Description                                  = product.Description;
					result.Price                                        = product.Price;
					result.ImageSrc                                     = PathUtility.CombineUrls(Config.StorageUrl, $@"Images/Product/{product.ID}/{product.ImageName}");
					result.Category                                     = product.ExecuteCreateCategoryByCategoryID().Name;
					result.CategoryID                                   = product.CategoryID;
					result.Status                                       = (product.Status == ETC.Product.STATUS_ACTIVE) ? true : false;
					result.Sellings                                     = CountSells(product);
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

		private List<TopSellingProductViewModel> GetTopSellingProducts(int? FilterBy, bool IncludeNotSold)
		{
			List<TopSellingProductViewModel>		result                  = new List<TopSellingProductViewModel>();
			List<ETC.Product>                       productList             = null;

			if (FilterBy.HasValue || FilterBy == 0)
			{
				productList                                                 = ETC.Product.List();
			}
			else
			{
				productList                                                 = ETC.Product.ListByCategoryID(FilterBy ?? 0);
			}

			foreach (ETC.Product product in productList)
			{
				int                                 sellings                = CountSells(product);

				if (sellings == 0 && !IncludeNotSold)
				{
					continue;
				}

				TopSellingProductViewModel          model                   = new TopSellingProductViewModel();
				model.ID                                                    = product.ID;
				model.Name                                                  = product.Name;
				model.Description                                           = product.Description;
				model.Price                                                 = product.Price;
				model.ImageSrc                                              = PathUtility.CombineUrls(Config.StorageUrl, $@"Images/Product/{product.ID}/{product.ImageName}");
				model.Category                                              = product.ExecuteCreateCategoryByCategoryID().Name;
				model.CategoryID                                            = product.CategoryID;
				model.Status                                                = (product.Status == ETC.Product.STATUS_ACTIVE) ? true : false;
				model.Sellings                                              = sellings;

				result.Add(model);
			}

			result                                                          = result.OrderByDescending(o => o.Sellings).ToList();

			return result;
		}

		private int CountSells(ETC.Product product)
		{
			List<ETC.OrderItem>                 orderItemList           = ETC.OrderItem.ListByProductID(product.ID);
			int                                 sellings                = 0;

			foreach (ETC.OrderItem item in orderItemList)
			{
				if (item.ExecuteCreateOrderByOrderID().Status == ETC.Order.STATUS_COMPLETED)
				{
					sellings                                            += item.Quantity;
				}
			}

			return sellings;
		}

		#endregion

	}
}