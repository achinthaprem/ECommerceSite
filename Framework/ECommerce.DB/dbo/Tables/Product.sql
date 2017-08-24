﻿CREATE TABLE [dbo].[Product] (
    [ID]                  INT             IDENTITY (1, 1) NOT NULL,
    [category_id]         INT             NOT NULL,
    [name]                NVARCHAR (200)  NOT NULL,
    [description]         NVARCHAR (MAX)  NOT NULL,
    [price]               DECIMAL (18, 2) NOT NULL,
    [image_name]          NVARCHAR (500)  NOT NULL,
    [status]              INT             NOT NULL,
    [date_created]        DATETIME        NOT NULL,
    [date_modified]       DATETIME        NOT NULL,
    [created_account_id]  INT             NOT NULL,
    [modified_account_id] INT             NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ID] ASC)
);

