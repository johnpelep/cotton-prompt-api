﻿CREATE TABLE [dbo].[Orders]
(
	[Id] INT CONSTRAINT PK_OrderId PRIMARY KEY IDENTITY(1,1),
	[OrderNumber] NVARCHAR(50) NOT NULL,
	[Priority] BIT NOT NULL,
	[Concept] NVARCHAR(MAX) NOT NULL,
	[PrintColor] NVARCHAR(50) NOT NULL,
	[DesignBracketId] INT NOT NULL CONSTRAINT FK_Orders_OrderDesignBrackets REFERENCES [dbo].[OrderDesignBrackets]([Id]), 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedOn] DATETIME2 NOT NULL CONSTRAINT DF_Orders_CreatedOn DEFAULT GETUTCDATE(),
	[ArtistClaimedBy] UNIQUEIDENTIFIER NULL,
	[ArtistClaimedOn] DATETIME2 NULL,
	[CheckerClaimedBy] UNIQUEIDENTIFIER NULL,
	[CheckerClaimedOn] DATETIME2 NULL,
    [UpdatedBy] UNIQUEIDENTIFIER NULL, 
    [UpdatedOn] DATETIME2 NULL,
)
