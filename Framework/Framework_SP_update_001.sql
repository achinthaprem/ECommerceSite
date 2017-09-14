/* DROP THE EXISTING PROCEDURE [OrderItemInsert] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[OrderItemInsert]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[OrderItemInsert]
GO

/* DROP THE EXISTING PROCEDURE [OrderItemGetByID] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[OrderItemGetByID]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[OrderItemGetByID]
GO

/* DROP THE EXISTING PROCEDURE [OrderItemList] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[OrderItemList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[OrderItemList]
GO

/* DROP THE EXISTING PROCEDURE [OrderItemUpdate] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[OrderItemUpdate]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[OrderItemUpdate]
GO

/* DROP THE EXISTING PROCEDURE [OrderItemDelete] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[OrderItemDelete]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[OrderItemDelete]
GO

/* DROP THE EXISTING PROCEDURE [OrderItemListByOrderID] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[OrderItemListByOrderID]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[OrderItemListByOrderID]
GO

/* DROP THE EXISTING PROCEDURE [OrderItemListByProductID] */
IF EXISTS (SELECT * FROM dbo.[SysObjects] WHERE ID = OBJECT_ID(N'[dbo].[OrderItemListByProductID]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
	DROP PROCEDURE [dbo].[OrderItemListByProductID]
GO

CREATE PROCEDURE [dbo].[OrderItemInsert]
	@order_id INT,
	@product_id INT,
	@quantity INT,
	@unit_cost DECIMAL(0, 0),
	@subtotal DECIMAL(0, 0)
AS

	/* Volume Technologies RADA Generator v6.1 */

	INSERT INTO [OrderItem]
	(
		[order_id],
		[product_id],
		[quantity],
		[unit_cost],
		[subtotal]
	)
	VALUES
	(
		@order_id,
		@product_id,
		@quantity,
		@unit_cost,
		@subtotal
	)

	SELECT SCOPE_IDENTITY()

GO

CREATE PROCEDURE [dbo].[OrderItemGetByID]
	@ID INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	INTO #temp
	FROM [OrderItem]
	WHERE [ID] = @ID

	SELECT *
	FROM #temp

	SELECT *
	FROM [Order]
	WHERE [ID] IN (SELECT [order_id] FROM #temp)

	SELECT *
	FROM [Product]
	WHERE [ID] IN (SELECT [product_id] FROM #temp)

GO

CREATE PROCEDURE [dbo].[OrderItemList]
	
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	INTO #temp
	FROM [OrderItem]

	SELECT *
	FROM #temp

	SELECT *
	FROM [Order]
	WHERE [ID] IN (SELECT [order_id] FROM #temp)

	SELECT *
	FROM [Product]
	WHERE [ID] IN (SELECT [product_id] FROM #temp)

GO

CREATE PROCEDURE [dbo].[OrderItemListByOrderID]
	@order_id INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	INTO #temp
	FROM [OrderItem]
	WHERE [order_id] = @order_id

	SELECT *
	FROM #temp

	SELECT *
	FROM [Order]
	WHERE [ID] IN (SELECT [order_id] FROM #temp)

	SELECT *
	FROM [Product]
	WHERE [ID] IN (SELECT [product_id] FROM #temp)

GO

CREATE PROCEDURE [dbo].[OrderItemListByProductID]
	@product_id INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	SELECT *
	INTO #temp
	FROM [OrderItem]
	WHERE [product_id] = @product_id

	SELECT *
	FROM #temp

	SELECT *
	FROM [Order]
	WHERE [ID] IN (SELECT [order_id] FROM #temp)

	SELECT *
	FROM [Product]
	WHERE [ID] IN (SELECT [product_id] FROM #temp)

GO

CREATE PROCEDURE [dbo].[OrderItemUpdate]
	@ID INT,
	@order_id INT,
	@product_id INT,
	@quantity INT,
	@unit_cost DECIMAL(0, 0),
	@subtotal DECIMAL(0, 0)
AS

	/* Volume Technologies RADA Generator v6.1 */

	UPDATE [OrderItem] SET
		[order_id] = @order_id,
		[product_id] = @product_id,
		[quantity] = @quantity,
		[unit_cost] = @unit_cost,
		[subtotal] = @subtotal
	WHERE [ID] = @ID

GO

CREATE PROCEDURE [dbo].[OrderItemDelete]
	@ID INT
AS

	/* Volume Technologies RADA Generator v6.1 */

	DELETE FROM [OrderItem]
	WHERE [ID] = @ID

GO

