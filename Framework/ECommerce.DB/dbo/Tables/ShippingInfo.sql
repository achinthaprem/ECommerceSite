CREATE TABLE [dbo].[ShippingInfo] (
    [ID]       INT             IDENTITY (1, 1) NOT NULL,
    [order_id] INT             NOT NULL,
    [type]     INT             NOT NULL,
    [cost]     DECIMAL (18, 2) NOT NULL,
    [address]  NVARCHAR (250)  NOT NULL,
    CONSTRAINT [PK_ShippingInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_ShippingInfo_order_id]
    ON [dbo].[ShippingInfo]([order_id] ASC);

