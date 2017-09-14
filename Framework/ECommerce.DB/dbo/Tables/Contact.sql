CREATE TABLE [dbo].[Contact] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (100) NOT NULL,
    [email]       NVARCHAR (255) NOT NULL,
    [contact_no]  NVARCHAR (30)  NOT NULL,
    [subject]     NVARCHAR (50)  NOT NULL,
    [message]     NVARCHAR (250) NOT NULL,
    [date]        DATETIME       NOT NULL,
    [read_status] INT            NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([ID] ASC)
);

