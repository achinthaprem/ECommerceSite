using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceWeb.Common.Utility
{
	public static class Lists
	{
		#region Enum

		/// <summary>
		/// Represents a type of selector for a list.
		/// </summary>
		public enum SelectorType
		{
			/// <summary>
			/// No selector.
			/// </summary>
			None,
			/// <summary>
			/// With a 'select' item.
			/// </summary>
			WithSelect,
			/// <summary>
			/// With an 'all' item.
			/// </summary>
			WithAll
		}

		#endregion

		#region  Constants 

		private const string            KEY_VALUE_PAIR_KEY                  = "Key";
		private const string            KEY_VALUE_PAIR_VALUE                = "Value";

		#endregion

		#region ListMethods

		/// <summary>
		/// Retrieves list of tags
		/// </summary>
		/// <returns></returns>
		public static SelectList ListRoles(SelectorType selector)
		{
			SelectList                                  result              = null;

			List<KeyValuePair<int?, string>>            keyValueList        = new List<KeyValuePair<int?,string>>();

			keyValueList.Add(new KeyValuePair<int?, string>((int)Account.RoleCode.SuperUser, Account.GetRoleText(Account.RoleCode.SuperUser)));
			keyValueList.Add(new KeyValuePair<int?, string>((int)Account.RoleCode.Trainer, Account.GetRoleText(Account.RoleCode.Trainer)));
			keyValueList.Add(new KeyValuePair<int?, string>((int)Account.RoleCode.Tester, Account.GetRoleText(Account.RoleCode.Tester)));

			result                                                          = GetSelectListObject(keyValueList, Constants.DEFAULT_VALUE_INT, selector);

			return result;
		}

		/// <summary>
		/// Retrieves list of tags
		/// </summary>
		/// <returns></returns>
		public static SelectList ListNonCanonicalQuestions(SelectorType selector)
		{
			SelectList                                  result              = null;

			List<KeyValuePair<int?, string>>            keyValueList        = new List<KeyValuePair<int?,string>>();
			List<CanonicalQuestion>                     list                = CanonicalQuestion.ListByWithoutType((int) CanonicalQuestion.TypeCodes.Question);

			for (int i = 0; i < list.Count; i++)
			{
				keyValueList.Add(new KeyValuePair<int?, string>((int)list[i].ID, list[i].Title));
			}

			result                                                          = GetSelectListObject(keyValueList, Constants.DEFAULT_VALUE_INT, selector);

			return result;
		}

		/// <summary>
		/// Retrieves list of tags
		/// </summary>
		/// <returns></returns>
		public static SelectList ListTags(SelectorType selector)
		{
			SelectList                                  result              = null;

			List<KeyValuePair<int?, string>>            keyValueList        = new List<KeyValuePair<int?,string>>();

			List<Tag>                                   tagList             = Tag.List();

			for (int i = 0; i < tagList.Count; i++)
			{

				keyValueList.Add(new KeyValuePair<int?, string>(tagList[i].ID, tagList[i].Name));
			}

			result                                                          = GetSelectListObject(keyValueList, Constants.DEFAULT_VALUE_INT, selector);

			return result;
		}

		/// <summary>
		/// Retrieves list of tags
		/// </summary>
		/// <returns></returns>
		public static SelectList ListUser(SelectorType selector)
		{
			SelectList                                  result              = null;

			List<KeyValuePair<int?, string>>            keyValueList        = new List<KeyValuePair<int?,string>>();

			List<Account>                               accountList         = Account.List();

			for (int i = 0; i < accountList.Count; i++)
			{

				keyValueList.Add(new KeyValuePair<int?, string>(accountList[i].ID, accountList[i].FullName));
			}

			result                                                          = GetSelectListObject(keyValueList, Constants.DEFAULT_VALUE_INT, selector);

			return result;
		}

		/// <summary>
		/// Retrieves list of tags
		/// </summary>
		/// <returns></returns>
		public static List<ValueTextPair> ListTags()
		{
			List<ValueTextPair>                         result              = new List<ValueTextPair>();
			List<Tag>                                   tagList             = Tag.List();

			for (int i = 0; i < tagList.Count; i++)
			{
				result.Add(new ValueTextPair(tagList[i].ID.ToString(), tagList[i].Name));
			}

			return result;
		}

		/// <summary>
		/// Retrieves list of permission levels
		/// </summary>
		/// <returns></returns>
		public static List<SelectListItem> ListSelectedTags(List<TagModel> tagList)
		{
			List<SelectListItem>                        result              = new List<SelectListItem>();

			for (int i = 0; i < tagList.Count; i++)
			{
				SelectListItem                          item                = new SelectListItem();
				item.Text                                                   = tagList[i].Title;
				item.Value                                                  = tagList[i].ID;
				item.Selected                                               = true;

				result.Add(item);
			}

			return result;
		}


		/// <summary>
		/// Retrieves list of permission levels
		/// </summary>
		/// <returns></returns>
		public static SelectList ListCQPermissionLevels(SelectorType selector, int selectionID)
		{
			SelectList                                  result              = null;

			List<KeyValuePair<int, string>>             keyValueList        = new List<KeyValuePair<int,string>>();

			keyValueList.Add(new KeyValuePair<int, string>((int)CanonicalQuestion.VisibilityTypes.Internal, ListsResource.ListItemTextCQVisibilityInternal));
			keyValueList.Add(new KeyValuePair<int, string>((int)CanonicalQuestion.VisibilityTypes.Distributor, ListsResource.ListItemTextCQVisibilityDistributor));
			keyValueList.Add(new KeyValuePair<int, string>((int)CanonicalQuestion.VisibilityTypes.Customer, ListsResource.ListItemTextCQVisibilityCustomer));
			keyValueList.Add(new KeyValuePair<int, string>((int)CanonicalQuestion.VisibilityTypes.Public, ListsResource.ListItemTextCQVisibilityPublic));

			result                                                          = GetSelectListObject(keyValueList, selectionID, selector);

			return result;
		}

		/// <summary>
		/// Retrieve the user sentiment 
		/// </summary>
		/// <param name="selector"></param>
		/// <param name="selectionID"></param>
		/// <returns></returns>
		public static SelectList ListUserSentiment(SelectorType selector, int selectionID)
		{
			SelectList                              result                          = null;
			List<KeyValuePair<int, string>>         keyValueList                    = new List<KeyValuePair<int,string>>();
			keyValueList.Add(new KeyValuePair<int, string>((int)QuestionHistoryFeedback.StatusCode.Positive, QuestionHistoryFeedback.StatusCode.Positive.ToString()));
			keyValueList.Add(new KeyValuePair<int, string>((int)QuestionHistoryFeedback.StatusCode.Negative, QuestionHistoryFeedback.StatusCode.Negative.ToString()));

			result                                                                  = GetSelectListObject(keyValueList, selectionID, selector);

			return result;
		}

		/// <summary>
		/// Retrieves list of permission levels
		/// </summary>
		/// <returns></returns>
		public static SelectList ListCQPermissionLevelsPreviewMode(SelectorType selector, int selectionID)
		{
			SelectList                                  result              = null;

			List<KeyValuePair<int?, string>>            keyValueList        = new List<KeyValuePair<int?,string>>();

			keyValueList.Add(new KeyValuePair<int?, string>((int)CanonicalQuestion.VisibilityTypes.Internal, ListsResource.ListItemTextCQVisibilityInternalPreviewMode));
			keyValueList.Add(new KeyValuePair<int?, string>((int)CanonicalQuestion.VisibilityTypes.Distributor, ListsResource.ListItemTextCQVisibilityDistributorPreviewMode));
			keyValueList.Add(new KeyValuePair<int?, string>((int)CanonicalQuestion.VisibilityTypes.Customer, ListsResource.ListItemTextCQVisibilityCustomerPreviewMode));
			keyValueList.Add(new KeyValuePair<int?, string>((int)CanonicalQuestion.VisibilityTypes.Public, ListsResource.ListItemTextCQVisibilityPublicPreviewMode));

			result                                                          = GetSelectListObject(keyValueList, selectionID, selector);

			return result;
		}


		#endregion

		#region Utility Methods 

		/// <summary>
		/// Gets the select list object for the passed list  and selected item  
		/// </summary>
		/// <param name="list"></param>
		/// <param name="selectedValue"></param>
		/// <returns></returns>
		private static SelectList GetSelectListObject(List<KeyValuePair<string, string>> list)
		{

			SelectList                  result              = new SelectList(list, KEY_VALUE_PAIR_KEY, KEY_VALUE_PAIR_VALUE);

			return result;
		}


		/// <summary>
		/// Gets the select list object for the passed list  and selected item  
		/// </summary>
		/// <param name="list"></param>
		/// <param name="selectedValue"></param>
		/// <returns></returns>
		private static SelectList GetSelectListObject(List<KeyValuePair<int?, string>> list, int selectedValue, SelectorType selector)
		{
			list                                            = AddDefaultSelected(list, selector);

			SelectList                  result              = new SelectList(list, KEY_VALUE_PAIR_KEY, KEY_VALUE_PAIR_VALUE);

			if (selectedValue != Constants.DEFAULT_VALUE_INT)
			{
				result                                      = new SelectList(list, KEY_VALUE_PAIR_KEY, KEY_VALUE_PAIR_VALUE, selectedValue);
			}


			return result;
		}

		/// <summary>
		/// Gets the select list object for the passed list  and selected item  
		/// </summary>
		/// <param name="list"></param>
		/// <param name="selectedValue"></param>
		/// <returns></returns>
		private static SelectList GetSelectListObject(List<KeyValuePair<int, string>> list, int selectedValue, SelectorType selector)
		{
			list                                            = AddDefaultSelected(list, selector);

			SelectList                  result              = new SelectList(list, KEY_VALUE_PAIR_KEY, KEY_VALUE_PAIR_VALUE);

			if (selectedValue != Constants.DEFAULT_VALUE_INT)
			{
				result                                      = new SelectList(list, KEY_VALUE_PAIR_KEY, KEY_VALUE_PAIR_VALUE, selectedValue);
			}


			return result;
		}


		private static List<KeyValuePair<int?, string>> AddDefaultSelected(List<KeyValuePair<int?, string>> list, SelectorType selector)
		{
			switch (selector)
			{
				case SelectorType.WithSelect:
					list.Insert(0, new KeyValuePair<int?, string>(null, ListsResource.DropDownSelection));
					break;
				case SelectorType.WithAll:
					list.Insert(0, new KeyValuePair<int?, string>(null, ListsResource.DropDownAll));
					break;
			}


			return list;
		}

		private static List<KeyValuePair<int, string>> AddDefaultSelected(List<KeyValuePair<int, string>> list, SelectorType selector)
		{
			switch (selector)
			{
				case SelectorType.WithSelect:
					list.Insert(0, new KeyValuePair<int, string>(Constants.DEFAULT_VALUE_INT, ListsResource.DropDownSelection));
					break;
				case SelectorType.WithAll:
					list.Insert(0, new KeyValuePair<int, string>(Constants.DEFAULT_VALUE_INT, ListsResource.DropDownAll));
					break;
			}


			return list;
		}
		#endregion

	}
}