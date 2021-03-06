USE [Prueba]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 11/6/2020 6:24:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PointOfSale] [nvarchar](150) NOT NULL,
	[Product] [nvarchar](150) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Stock] [int] NOT NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
