﻿CREATE TABLE [dbo].[InvoiceSections]
(
	[Id] INT CONSTRAINT PK_InvoiceSectionId PRIMARY KEY IDENTITY(1,1),
	[InvoiceId] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Amount] DECIMAL(18,2) NOT NULL,
	[Quantity] INT NOT NULL,
	CONSTRAINT FK_InvoiceSections_Invoices FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoices]([Id]) ON DELETE CASCADE
)
