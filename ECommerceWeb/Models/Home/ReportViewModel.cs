using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
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

		private bool						isAdmin                 = false;
		private SelectList					activeCategoryList      = null;
		private List<SalesViewModel>		productList             = null;
		private int                         selectedCategoryID      = Constants.DEFAULT_VALUE_INT;
		private SelectList					productOptionList       = null;
		private SalesViewModel				selectedProduct         = null;
		private string                      reportName              = String.Empty;

		#endregion

		#region Properties

		public bool IsAdmin
		{
			get { return this.isAdmin; }
			set { this.isAdmin = value; }
		}

		public SelectList ActiveCategoryList
		{
			get { return this.activeCategoryList; }
			set { this.activeCategoryList = value; }
		}

		public List<SalesViewModel> ProductList
		{
			get { return this.productList; }
			set { this.productList = value; }
		}

		public int SelectedCategoryID
		{
			get { return this.selectedCategoryID; }
			set { this.selectedCategoryID = value; }
		}

		public SelectList ProductOptionList
		{
			get { return this.productOptionList; }
			set { this.productOptionList = value; }
		}

		public SalesViewModel SelectedProduct
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

		private ReportViewModel(int? TopSellingFilterBy, int? SelectedProduct)
		{
			this.activeCategoryList                 = Lists.ListCategories(
														ETC.Category.ListByStatus(ETC.Category.STATUS_ACTIVE), 
														Lists.SelectorType.WithAll, 
														selectedValue: null);
			this.isAdmin                            = true;
			this.productList						= SalesViewModel.List(
														TopSellingFilterBy, 
														IncludeNotSold: false);
			this.selectedCategoryID					= TopSellingFilterBy ?? Constants.DEFAULT_VALUE_INT;
			this.productOptionList					= Lists.ListProducts(
														ETC.Product.ListByStatus(ETC.Product.STATUS_ACTIVE), 
														Lists.SelectorType.None,
														selectedValue: null);
			this.selectedProduct                    = SearchProduct(SelectedProduct);
		}

		#endregion

		#region Execute Create

		public static ReportViewModel ExecuteCreate(int? TopSellingFilterBy, int? SelectedProduct)
		{
			ReportViewModel             result              = new ReportViewModel(TopSellingFilterBy, SelectedProduct);

			if (TopSellingFilterBy == null || TopSellingFilterBy == Constants.DEFAULT_VALUE_INT)
			{
				result.reportName							= "All Categories";
			}
			else
			{
				ETC.Category            category            = ETC.Category.ExecuteCreate(TopSellingFilterBy.Value);

				if (category != null)
				{
					result.reportName                       = category.Name;
				}
				else
				{
					result                                  = null;
				}
			}

			return result;
		}

		#endregion

		#region Methods

		public void GenerateReport(int? reportMode, int? TopSellingFilterBy, HttpApplication application)
		{
			switch (reportMode)
			{
				case 1:
					this.GenerateWorkbook(
						SalesViewModel.List(TopSellingFilterBy, IncludeNotSold: false), 
						this.reportName, 
						application);
					break;
				case 2:
					List<SalesViewModel> tempList = new List<SalesViewModel>();
					tempList.Add(this.selectedProduct);

					this.GenerateWorkbook(
						tempList, 
						this.selectedProduct.Name.Replace("/", "-"), 
						application);
					break;
				default:
					break;
			}
		}

		#endregion

		#region Utility Methods

		private void GenerateWorkbook(List<SalesViewModel> list, string ReportName, HttpApplication application)
		{ // TODO: Replace with List<Product> or else suitable
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

					System.Drawing.Image        image                           = System.Drawing.Image.FromFile(PathUtility.CombinePaths(Config.StoragePathProduct, 
																					list[i-startRowIndex].ID.ToString(), 
																					list[i-startRowIndex].ImageName));
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

		private SalesViewModel SearchProduct(int? selectedProduct)
		{
			SalesViewModel			result              = null;

			if (selectedProduct.HasValue)
			{
				ETC.Product         product             = ETC.Product.ExecuteCreate(selectedProduct.Value);

				if (product != null)
				{
					result                              = SalesViewModel.ExecuteCreate(product);
				}
			}

			return result;
		}
		
		#endregion

	}
}