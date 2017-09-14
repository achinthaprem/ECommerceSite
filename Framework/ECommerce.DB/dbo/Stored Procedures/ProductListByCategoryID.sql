
CREATE PROCEDURE [dbo].[ProductListByCategoryID]
	@category_id INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	INTO #temp
	FROM [Product]
	WHERE [category_id] = @category_id

	SELECT *
	FROM #temp

	SELECT *
	FROM [Category]
	WHERE [ID] IN (SELECT [category_id] FROM #temp)

	SELECT *
	FROM [Account]
	WHERE [ID] IN (SELECT [created_account_id] FROM #temp)

	SELECT *
	FROM [Account]
	WHERE [ID] IN (SELECT [modified_account_id] FROM #temp)