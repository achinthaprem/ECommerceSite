CREATE TABLE [dbo].[OrderItem] (
    [ID]         INT             IDENTITY (1, 1) NOT NULL,
    [order_id]   INT             NOT NULL,
    [product_id] INT             NOT NULL,
    [quantity]   INT             NOT NULL,
    [unit_cost]  DECIMAL (18, 2) NOT NULL,
    [subtotal]   DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_OrderItem_product_id]
    ON [dbo].[OrderItem]([product_id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OrderItem_order_id]
    ON [dbo].[OrderItem]([order_id] ASC);

