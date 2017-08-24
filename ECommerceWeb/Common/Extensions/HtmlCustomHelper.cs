using System;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace ECommerceWeb.Common.Extensions
{
	/// <summary>
	/// TODO: This class is work-in-progress. The aim is to make helper methods to become the MVC equivalents of the Volume
	/// toolkit's Validation utility methods for rendering and flagging icons, and the summary message user control. These
	/// helpers should be flexible enough for any of our MVC projects and should also end up in the toolkit.
	/// </summary>
	public static class HtmlCustomHelper
	{
		/// <summary>
		/// Renders a div element containing a validation error message, intended for use in combination with the
		/// ValidationIconFor helper method which flags any invalid fields. If multiple error messages have been raised, the
		/// user is directed to check all invalid fields. If one error message has been raised, the user is shown the details of
		/// that specific error. Compatible with the jQuery unobtrusive validation library.
		/// </summary>
		public static MvcHtmlString ValidationIconSummary(this HtmlHelper htmlHelper)
		{
			TagBuilder          tagBuilder                  = new TagBuilder("div");

			tagBuilder.Attributes.Add("data-valmsg-summary", "true");

			if (htmlHelper.ViewData.ModelState.IsValid)
			{
				tagBuilder.AddCssClass("validation-summary-valid");
			}
			else
			{
				tagBuilder.AddCssClass("validation-summary-errors");

				ModelState      invalidFieldModelState      = null;

				foreach (ModelState modelState in htmlHelper.ViewData.ModelState.Values)
				{
					if (modelState.Errors.Count > 0)
					{
						if (invalidFieldModelState != null)
						{
							invalidFieldModelState          = null;
							break;
						}

						invalidFieldModelState              = modelState;
					}
				}

				if (invalidFieldModelState != null)
				{
					tagBuilder.SetInnerText(invalidFieldModelState.Errors[0].ErrorMessage);
				}
				else
				{
					// TODO: Load this from resource file or custom data annotation attribute.
					tagBuilder.SetInnerText(Resources.Common.Validation.ErrorMultipleInvalidFields);
				}
			}

			return MvcHtmlString.Create(tagBuilder.ToString());
		}

		/// <summary>
		/// Renders an anchor element which shows either the field's hint message or the relevant error message, depending on
		/// the field's validation state. Compatible with the jQuery unobtrusive validation library.
		/// </summary>
		public static MvcHtmlString ValidationIconFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			string          propertyName            = ExpressionHelper.GetExpressionText(expression);

			ModelMetadata   metaData                = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			TagBuilder      tagBuilder              = new TagBuilder("a");

			tagBuilder.AddCssClass("icon");
			tagBuilder.AddCssClass("validation");

			// Here we add the class names required to support jQuery unobtrusive validation.
			tagBuilder.Attributes.Add("data-valmsg-for", propertyName);
			tagBuilder.Attributes.Add("data-valmsg-replace", "true");

			ModelState      modelState;

			// If there's at least one error, we check whether the field whose icon we are going to render is the one
			// responsible. If so, we show the appropriate validation message in the icon's tooltip.
			if (!htmlHelper.ViewData.ModelState.IsValid &&
				htmlHelper.ViewData.ModelState.TryGetValue(propertyName, out modelState) &&
				modelState.Errors.Count > 0)
			{
				tagBuilder.Attributes.Add("title", modelState.Errors[0].ErrorMessage);
				tagBuilder.AddCssClass("field-validation-error");
			}
			else
			{
				tagBuilder.Attributes.Add("title", metaData.Watermark);
				tagBuilder.AddCssClass("field-validation-valid");
				// TODO: Customisable configuration
			}

			return MvcHtmlString.Create(tagBuilder.ToString());
		}
	}
}
