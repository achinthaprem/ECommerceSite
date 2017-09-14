/* DROP THE EXISTING PROCEDURE [ProductInsert] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ProductInsert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ProductInsert]
GO

/* DROP THE EXISTING PROCEDURE [ProductGetByID] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ProductGetByID]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ProductGetByID]
GO

/* DROP THE EXISTING PROCEDURE [ProductList] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ProductList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ProductList]
GO

/* DROP THE EXISTING PROCEDURE [ProductUpdate] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ProductUpdate]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ProductUpdate]
GO

/* DROP THE EXISTING PROCEDURE [ProductDelete] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ProductDelete]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ProductDelete]
GO

/* DROP THE EXISTING PROCEDURE [ProductListByCategoryID] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ProductListByCategoryID]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ProductListByCategoryID]
GO

/* DROP THE EXISTING PROCEDURE [ProductListByStatus] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[ProductListByStatus]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[ProductListByStatus]
GO

CREATE PROCEDURE [dbo].[ProductInsert]
	@category_id INT,
	@name NVARCHAR(200),
	@description NVARCHAR(MAX),
	@price DECIMAL(0, 0),
	@image_name NVARCHAR(500),
	@status INT,
	@date_created DATETIME,
	@date_modified DATETIME,
	@created_account_id INT,
	@modified_account_id INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	INSERT INTO [Product]
	(
		[category_id],
		[name],
		[description],
		[price],
		[image_name],
		[status],
		[date_created],
		[date_modified],
		[created_account_id],
		[modified_account_id]
	)
	VALUES
	(
		@category_id,
		@name,
		@description,
		@price,
		@image_name,
		@status,
		@date_created,
		@date_modified,
		@created_account_id,
		@modified_account_id
	)

	SELECT SCOPE_IDENTITY()

GO

CREATE PROCEDURE [dbo].[ProductGetByID]
	@ID INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	INTO #temp
	FROM [Product]
	WHERE [ID] = @ID

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

GO

CREATE PROCEDURE [dbo].[ProductList]
	
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	INTO #temp
	FROM [Product]

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

GO

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

GO

CREATE PROCEDURE [dbo].[ProductListByStatus]
	@status INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	INTO #temp
	FROM [Product]
	WHERE [status] = @status

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

GO

CREATE PROCEDURE [dbo].[ProductUpdate]
	@ID INT,
	@category_id INT,
	@name NVARCHAR(200),
	@description NVARCHAR(MAX),
	@price DECIMAL(0, 0),
	@image_name NVARCHAR(500),
	@status INT,
	@date_created DATETIME,
	@date_modified DATETIME,
	@created_account_id INT,
	@modified_account_id INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	UPDATE [Product] SET
		[category_id] = @category_id,
		[name] = @name,
		[description] = @description,
		[price] = @price,
		[image_name] = @image_name,
		[status] = @status,
		[created_account_id] = @created_account_id,
		[modified_account_id] = @modified_account_id
	WHERE [ID] = @ID

GO

CREATE PROCEDURE [dbo].[ProductDelete]
	@ID INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	DELETE FROM [Product]
	WHERE [ID] = @ID

GO

