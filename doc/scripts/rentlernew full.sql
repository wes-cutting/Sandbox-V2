/****** Object:  User [RentlerAdmin]    Script Date: 04/26/2012 16:35:42 ******/
CREATE USER [RentlerAdmin] FOR LOGIN [RentlerAdmin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Photos]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photos](
	[PhotoId] [uniqueidentifier] NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[Extension] [nvarchar](4) NOT NULL,
	[SortOrder] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[PhotoId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'c5a2e108-fa84-41b4-9b5c-006ff7c1dbc5', 5, 0, N'jpg', 5, CAST(0x0000A03D016DDAF0 AS DateTime), N'web linq', CAST(0x0000A03D016DCEC6 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'7d13bc3c-a440-4fd1-8382-0ec3f0255665', 7, 0, N'jpg', 1, CAST(0x0000A03F000F165A AS DateTime), N'web linq', CAST(0x0000A03D016E8A30 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'3a00f62c-10ba-472e-bdf3-46d617d24f07', 7, 0, N'jpg', 5, CAST(0x0000A03D016EA76D AS DateTime), N'web linq', CAST(0x0000A03D016E962F AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'e023d781-1b2f-4885-8cb5-4f76f2d22f64', 5, 0, N'jpg', 4, CAST(0x0000A03D016DDAFF AS DateTime), N'web linq', CAST(0x0000A03D016DCD7E AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'd55f0890-6da7-4e5f-9c2a-68a561226928', 6, 0, N'jpg', 4, CAST(0x0000A03D0169D4FD AS DateTime), N'web linq', CAST(0x0000A03D016907A2 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'94288640-32b0-4551-ac7f-692503214e0f', 7, 0, N'jpg', 2, CAST(0x0000A03F000F1667 AS DateTime), N'web linq', CAST(0x0000A03D016E89ED AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'926c36f1-d507-4751-a2de-6978c8fc006d', 7, 0, N'jpg', 0, CAST(0x0000A03F000F1660 AS DateTime), N'web linq', CAST(0x0000A03D016E91D2 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'0026ab42-a26c-46b2-ae3b-807338c71405', 5, 0, N'jpg', 0, CAST(0x0000A03D016DB360 AS DateTime), N'web linq', CAST(0x0000A03D016D8801 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'943f7a83-d8e4-40f4-84d5-93861bd411a9', 5, 0, N'jpg', 2, NULL, NULL, CAST(0x0000A03D016DC5F9 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'4cbaa5d4-db65-44dd-a44f-97dee8ff6362', 4, 0, N'jpg', 0, CAST(0x0000A03D017392D8 AS DateTime), N'web linq', CAST(0x0000A036004FA7C1 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'b78f8f13-f9ab-4942-9303-9a0a8ecd0e16', 4, 0, N'jpg', 3, CAST(0x0000A03D0012BDAA AS DateTime), N'web linq', CAST(0x0000A0360052512E AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'16136fee-d645-4554-bbc9-b08618c49671', 7, 0, N'jpg', 3, CAST(0x0000A03F000F1654 AS DateTime), N'web linq', CAST(0x0000A03D016E8F3E AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'a670d1ab-4777-4014-95c1-b956d883dfad', 5, 0, N'jpg', 1, CAST(0x0000A03D016DDAE2 AS DateTime), N'web linq', CAST(0x0000A03D016DC6D3 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'cccecc56-e39d-4f44-a814-bffdb3dc2644', 6, 0, N'jpg', 5, CAST(0x0000A03D0169D4F0 AS DateTime), N'web linq', CAST(0x0000A03D01690862 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'004685a8-faa6-4014-b1e3-c049b171ccab', 6, 0, N'jpg', 3, CAST(0x0000A03D0169D4AD AS DateTime), N'web linq', CAST(0x0000A03D01690781 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'a4a7abd2-1853-4653-a8fe-c8c12b436fa6', 5, 0, N'jpg', 3, CAST(0x0000A03D016DDAD3 AS DateTime), N'web linq', CAST(0x0000A03D016DC7E6 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'24bbeb38-1c3a-4c45-b3f1-dcadd361c45b', 6, 0, N'jpg', 1, CAST(0x0000A03D0169D4CB AS DateTime), N'web linq', CAST(0x0000A03D0168FEDD AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'8d8cbf64-285f-4b13-a87c-dcd9a62e429a', 7, 0, N'jpg', 4, CAST(0x0000A03D016EA78A AS DateTime), N'web linq', CAST(0x0000A03D016E91E4 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'4638c59d-4324-40cc-879d-edeebdbfaa47', 4, 0, N'jpg', 2, CAST(0x0000A03D014F12BC AS DateTime), N'web linq', CAST(0x0000A036004A6D27 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'9357f94d-048c-465a-b751-ef8598cb6c47', 6, 0, N'jpg', 2, CAST(0x0000A03D0169D4D7 AS DateTime), N'web linq', CAST(0x0000A03D0168FEFD AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'6c520d58-8b34-4b17-8fdb-f37d03d27654', 4, 0, N'jpg', 1, CAST(0x0000A03D017392E3 AS DateTime), N'web linq', CAST(0x0000A0360050B0D6 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'b3f161cb-78b9-402a-924b-f6272132e2a3', 6, 0, N'jpg', 0, CAST(0x0000A03D0169D4E4 AS DateTime), N'web linq', CAST(0x0000A03D0168FDD9 AS DateTime), N'web linq', 0)
INSERT [dbo].[Photos] ([PhotoId], [BuildingId], [IsPrimary], [Extension], [SortOrder], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'd5b92040-7f00-4c9c-a695-fae1d862a4cc', 6, 0, N'jpg', 6, CAST(0x0000A03D0169D50A AS DateTime), N'web linq', CAST(0x0000A03D01695A62 AS DateTime), N'web linq', 0)
/****** Object:  Table [dbo].[FeaturedSections]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeaturedSections](
	[FeaturedSectionId] [int] IDENTITY(1,1) NOT NULL,
	[PriceLower] [decimal](19, 4) NOT NULL,
	[PriceUpper] [decimal](19, 4) NOT NULL,
 CONSTRAINT [PK_FeaturedSection] PRIMARY KEY CLUSTERED 
(
	[FeaturedSectionId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[FeaturedSections] ON
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (2, CAST(0.0000 AS Decimal(19, 4)), CAST(199.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (3, CAST(200.0000 AS Decimal(19, 4)), CAST(249.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (4, CAST(250.0000 AS Decimal(19, 4)), CAST(299.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (5, CAST(300.0000 AS Decimal(19, 4)), CAST(349.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (6, CAST(350.0000 AS Decimal(19, 4)), CAST(399.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (7, CAST(400.0000 AS Decimal(19, 4)), CAST(449.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (8, CAST(450.0000 AS Decimal(19, 4)), CAST(499.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (9, CAST(500.0000 AS Decimal(19, 4)), CAST(549.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (10, CAST(550.0000 AS Decimal(19, 4)), CAST(599.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (11, CAST(600.0000 AS Decimal(19, 4)), CAST(649.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (12, CAST(650.0000 AS Decimal(19, 4)), CAST(699.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (13, CAST(700.0000 AS Decimal(19, 4)), CAST(749.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (14, CAST(750.0000 AS Decimal(19, 4)), CAST(799.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (15, CAST(800.0000 AS Decimal(19, 4)), CAST(849.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (16, CAST(850.0000 AS Decimal(19, 4)), CAST(899.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (17, CAST(900.0000 AS Decimal(19, 4)), CAST(949.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (18, CAST(950.0000 AS Decimal(19, 4)), CAST(999.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (19, CAST(1000.0000 AS Decimal(19, 4)), CAST(1049.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (20, CAST(1050.0000 AS Decimal(19, 4)), CAST(1099.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (21, CAST(1100.0000 AS Decimal(19, 4)), CAST(1149.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (22, CAST(1150.0000 AS Decimal(19, 4)), CAST(1199.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (23, CAST(1200.0000 AS Decimal(19, 4)), CAST(1249.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (24, CAST(1250.0000 AS Decimal(19, 4)), CAST(1299.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (25, CAST(1300.0000 AS Decimal(19, 4)), CAST(1349.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (26, CAST(1350.0000 AS Decimal(19, 4)), CAST(1399.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (27, CAST(1400.0000 AS Decimal(19, 4)), CAST(1449.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (28, CAST(1450.0000 AS Decimal(19, 4)), CAST(1499.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (29, CAST(1500.0000 AS Decimal(19, 4)), CAST(1549.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (30, CAST(1550.0000 AS Decimal(19, 4)), CAST(1599.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (31, CAST(1600.0000 AS Decimal(19, 4)), CAST(1649.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (32, CAST(1650.0000 AS Decimal(19, 4)), CAST(1699.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (33, CAST(1700.0000 AS Decimal(19, 4)), CAST(1749.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (34, CAST(1750.0000 AS Decimal(19, 4)), CAST(1799.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (35, CAST(1800.0000 AS Decimal(19, 4)), CAST(1849.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (36, CAST(1850.0000 AS Decimal(19, 4)), CAST(1899.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (37, CAST(1900.0000 AS Decimal(19, 4)), CAST(1949.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (38, CAST(1950.0000 AS Decimal(19, 4)), CAST(1999.0000 AS Decimal(19, 4)))
INSERT [dbo].[FeaturedSections] ([FeaturedSectionId], [PriceLower], [PriceUpper]) VALUES (39, CAST(2000.0000 AS Decimal(19, 4)), CAST(99999.0000 AS Decimal(19, 4)))
SET IDENTITY_INSERT [dbo].[FeaturedSections] OFF
/****** Object:  Table [dbo].[Roles]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Roles_1] PRIMARY KEY CLUSTERED 
(
	[RoleName] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[Roles] ([RoleName], [Description], [IsDeleted]) VALUES (N'Admin', N'An administrator', 0)
/****** Object:  Table [dbo].[Ribbons]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ribbons](
	[RibbonId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Ribbons] PRIMARY KEY CLUSTERED 
(
	[RibbonId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[Ribbons] ON
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (1, N'3 Car Garage')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (2, N'Big Yard')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (3, N'Brand New')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (4, N'Great Schools')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (5, N'Near a Park')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (6, N'No Yardwork')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (7, N'Pet Friendly')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (8, N'Snow Removal')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (9, N'Utilities Included')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (10,N'Newly Remodeled')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (11,N'Air Conditioning')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (12,N'Clubhouse')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (13,N'Fenced Yard')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (14,N'First Month Free')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (15,N'Fitness Center')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (16,N'Granite Countertops')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (17,N'Hardwood Floors')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (18,N'Hot Tub')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (19,N'Month to Month')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (20,N'New Carpet')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (21,N'Newly Remodeled')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (22,N'New Paint')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (23,N'Onsite Maintenance')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (24,N'Onsite Manager')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (25,N'Playground')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (26,N'Pool')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (27,N'Smoker Friendly')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (28,N'Stainless Appliances')
INSERT [dbo].[Ribbons] ([RibbonId], [Name]) VALUES (29,N'Washer/Dryer')

SET IDENTITY_INSERT [dbo].[Ribbons] OFF
/****** Object:  Table [dbo].[AmenitiesWithOptions]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AmenitiesWithOptions](
	[AmenityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Category] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_AmenitiesWithOptions] PRIMARY KEY CLUSTERED 
(
	[AmenityId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[AmenitiesWithOptions] ON
INSERT [dbo].[AmenitiesWithOptions] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (1, N'Covered Garage', N'Has a covered garage', N'Community', CAST(0x00009E54017F46D8 AS DateTime), N'2010-12-22 23:15:28.080', CAST(0x00009E9801338CFC AS DateTime), N'sql script', 0)
INSERT [dbo].[AmenitiesWithOptions] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (2, N'Street Parking', N'Has street parking', N'Community', CAST(0x00009E54017F46D8 AS DateTime), N'2010-12-22 23:15:28.080', CAST(0x00009E9801338E46 AS DateTime), N'sql script', 0)
INSERT [dbo].[AmenitiesWithOptions] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (3, N'Air Conditioning', N'Has Air Conditioning', N'Property', CAST(0x00009F6600F73140 AS DateTime), N'dusda', CAST(0x00009F6600F73140 AS DateTime), N'dusda', 0)
INSERT [dbo].[AmenitiesWithOptions] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (4, N'Heating', N'Has heating', N'Property', CAST(0x00009F6600F73140 AS DateTime), N'dusda', CAST(0x00009F6600F73140 AS DateTime), N'dusda', 0)
SET IDENTITY_INSERT [dbo].[AmenitiesWithOptions] OFF
/****** Object:  Table [dbo].[Amenities]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Amenities](
	[AmenityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Category] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Amenities] PRIMARY KEY CLUSTERED 
(
	[AmenityId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[Amenities] ON
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (3, N'Cable/Satellite', N'Has access to cable or satellite TV', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E98013313FD AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (4, N'Dishwasher', N'Has a dishwasher', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801331540 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (5, N'Garbage Disposal', N'Has a garbage disposal', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801331682 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (6, N'Washer/Dryer', N'Has a washer and a dryer', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E98013317BF AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (7, N'Pool', N'Has a pool', N'Community', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E98013318F9 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (8, N'Hot Tub', N'Has a hot tub', N'Community', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801331A3B AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (9, N'Internet', N'Has internet access', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801331B77 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (10, N'Carpet', N'Has carpet', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801331CAC AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (11, N'Fireplace', N'Has a fireplace', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801331DF1 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (12, N'High Ceilings', N'Has high ceilings', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801331F29 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (13, N'Microwave', N'Has a microwave', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801332067 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (14, N'Patio/Balcony', N'Has a patio and/or balcony', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E98013321A1 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (15, N'Refrigerator', N'Has a refrigerator', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E98013322E5 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (16, N'View', N'Has a special view', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801332422 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (17, N'Hardwood Floors', N'Has  hardwood floors', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E980133255D AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (18, N'Affordable Housing', N'Is affordable housing', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801332695 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (19, N'Disability Access', N'Has disability access', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E98013327D0 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (20, N'Furnished', N'Is furnished', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E980133291E AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (21, N'Short Term Lease', N'Short-term lease available', N'Property', CAST(0x00009E54017F4E4E AS DateTime), N'2010-12-22 23:15:34.447', CAST(0x00009E9801332A59 AS DateTime), N'sql script', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (22, N'Pet-Friendly', N'Most pets are allowed', N'Property', CAST(0x00009FDA00E0CC4E AS DateTime), N'sql script patch', CAST(0x00009FDA00E0CC4E AS DateTime), N'sql script patch', 0)
INSERT [dbo].[Amenities] ([AmenityId], [Name], [Description], [Category], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (23, N'Smoking', N'Smoking is allowed', N'Property', CAST(0x00009FDA00E0CC5C AS DateTime), N'sql script patch', CAST(0x00009FDA00E0CC5C AS DateTime), N'sql script patch', 0)
SET IDENTITY_INSERT [dbo].[Amenities] OFF
/****** Object:  Table [dbo].[EvictionNotices]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EvictionNotices](
	[EvictionNoticeId] [int] IDENTITY(1,1) NOT NULL,
	[State] [nvarchar](10) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EvictionNoticeId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[ContractId] [int] IDENTITY(1,1) NOT NULL,
	[BaseText] [nvarchar](max) NOT NULL,
	[State] [nvarchar](10) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[ContractId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[ZipInfos]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZipInfos](
	[ZipInfoId] [int] IDENTITY(1,1) NOT NULL,
	[ZipCode] [int] NOT NULL,
	[StateCode] [nvarchar](2) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[Population] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ZipInfos] PRIMARY KEY CLUSTERED 
(
	[ZipInfoId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsLandlord] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[Users] ([UserId], [Username], [PasswordHash], [Email], [FirstName], [LastName], [PhoneNumber], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsLandlord], [IsDeleted]) VALUES (N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', N'dusda', N'DB753C751F846261A41B5A99BCE5E51017469478', N'dustin.dahl@gmail.com', N'Dustin', N'Dahl', NULL, CAST(0x00009FC901679D99 AS DateTime), N'web linq', CAST(0x00009E98010D4AC9 AS DateTime), N'web linq', 0, 0)
INSERT [dbo].[Users] ([UserId], [Username], [PasswordHash], [Email], [FirstName], [LastName], [PhoneNumber], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsLandlord], [IsDeleted]) VALUES (N'af4b3189-0430-4877-9696-a5212d9f83f0', N'bobbarker', N'DB753C751F846261A41B5A99BCE5E51017469478', N'bobbarker@gmail.com', N'Bob', N'Barker', NULL, NULL, NULL, CAST(0x0000A034000651A6 AS DateTime), N'web linq', 0, 0)
/****** Object:  Table [dbo].[RoleUsers]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleUsers](
	[RoleName] [nvarchar](50) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_RoleUsers] PRIMARY KEY CLUSTERED 
(
	[RoleName] ASC,
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[RoleUsers] ([RoleName], [UserId], [IsDeleted]) VALUES (N'Admin', N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', 0)
/****** Object:  Table [dbo].[UserCreditCards]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCreditCards](
	[UserCreditCardId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Alias] [bigint] NULL,
	[AccountReference] [uniqueidentifier] NULL,
	[CardNumber] [nvarchar](16) NOT NULL,
	[CardName] [nvarchar](50) NOT NULL,
	[ExpirationMonth] [int] NOT NULL,
	[ExpirationYear] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[AddressLine1] [nvarchar](50) NOT NULL,
	[AddressLine2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](2) NOT NULL,
	[Zip] [nvarchar](10) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UserCreditCards] PRIMARY KEY CLUSTERED 
(
	[UserCreditCardId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[UserCreditCards] ON
INSERT [dbo].[UserCreditCards] ([UserCreditCardId], [UserId], [Alias], [AccountReference], [CardNumber], [CardName], [ExpirationMonth], [ExpirationYear], [FirstName], [LastName], [AddressLine1], [AddressLine2], [City], [State], [Zip], [Phone], [Email], [IsDeleted], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy]) VALUES (1, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', 23135932, N'90cf95f2-54ad-4bdf-a84a-1d50296fdd02', N'************1111', N'Dustin Dahl', 7, 2012, N'Dustin', N'Dahl', N'5611 Goodway Dr', NULL, N'Murray', N'UT', N'84123', N'801.243.1675', N'dustin.dahl@gmail.com', 0, CAST(0x0000A039016AF136 AS DateTime), N'web linq', CAST(0x0000A03901670CCE AS DateTime), N'web linq')
INSERT [dbo].[UserCreditCards] ([UserCreditCardId], [UserId], [Alias], [AccountReference], [CardNumber], [CardName], [ExpirationMonth], [ExpirationYear], [FirstName], [LastName], [AddressLine1], [AddressLine2], [City], [State], [Zip], [Phone], [Email], [IsDeleted], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy]) VALUES (2, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', 23135969, N'92b18a9b-73cc-499e-bc35-4e7ff6969851', N'************1112', N'Dustin Dahl', 7, 2012, N'Dustin', N'Dahl', N'5610 Goodway Dr', NULL, N'Murray', N'UT', N'84123', N'801.243.1675', N'dustin.dahl@gmail.com', 0, NULL, NULL, CAST(0x0000A0390167563A AS DateTime), N'web linq')
INSERT [dbo].[UserCreditCards] ([UserCreditCardId], [UserId], [Alias], [AccountReference], [CardNumber], [CardName], [ExpirationMonth], [ExpirationYear], [FirstName], [LastName], [AddressLine1], [AddressLine2], [City], [State], [Zip], [Phone], [Email], [IsDeleted], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy]) VALUES (3, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', 23158688, N'0efa076e-6645-489b-a2af-81d0e3222453', N'***********1039', N'Dustin Dahl', 2, 2016, N'Dustin', N'Dahl', N'5611 Goodway Dr', NULL, N'Murray', N'UT', N'84123', NULL, NULL, 0, CAST(0x0000A03D002FD332 AS DateTime), N'web linq', CAST(0x0000A03D001B3022 AS DateTime), N'web linq')
SET IDENTITY_INSERT [dbo].[UserCreditCards] OFF
/****** Object:  Table [dbo].[UserContracts]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserContracts](
	[UserContractId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ContractId] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UserContracts] PRIMARY KEY CLUSTERED 
(
	[UserContractId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[ContractId] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Key] [nvarchar](50) NOT NULL,
	[Text] [nvarchar](500) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ContractQuestions] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[UserBanks]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBanks](
	[UserBankId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[PayerAlias] [bigint] NULL,
	[PayeeAlias] [bigint] NULL,
	[AccountName] [nvarchar](50) NOT NULL,
	[RoutingNumber] [nvarchar](9) NOT NULL,
	[AccountNumber] [nvarchar](20) NOT NULL,
	[AccountType] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[AddressLine1] [nvarchar](50) NOT NULL,
	[AddressLine2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](2) NOT NULL,
	[Zip] [nvarchar](10) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UserBanks] PRIMARY KEY CLUSTERED 
(
	[UserBankId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[ContactInfos]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactInfos](
	[ContactInfoId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](15) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CompanyName] [nvarchar](100) NULL,
	[Email] [nvarchar](250) NOT NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[DisplayPhoneNumber] [bit] NOT NULL,
 CONSTRAINT [PK_ContactInfos] PRIMARY KEY CLUSTERED 
(
	[ContactInfoId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[ContactInfos] ON
INSERT [dbo].[ContactInfos] ([ContactInfoId], [UserId], [Type], [Name], [CompanyName], [Email], [PhoneNumber], [DisplayPhoneNumber]) VALUES (6, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', N'Personal', N'Dustin Dahl', NULL, N'dustin.dahl@gmail.com', N'801.243.1675', 0)
INSERT [dbo].[ContactInfos] ([ContactInfoId], [UserId], [Type], [Name], [CompanyName], [Email], [PhoneNumber], [DisplayPhoneNumber]) VALUES (12, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', N'Personal', N'Bob Barker', NULL, N'bob.barker@gmail.com', N'8012431675', 0)
SET IDENTITY_INSERT [dbo].[ContactInfos] OFF
/****** Object:  Table [dbo].[AuthTokens]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthTokens](
	[Token] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RequireLogin] [bit] NOT NULL,
	[ApiKey] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_AuthTokens] PRIMARY KEY CLUSTERED 
(
	[Token] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[ApplicationInfo]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationInfo](
	[UserId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](250) NULL,
	[LastName] [nvarchar](250) NULL,
	[Ssn] [nvarchar](50) NULL,
	[PresentPhone] [nvarchar](50) NULL,
	[PresentAddressLine1] [nvarchar](250) NULL,
	[PresentAddressLine2] [nvarchar](250) NULL,
	[PresentCity] [nvarchar](250) NULL,
	[PresentState] [nvarchar](50) NULL,
	[PresentZip] [nvarchar](10) NULL,
	[PresentLandlord] [nvarchar](50) NULL,
	[PresentLandlordPhone] [nvarchar](50) NULL,
	[PreviousAddressLine1] [nvarchar](250) NULL,
	[PreviousAddressLine2] [nvarchar](250) NULL,
	[PreviousCity] [nvarchar](250) NULL,
	[PreviousState] [nvarchar](50) NULL,
	[PreviousZip] [nvarchar](10) NULL,
	[PreviousLandlord] [nvarchar](50) NULL,
	[PreviousLandlordPhone] [nvarchar](50) NULL,
	[PresentEmployer] [nvarchar](50) NULL,
	[PresentEmployerPhone] [nvarchar](50) NULL,
	[PreviousEmployer] [nvarchar](50) NULL,
	[PreviousEmployerPhone] [nvarchar](50) NULL,
	[EmergencyContact] [nvarchar](50) NULL,
	[EmergencyContactPhone] [nvarchar](50) NULL,
	[EmergencyContactAddressLine1] [nvarchar](250) NULL,
	[EmergencyContactAddressLine2] [nvarchar](250) NULL,
	[EmergencyContactCity] [nvarchar](250) NULL,
	[EmergencyContactState] [nvarchar](50) NULL,
	[EmergencyContactZip] [nvarchar](10) NULL,
	[VehicleYear] [int] NULL,
	[VehicleMake] [nvarchar](50) NULL,
	[VehicleModel] [nvarchar](50) NULL,
	[VehicleColor] [nvarchar](50) NULL,
	[VehicleLicenseNumber] [nvarchar](50) NULL,
	[VehicleState] [nvarchar](50) NULL,
	[HasBeenConvicted] [bit] NULL,
	[ConvictionExplanation] [nvarchar](500) NULL,
	[HasEverBeenUnlawfulDetainer] [bit] NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ApplicationInfo] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[ApplicationInfo] ([UserId], [FirstName], [LastName], [Ssn], [PresentPhone], [PresentAddressLine1], [PresentAddressLine2], [PresentCity], [PresentState], [PresentZip], [PresentLandlord], [PresentLandlordPhone], [PreviousAddressLine1], [PreviousAddressLine2], [PreviousCity], [PreviousState], [PreviousZip], [PreviousLandlord], [PreviousLandlordPhone], [PresentEmployer], [PresentEmployerPhone], [PreviousEmployer], [PreviousEmployerPhone], [EmergencyContact], [EmergencyContactPhone], [EmergencyContactAddressLine1], [EmergencyContactAddressLine2], [EmergencyContactCity], [EmergencyContactState], [EmergencyContactZip], [VehicleYear], [VehicleMake], [VehicleModel], [VehicleColor], [VehicleLicenseNumber], [VehicleState], [HasBeenConvicted], [ConvictionExplanation], [HasEverBeenUnlawfulDetainer], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x0000A031006FDC2C AS DateTime), N'web linq', 0)
/****** Object:  Table [dbo].[ApiKeys]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiKeys](
	[ApiKeyId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IpAddress] [nvarchar](15) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ApiKeys] PRIMARY KEY CLUSTERED 
(
	[ApiKeyId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[ApiKeys] ([ApiKeyId], [UserId], [Name], [IpAddress], [IsDeleted]) VALUES (N'58ad1d80-7ed5-4acd-af7e-99c6ad3b1c2f', N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', N'Ksl', N'*.*.*.*', 0)
/****** Object:  Table [dbo].[Alerts]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alerts](
	[AlertId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](250) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[BuildingId] [bigint] NULL,
	[Timestamp] [datetime] NOT NULL,
	[IsDismissed] [bit] NOT NULL,
	[ReceiveType] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Alerts] PRIMARY KEY CLUSTERED 
(
	[AlertId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Products]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
	[Price] [decimal](19, 4) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[Products] ON
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [RoleName], [IsDeleted]) VALUES (2, N'Ribbon', N'Buy a ribbon for your listing!', CAST(0.9900 AS Decimal(19, 4)), NULL, 0)
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [Price], [RoleName], [IsDeleted]) VALUES (3, N'Featured Listing', N'Feature your listing!', CAST(9.9500 AS Decimal(19, 4)), NULL, 0)
SET IDENTITY_INSERT [dbo].[Products] OFF
/****** Object:  Trigger [PhotosSortorder]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[PhotosSortorder]
on [dbo].[Photos]
instead of insert
as
 begin
  declare @buildingId bigint = ( select BuildingId
          from inserted)  
  declare @pCount int =
   (select count(*)
   from Photos p
   where p.BuildingId = @buildingId)
  
  insert into Photos
  select PhotoId, BuildingId, IsPrimary, 
    Extension, SortOrder = @pCount, 
    UpdateDate, UpdatedBy, CreateDate, 
    CreatedBy, IsDeleted
  from inserted
 end
GO
/****** Object:  Table [dbo].[AmenityOptions]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AmenityOptions](
	[AmenityId] [int] NOT NULL,
	[Option] [nvarchar](50) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_AmenityOptions] PRIMARY KEY CLUSTERED 
(
	[AmenityId] ASC,
	[Option] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (1, N'1 Car', CAST(0x00009E840158C275 AS DateTime), N'2011-02-08 20:55:13.883', CAST(0x00009E98013426F7 AS DateTime), N'dusda', 0, 0)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (1, N'2 Cars', CAST(0x00009E840158CE02 AS DateTime), N'2011-02-08 20:55:23.740', CAST(0x00009E98013428C0 AS DateTime), N'dusda', 0, 0)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (1, N'3+ Cars', CAST(0x00009E840158DA8E AS DateTime), N'2011-02-08 20:55:34.447', CAST(0x00009E98013429FA AS DateTime), N'dusda', 0, 0)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (2, N'1 Car', CAST(0x00009E840158E4EE AS DateTime), N'2011-02-08 20:55:43.300', CAST(0x00009E9801342B43 AS DateTime), N'dusda', 0, 0)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (2, N'2 Cars', CAST(0x00009E840158E9AE AS DateTime), N'2011-02-08 20:55:47.353', CAST(0x00009E9801342C87 AS DateTime), N'dusda', 0, 0)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (2, N'3+ Cars', CAST(0x00009E840158F197 AS DateTime), N'2011-02-08 20:55:54.103', CAST(0x00009E9801342DDA AS DateTime), N'dusda', 0, 0)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (3, N'Central Air', CAST(0x00009F6700FD974A AS DateTime), N'dusda', CAST(0x00009F6700FD974A AS DateTime), N'dusda', 0, 0)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (3, N'Evaporative Cooler', CAST(0x00009F6700FD974A AS DateTime), N'dusda', CAST(0x00009F6700FD974A AS DateTime), N'dusda', 0, 1)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (3, N'Forced Air', CAST(0x00009F6700FAB953 AS DateTime), N'dusda', CAST(0x00009F6700FAB953 AS DateTime), N'dusda', 0, 0)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (3, N'Other', CAST(0x00009F6700FAB953 AS DateTime), N'dusda', CAST(0x00009F6700FAB953 AS DateTime), N'dusda', 0, 5)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (4, N'Forced Air', CAST(0x00009FCC01185CC9 AS DateTime), N'dusda', CAST(0x00009FCC01185CC9 AS DateTime), N'dusda', 0, 0)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (4, N'Geothermal', CAST(0x00009F6700FAB953 AS DateTime), N'dusda', CAST(0x00009F6700FAB953 AS DateTime), N'dusda', 0, 2)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (4, N'Hydronic', CAST(0x00009F6700FAB94B AS DateTime), N'dusda', CAST(0x00009F6700FAB94B AS DateTime), N'dusda', 0, 3)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (4, N'None', CAST(0x00009F6700FD974A AS DateTime), N'dusda', CAST(0x00009F6700FD974A AS DateTime), N'dusda', 0, 4)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (4, N'Other', CAST(0x00009F6700FD974B AS DateTime), N'dusda', CAST(0x00009F6700FD974B AS DateTime), N'dusda', 0, 5)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (4, N'Radiant Heat', CAST(0x00009F6700FAB953 AS DateTime), N'dusda', CAST(0x00009F6700FAB953 AS DateTime), N'dusda', 0, 1)
INSERT [dbo].[AmenityOptions] ([AmenityId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted], [SortOrder]) VALUES (4, N'Steam Radiant', CAST(0x00009F6700FAB952 AS DateTime), N'dusda', CAST(0x00009F6700FAB952 AS DateTime), N'dusda', 0, 0)
/****** Object:  Table [dbo].[Orders]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserCreditCardId] [int] NOT NULL,
	[PurchaseDate] [datetime] NOT NULL,
	[CancelDate] [datetime] NULL,
	[Status] [nvarchar](300) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[Orders] ON
INSERT [dbo].[Orders] ([OrderId], [UserId], [UserCreditCardId], [PurchaseDate], [CancelDate], [Status], [IsDeleted]) VALUES (4, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', 3, CAST(0x0000A03D00334596 AS DateTime), NULL, N'CardDeclined', 0)
INSERT [dbo].[Orders] ([OrderId], [UserId], [UserCreditCardId], [PurchaseDate], [CancelDate], [Status], [IsDeleted]) VALUES (5, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', 3, CAST(0x0000A03D0037E012 AS DateTime), NULL, N'CardDeclined', 0)
SET IDENTITY_INSERT [dbo].[Orders] OFF
/****** Object:  Table [dbo].[AffiliateUsers]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AffiliateUsers](
	[UserId] [uniqueidentifier] NOT NULL,
	[AffiliateUserKey] [nvarchar](50) NOT NULL,
	[ApiKey] [uniqueidentifier] NOT NULL,
	[PasswordHash] [nvarchar](250) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_AffiliateUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[AffiliateUsers] ([UserId], [AffiliateUserKey], [ApiKey], [PasswordHash], [IsDeleted]) VALUES (N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', N'0', N'58ad1d80-7ed5-4acd-af7e-99c6ad3b1c2f', NULL, 0)
/****** Object:  Table [dbo].[Answers]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[AnswerId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Text] [nvarchar](50) NOT NULL,
	[ContractText] [nvarchar](1000) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Buildings]    Script Date: 04/26/2012 16:35:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buildings](
	[BuildingId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RibbonId] [int] NULL,
	[ListingType] [nvarchar](50) NOT NULL,
	[ContactInfoId] [bigint] NOT NULL,
	[Address1] [nvarchar](250) NOT NULL,
	[Address2] [nvarchar](250) NULL,
	[City] [nvarchar](250) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[Zip] [nvarchar](10) NOT NULL,
	[Latitude] [nvarchar](50) NULL,
	[Longitude] [nvarchar](50) NULL,
	[PropertyType] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](4000) NULL,
	[SquareFeet] [int] NOT NULL,
	[Acres] [decimal](19, 4) NOT NULL,
	[YearBuilt] [int] NOT NULL,
	[Bedrooms] [int] NOT NULL,
	[Bathrooms] [decimal](19, 4) NOT NULL,
	[Price] [decimal](19, 4) NULL,
	[DateAvailable] [datetime] NULL,
	[DateActivated] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[PrimaryPhotoId] [uniqueidentifier] NULL,
	[PrimaryPhotoExtension] [nvarchar](4) NULL,
	[CreditCheckRequired] [bit] NOT NULL,
	[BackgroundCheckRequired] [bit] NOT NULL,
	[Deposit] [decimal](19, 4) NULL,
	[RefundableDeposit] [decimal](19, 4) NULL,
	[LeaseLength] [nvarchar](50) NULL,
	[SmokingAllowed] [bit] NOT NULL,
	[PetsAllowed] [bit] NULL,
	[PetFee] [decimal](19, 4) NULL,
	[Power] [bit] NULL,
	[Gas] [bit] NULL,
	[Water] [bit] NULL,
	[Sewer] [bit] NULL,
	[Garbage] [bit] NULL,
	[HOA] [bit] NULL,
	[Internet] [bit] NULL,
	[CableSatellite] [bit] NULL,
	[RemovedByAdmin] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
 CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[Buildings] ON
INSERT [dbo].[Buildings] ([BuildingId], [UserId], [RibbonId], [ListingType], [ContactInfoId], [Address1], [Address2], [City], [State], [Zip], [Latitude], [Longitude], [PropertyType], [Description], [SquareFeet], [Acres], [YearBuilt], [Bedrooms], [Bathrooms], [Price], [DateAvailable], [DateActivated], [IsActive], [PrimaryPhotoId], [PrimaryPhotoExtension], [CreditCheckRequired], [BackgroundCheckRequired], [Deposit], [RefundableDeposit], [LeaseLength], [SmokingAllowed], [PetsAllowed], [PetFee], [Power], [Gas], [Water], [Sewer], [Garbage], [HOA], [Internet], [CableSatellite], [RemovedByAdmin], [IsDeleted], [CreateDate], [CreatedBy], [UpdateDate], [UpdatedBy]) VALUES (4, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', NULL, N'Personal', 12, N'5610 Goodway Dr', NULL, N'Murray', N'UT', N'84123', NULL, NULL, N'Single-Family Home', NULL, 900, CAST(0.0000 AS Decimal(19, 4)), 2002, 3, CAST(2.5000 AS Decimal(19, 4)), CAST(900.0000 AS Decimal(19, 4)), CAST(0x0000A03F00000000 AS DateTime), NULL, 1, N'4cbaa5d4-db65-44dd-a44f-97dee8ff6362', N'jpg', 1, 1, CAST(900.0000 AS Decimal(19, 4)), CAST(900.0000 AS Decimal(19, 4)), N'6 Months', 0, 0, CAST(90.0000 AS Decimal(19, 4)), 1, 1, 0, 1, 1, NULL, 0, NULL, 0, 0, CAST(0x0000A03500346F34 AS DateTime), N'web linq', CAST(0x0000A03D017392A8 AS DateTime), N'web linq')
INSERT [dbo].[Buildings] ([BuildingId], [UserId], [RibbonId], [ListingType], [ContactInfoId], [Address1], [Address2], [City], [State], [Zip], [Latitude], [Longitude], [PropertyType], [Description], [SquareFeet], [Acres], [YearBuilt], [Bedrooms], [Bathrooms], [Price], [DateAvailable], [DateActivated], [IsActive], [PrimaryPhotoId], [PrimaryPhotoExtension], [CreditCheckRequired], [BackgroundCheckRequired], [Deposit], [RefundableDeposit], [LeaseLength], [SmokingAllowed], [PetsAllowed], [PetFee], [Power], [Gas], [Water], [Sewer], [Garbage], [HOA], [Internet], [CableSatellite], [RemovedByAdmin], [IsDeleted], [CreateDate], [CreatedBy], [UpdateDate], [UpdatedBy]) VALUES (5, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', NULL, N'Personal', 6, N'650 W S Temple', NULL, N'Murray', N'UT', N'84123', NULL, NULL, N'Single-Family Home', NULL, 900, CAST(0.0000 AS Decimal(19, 4)), 2002, 3, CAST(2.0000 AS Decimal(19, 4)), CAST(900.0000 AS Decimal(19, 4)), CAST(0x0000A03500000000 AS DateTime), NULL, 0, N'0026ab42-a26c-46b2-ae3b-807338c71405', N'jpg', 0, 0, NULL, NULL, N'Month to Month', 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, CAST(0x0000A03600005F9C AS DateTime), N'web linq', CAST(0x0000A03D016DB34A AS DateTime), N'web linq')
INSERT [dbo].[Buildings] ([BuildingId], [UserId], [RibbonId], [ListingType], [ContactInfoId], [Address1], [Address2], [City], [State], [Zip], [Latitude], [Longitude], [PropertyType], [Description], [SquareFeet], [Acres], [YearBuilt], [Bedrooms], [Bathrooms], [Price], [DateAvailable], [DateActivated], [IsActive], [PrimaryPhotoId], [PrimaryPhotoExtension], [CreditCheckRequired], [BackgroundCheckRequired], [Deposit], [RefundableDeposit], [LeaseLength], [SmokingAllowed], [PetsAllowed], [PetFee], [Power], [Gas], [Water], [Sewer], [Garbage], [HOA], [Internet], [CableSatellite], [RemovedByAdmin], [IsDeleted], [CreateDate], [CreatedBy], [UpdateDate], [UpdatedBy]) VALUES (6, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', NULL, N'Personal', 6, N'5610 Goodway Dr', NULL, N'Murray', N'UT', N'84123', NULL, NULL, N'Single-Family Home', NULL, 900, CAST(0.0000 AS Decimal(19, 4)), 2002, 3, CAST(2.2500 AS Decimal(19, 4)), CAST(900.0000 AS Decimal(19, 4)), CAST(0x0000A03A00000000 AS DateTime), NULL, 0, N'b3f161cb-78b9-402a-924b-f6272132e2a3', N'jpg', 0, 0, NULL, NULL, N'Month to Month', 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, CAST(0x0000A03B003BFB4B AS DateTime), N'web linq', CAST(0x0000A03D0169D45C AS DateTime), N'web linq')
INSERT [dbo].[Buildings] ([BuildingId], [UserId], [RibbonId], [ListingType], [ContactInfoId], [Address1], [Address2], [City], [State], [Zip], [Latitude], [Longitude], [PropertyType], [Description], [SquareFeet], [Acres], [YearBuilt], [Bedrooms], [Bathrooms], [Price], [DateAvailable], [DateActivated], [IsActive], [PrimaryPhotoId], [PrimaryPhotoExtension], [CreditCheckRequired], [BackgroundCheckRequired], [Deposit], [RefundableDeposit], [LeaseLength], [SmokingAllowed], [PetsAllowed], [PetFee], [Power], [Gas], [Water], [Sewer], [Garbage], [HOA], [Internet], [CableSatellite], [RemovedByAdmin], [IsDeleted], [CreateDate], [CreatedBy], [UpdateDate], [UpdatedBy]) VALUES (7, N'b9c94fda-6b56-40a6-aef6-18ed3e326b7a', NULL, N'Personal', 6, N'5640 Goodway Dr', NULL, N'Murray', N'UT', N'84123', NULL, NULL, N'Single-Family Home', NULL, 900, CAST(0.0000 AS Decimal(19, 4)), 2002, 3, CAST(2.0000 AS Decimal(19, 4)), CAST(900.0000 AS Decimal(19, 4)), CAST(0x0000A03D00000000 AS DateTime), NULL, 0, N'926c36f1-d507-4751-a2de-6978c8fc006d', N'jpg', 0, 0, NULL, NULL, N'Month to Month', 0, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, CAST(0x0000A03D016E6ED4 AS DateTime), N'web linq', CAST(0x0000A03F000F1643 AS DateTime), N'web linq')
SET IDENTITY_INSERT [dbo].[Buildings] OFF
/****** Object:  Table [dbo].[Subscriptions]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscriptions](
	[SubscriptionId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserCreditCardId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDisputed] [bit] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[NextChargeDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Subscriptions] PRIMARY KEY CLUSTERED 
(
	[SubscriptionId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[UserContractAnswers]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserContractAnswers](
	[UserContractAnswersId] [int] IDENTITY(1,1) NOT NULL,
	[UserContractId] [int] NOT NULL,
	[AnswerId] [int] NOT NULL,
	[CustomText] [nvarchar](1000) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_UserContractAnswers] PRIMARY KEY CLUSTERED 
(
	[UserContractAnswersId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[SubscriptionOrders]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionOrders](
	[SubscriptionId] [bigint] NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_SubscriptionOrders] PRIMARY KEY CLUSTERED 
(
	[SubscriptionId] ASC,
	[OrderId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[SavedBuildings]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SavedBuildings](
	[UserId] [uniqueidentifier] NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_SavedBuildings] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[BuildingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[PropertyTransactions]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTransactions](
	[PropertyTransactionId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[Amount] [decimal](19, 4) NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Generated] [bit] NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_PropertyTransactions] PRIMARY KEY CLUSTERED 
(
	[PropertyTransactionId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[UserInterests]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInterests](
	[UserInterestId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[LandlordUserId] [uniqueidentifier] NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[ShowApplicationInfo] [bit] NOT NULL,
	[AllowBackgroundCheck] [bit] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Message] [nvarchar](1000) NULL,
	[ResponseMessage] [nvarchar](1000) NULL,
 CONSTRAINT [PK_UserInterests] PRIMARY KEY CLUSTERED 
(
	[UserInterestId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[BuildingCounts]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuildingCounts](
	[BuildingId] [bigint] NOT NULL,
	[SearchCount] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
 CONSTRAINT [PK_BuildingCounts] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[BuildingCounts] ([BuildingId], [SearchCount], [ViewCount]) VALUES (4, 86, 10)
INSERT [dbo].[BuildingCounts] ([BuildingId], [SearchCount], [ViewCount]) VALUES (5, 79, 0)
INSERT [dbo].[BuildingCounts] ([BuildingId], [SearchCount], [ViewCount]) VALUES (6, 70, 0)
INSERT [dbo].[BuildingCounts] ([BuildingId], [SearchCount], [ViewCount]) VALUES (7, 13, 0)
/****** Object:  Table [dbo].[BuildingAmenitiesWithOptions]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuildingAmenitiesWithOptions](
	[AmenityId] [int] NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[Option] [nvarchar](50) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_HomeAmenitiesWithOptions] PRIMARY KEY CLUSTERED 
(
	[AmenityId] ASC,
	[BuildingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[BuildingAmenitiesWithOptions] ([AmenityId], [BuildingId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (1, 4, N'2 Cars', NULL, NULL, CAST(0x0000A03C00542824 AS DateTime), N'linq', 0)
INSERT [dbo].[BuildingAmenitiesWithOptions] ([AmenityId], [BuildingId], [Option], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (3, 4, N'Other', NULL, NULL, CAST(0x0000A03C00542824 AS DateTime), N'linq', 0)
/****** Object:  Table [dbo].[BuildingAmenities]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuildingAmenities](
	[AmenityId] [int] NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_HomeAmenities] PRIMARY KEY CLUSTERED 
(
	[AmenityId] ASC,
	[BuildingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[BuildingAmenities] ([AmenityId], [BuildingId], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (3, 4, NULL, NULL, CAST(0x0000A03C00542824 AS DateTime), N'linq', 0)
INSERT [dbo].[BuildingAmenities] ([AmenityId], [BuildingId], [UpdateDate], [UpdatedBy], [CreateDate], [CreatedBy], [IsDeleted]) VALUES (6, 4, NULL, NULL, CAST(0x0000A03C00542824 AS DateTime), N'linq', 0)
/****** Object:  Table [dbo].[CustomAmenities]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomAmenities](
	[AmenityId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_CustomAmenities] PRIMARY KEY CLUSTERED 
(
	[AmenityId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[ReservedRibbons]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservedRibbons](
	[BuildingId] [bigint] NOT NULL,
	[RibbonId] [int] NOT NULL,
 CONSTRAINT [PK_ReservedRibbons] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[ReservedRibbons] ([BuildingId], [RibbonId]) VALUES (4, 5)
INSERT [dbo].[ReservedRibbons] ([BuildingId], [RibbonId]) VALUES (7, 5)
/****** Object:  Table [dbo].[ReservedFeaturedListings]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservedFeaturedListings](
	[BuildingId] [bigint] NOT NULL,
	[ScheduledDate] [datetime] NOT NULL,
	[FeaturedSectionId] [int] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ReservedFeaturedListings] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC,
	[ScheduledDate] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[ReportedBuildings]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportedBuildings](
	[ReportedBuildingId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[ReportContent] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_ReportedBuildings] PRIMARY KEY CLUSTERED 
(
	[ReportedBuildingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Leases]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leases](
	[LeaseId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](255) NULL,
	[Phone] [nvarchar](100) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[PaymentDay] [int] NOT NULL,
	[Amount] [decimal](19, 4) NOT NULL,
	[LateFee] [decimal](19, 4) NOT NULL,
	[SecurityDeposit] [decimal](19, 4) NOT NULL,
	[PetRent] [decimal](19, 4) NOT NULL,
	[PetDeposit] [decimal](19, 4) NOT NULL,
	[CleaningDeposit] [decimal](19, 4) NOT NULL,
	[AchEnabled] [bit] NOT NULL,
	[TenantBankId] [int] NULL,
	[LandlordBankId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[Duration] [int] NOT NULL,
	[GracePeriod] [int] NOT NULL,
	[ContinuesMonthly] [bit] NOT NULL,
 CONSTRAINT [PK_Leases] PRIMARY KEY CLUSTERED 
(
	[LeaseId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[FeaturedListings]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeaturedListings](
	[BuildingId] [bigint] NOT NULL,
	[ScheduledDate] [datetime] NOT NULL,
	[FeaturedSectionId] [int] NOT NULL,
 CONSTRAINT [PK_FeaturedListings] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC,
	[ScheduledDate] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (4, CAST(0x0000A03D01745D54 AS DateTime), 2)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (4, CAST(0x0000A03E0062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (4, CAST(0x0000A03F0062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (4, CAST(0x0000A0400062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (4, CAST(0x0000A0410062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (4, CAST(0x0000A0420062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (5, CAST(0x0000A03E0062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (5, CAST(0x0000A03F0062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (5, CAST(0x0000A0400062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (5, CAST(0x0000A0410062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (5, CAST(0x0000A0420062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (6, CAST(0x0000A03E0062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (6, CAST(0x0000A03F0062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (6, CAST(0x0000A0400062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (6, CAST(0x0000A0410062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (6, CAST(0x0000A0420062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (7, CAST(0x0000A03E0062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (7, CAST(0x0000A03F0062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (7, CAST(0x0000A0400062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (7, CAST(0x0000A0410062E080 AS DateTime), 17)
INSERT [dbo].[FeaturedListings] ([BuildingId], [ScheduledDate], [FeaturedSectionId]) VALUES (7, CAST(0x0000A0420062E080 AS DateTime), 17)
/****** Object:  Table [dbo].[LandlordAlerts]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LandlordAlerts](
	[AlertId] [bigint] IDENTITY(1,1) NOT NULL,
	[LandlordId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[Message] [nvarchar](100) NULL,
	[BuildingId] [bigint] NULL,
	[CreateDateUtc] [datetime] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_LandlordAlerts] PRIMARY KEY CLUSTERED 
(
	[AlertId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[OrderItemId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[OrderItems] ON
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [ProductId], [Quantity], [IsDeleted]) VALUES (4, 4, 2, 1, 0)
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [ProductId], [Quantity], [IsDeleted]) VALUES (5, 4, 3, 3, 0)
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [ProductId], [Quantity], [IsDeleted]) VALUES (6, 5, 2, 1, 0)
INSERT [dbo].[OrderItems] ([OrderItemId], [OrderId], [ProductId], [Quantity], [IsDeleted]) VALUES (7, 5, 3, 3, 0)
SET IDENTITY_INSERT [dbo].[OrderItems] OFF
/****** Object:  Table [dbo].[LeaseTransactions]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaseTransactions](
	[LeaseTransactionId] [bigint] IDENTITY(1,1) NOT NULL,
	[LeaseId] [int] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[ResultDate] [datetime] NULL,
	[Amount] [decimal](19, 4) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_LeaseTransactions] PRIMARY KEY CLUSTERED 
(
	[LeaseTransactionId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[LeaseTenants]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaseTenants](
	[LeaseTenantId] [int] IDENTITY(1,1) NOT NULL,
	[LeaseId] [int] NOT NULL,
	[TenantName] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_LeaseTenants] PRIMARY KEY CLUSTERED 
(
	[LeaseTenantId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[LeasePaymentItems]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeasePaymentItems](
	[LeasePaymentItemId] [int] IDENTITY(1,1) NOT NULL,
	[LeaseId] [int] NOT NULL,
	[DueDateUtc] [datetime] NOT NULL,
	[Rent] [decimal](18, 0) NOT NULL,
	[PayDateUtc] [datetime] NULL,
	[LateFee] [decimal](18, 0) NOT NULL,
	[TotalDue] [decimal](18, 0) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_LeasePaymentItems] PRIMARY KEY CLUSTERED 
(
	[LeasePaymentItemId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[LeaseContracts]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaseContracts](
	[LeaseContractId] [int] IDENTITY(1,1) NOT NULL,
	[LeaseId] [int] NOT NULL,
	[PostDateUtc] [datetime] NOT NULL,
	[FinalText] [nvarchar](max) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_LeaseContracts] PRIMARY KEY CLUSTERED 
(
	[LeaseContractId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[LeasePaymentPropertyTransactions]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeasePaymentPropertyTransactions](
	[LeasePaymentItemPropertyTransactionId] [int] IDENTITY(1,1) NOT NULL,
	[LeasePaymentItemId] [int] NOT NULL,
	[PropertyTransactionId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_LeasePaymentLeaseTransactions] PRIMARY KEY CLUSTERED 
(
	[LeasePaymentItemPropertyTransactionId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[AchFromTenantTransactions]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AchFromTenantTransactions](
	[AchFromTenantTransactionId] [bigint] IDENTITY(1,1) NOT NULL,
	[LeaseTransactionId] [bigint] NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[ResultDate] [datetime] NULL,
	[Alias] [bigint] NOT NULL,
	[TransactionId] [bigint] NOT NULL,
	[Amount] [decimal](19, 4) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_AchFromTenantTransactions] PRIMARY KEY CLUSTERED 
(
	[AchFromTenantTransactionId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[AchToLandlordTransactions]    Script Date: 04/26/2012 16:35:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AchToLandlordTransactions](
	[AchToLandlordTransactionId] [bigint] IDENTITY(1,1) NOT NULL,
	[AchFromTenantTransactionId] [bigint] NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[ResultDate] [datetime] NULL,
	[Alias] [bigint] NOT NULL,
	[TransactionId] [bigint] NULL,
	[Amount] [decimal](19, 4) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_AchToLandlordTransactions] PRIMARY KEY CLUSTERED 
(
	[AchToLandlordTransactionId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Default [DF__Photos__CreateDa__693CA210]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Photos] ADD  CONSTRAINT [DF__Photos__CreateDa__693CA210]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__Photos__CreatedB__6A30C649]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Photos] ADD  CONSTRAINT [DF__Photos__CreatedB__6A30C649]  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__Photos__IsDelete__72C60C4A]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Photos] ADD  CONSTRAINT [DF__Photos__IsDelete__72C60C4A]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Roles__IsDeleted__589C25F3]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Roles] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Amenities__Creat__44FF419A]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AmenitiesWithOptions] ADD  CONSTRAINT [DF__Amenities__Creat__44FF419A]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__Amenities__Creat__45F365D3]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AmenitiesWithOptions] ADD  CONSTRAINT [DF__Amenities__Creat__45F365D3]  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__Amenities__IsDel__09DE7BCC]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AmenitiesWithOptions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Amenities__Creat__46E78A0C]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Amenities] ADD  CONSTRAINT [DF__Amenities__Creat__46E78A0C]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__Amenities__Creat__47DBAE45]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Amenities] ADD  CONSTRAINT [DF__Amenities__Creat__47DBAE45]  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__Amenities__IsDel__0519C6AF]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Amenities] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__EvictionN__IsDel__1699586C]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[EvictionNotices] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Contracts__Creat__6442E2C9]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Contracts] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__Contracts__Creat__65370702]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Contracts] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__Contracts__IsDel__662B2B3B]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Contracts] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__ZipInfos__IsDele__619B8048]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[ZipInfos] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Users__CreateDat__4AB81AF0]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__CreateDat__4AB81AF0]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__Users__CreatedBy__4BAC3F29]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__CreatedBy__4BAC3F29]  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__Users__IsLandlor__34C8D9D1]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsLandlord]
GO
/****** Object:  Default [DF__Users__IsDeleted__35BCFE0A]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__RoleUsers__IsDel__5D60DB10]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[RoleUsers] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Products__IsDele__61316BF4]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_UserCreditCards_IsDeleted]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserCreditCards] ADD  CONSTRAINT [DF_UserCreditCards_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_UserCreditCards_CreateDate]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserCreditCards] ADD  CONSTRAINT [DF_UserCreditCards_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_UserCreditCards_CreatedBy]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserCreditCards] ADD  CONSTRAINT [DF_UserCreditCards_CreatedBy]  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__UserContr__Creat__6AEFE058]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserContracts] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__UserContr__Creat__6BE40491]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserContracts] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__UserContr__IsDel__6CD828CA]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserContracts] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_UserBanks_IsDeleted]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserBanks] ADD  CONSTRAINT [DF_UserBanks_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_UserBanks_CreateDate]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserBanks] ADD  CONSTRAINT [DF_UserBanks_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF_UserBanks_CreatedBy]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserBanks] ADD  CONSTRAINT [DF_UserBanks_CreatedBy]  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__AuthToken__IsDel__534D60F1]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AuthTokens] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Applicati__Creat__4BAC3F29]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[ApplicationInfo] ADD  CONSTRAINT [DF__Applicati__Creat__4BAC3F29]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__Applicati__Creat__4CA06362]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[ApplicationInfo] ADD  CONSTRAINT [DF__Applicati__Creat__4CA06362]  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__Applicati__IsDel__59063A47]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[ApplicationInfo] ADD  CONSTRAINT [DF__Applicati__IsDel__59063A47]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__ApiKeys__IsDelet__4AB81AF0]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[ApiKeys] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Alerts_IsDismissed]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Alerts] ADD  CONSTRAINT [DF_Alerts_IsDismissed]  DEFAULT ((0)) FOR [IsDismissed]
GO
/****** Object:  Default [DF__Alerts__IsDelete__7720AD13]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Alerts] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Questions__Creat__7AF13DF7]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Questions] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__Questions__Creat__7BE56230]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__Questions__IsDel__7CD98669]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__AmenityOp__Creat__0DAF0CB0]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AmenityOptions] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__AmenityOp__Creat__0EA330E9]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AmenityOptions] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__AmenityOp__IsDel__0F975522]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AmenityOptions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__AmenityOp__SortO__108B795B]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AmenityOptions] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
/****** Object:  Default [DF__Orders__IsDelete__6BAEFA67]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Affiliate__IsDel__4F7CD00D]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AffiliateUsers] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Answers__CreateD__00AA174D]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Answers] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__Answers__Created__019E3B86]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Answers] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__Answers__IsDelet__02925FBF]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Answers] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Buildings_IsActive]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Buildings] ADD  CONSTRAINT [DF_Buildings_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
/****** Object:  Default [DF_Buildings_CreditCheckRequired]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Buildings] ADD  CONSTRAINT [DF_Buildings_CreditCheckRequired]  DEFAULT ((0)) FOR [CreditCheckRequired]
GO
/****** Object:  Default [DF_Buildings_BackgroundCheckRequired]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Buildings] ADD  CONSTRAINT [DF_Buildings_BackgroundCheckRequired]  DEFAULT ((0)) FOR [BackgroundCheckRequired]
GO
/****** Object:  Default [DF_Buildings_SmokingAllowed]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Buildings] ADD  CONSTRAINT [DF_Buildings_SmokingAllowed]  DEFAULT ((0)) FOR [SmokingAllowed]
GO
/****** Object:  Default [DF_Buildings_RemovedByAdmin]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Buildings] ADD  CONSTRAINT [DF_Buildings_RemovedByAdmin]  DEFAULT ((0)) FOR [RemovedByAdmin]
GO
/****** Object:  Default [DF_Buildings_IsDeleted]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Buildings] ADD  CONSTRAINT [DF_Buildings_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Subscript__IsDel__66EA454A]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Subscriptions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__UserContr__Creat__075714DC]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[UserContractAnswers] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__UserContr__Creat__084B3915]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[UserContractAnswers] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__UserContr__IsDel__093F5D4E]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[UserContractAnswers] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Subscript__IsDel__1F2E9E6D]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[SubscriptionOrders] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__UserInter__Creat__72C60C4A]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[UserInterests] ADD  CONSTRAINT [DF__UserInter__Creat__72C60C4A]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__UserInter__Creat__73BA3083]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[UserInterests] ADD  CONSTRAINT [DF__UserInter__Creat__73BA3083]  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__UserInter__IsDel__7A672E12]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[UserInterests] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__BuildingA__Creat__21B6055D]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingAmenitiesWithOptions] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__BuildingA__Creat__22AA2996]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingAmenitiesWithOptions] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__BuildingA__IsDel__239E4DCF]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingAmenitiesWithOptions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__BuildingA__Creat__1B0907CE]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingAmenities] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__BuildingA__Creat__1BFD2C07]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingAmenities] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__BuildingA__IsDel__1CF15040]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingAmenities] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__CustomAme__Creat__145C0A3F]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[CustomAmenities] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__CustomAme__Creat__15502E78]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[CustomAmenities] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__CustomAme__IsDel__164452B1]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[CustomAmenities] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__PropertyT__Creat__57DD0BE4]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[PropertyTransactions] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__PropertyT__Creat__58D1301D]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[PropertyTransactions] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__PropertyT__IsDel__59C55456]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[PropertyTransactions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_Leases_IsActive]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases] ADD  CONSTRAINT [DF_Leases_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_Leases_IsDeleted]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases] ADD  CONSTRAINT [DF_Leases_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__Leases__CreateDa__66603565]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases] ADD  CONSTRAINT [DF__Leases__CreateDa__66603565]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__Leases__CreatedB__6754599E]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases] ADD  CONSTRAINT [DF__Leases__CreatedB__6754599E]  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF_Leases_Duration]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases] ADD  CONSTRAINT [DF_Leases_Duration]  DEFAULT ((6)) FOR [Duration]
GO
/****** Object:  Default [DF_Leases_GracePeriod]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases] ADD  CONSTRAINT [DF_Leases_GracePeriod]  DEFAULT ((4)) FOR [GracePeriod]
GO
/****** Object:  Default [DF_Leases_ContinuesMonthly]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases] ADD  CONSTRAINT [DF_Leases_ContinuesMonthly]  DEFAULT ((0)) FOR [ContinuesMonthly]
GO
/****** Object:  Default [DF_LandlordAlerts_UserId]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LandlordAlerts] ADD  CONSTRAINT [DF_LandlordAlerts_UserId]  DEFAULT (NULL) FOR [UserId]
GO
/****** Object:  Default [DF_LandlordAlerts_IsRead]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LandlordAlerts] ADD  CONSTRAINT [DF_LandlordAlerts_IsRead]  DEFAULT ((0)) FOR [IsRead]
GO
/****** Object:  Default [DF__LandlordA__IsDel__367C1819]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LandlordAlerts] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__OrderItem__IsDel__7073AF84]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[OrderItems] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF_LeaseTransactions_CreateDate]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeaseTransactions] ADD  CONSTRAINT [DF_LeaseTransactions_CreateDate]  DEFAULT (getutcdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__LeaseTran__IsDel__55009F39]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeaseTransactions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__LeaseTena__IsDel__503BEA1C]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeaseTenants] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__LeasePaym__IsDel__4C6B5938]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeasePaymentItems] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__LeaseCont__Creat__46B27FE2]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeaseContracts] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  Default [DF__LeaseCont__Creat__47A6A41B]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeaseContracts] ADD  DEFAULT ('sql script') FOR [CreatedBy]
GO
/****** Object:  Default [DF__LeaseCont__IsDel__489AC854]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeaseContracts] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__LeasePaym__IsDel__0E04126B]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeasePaymentPropertyTransactions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__AchFromTe__IsDel__11D4A34F]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[AchFromTenantTransactions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  Default [DF__AchToLand__IsDel__1A69E950]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[AchToLandlordTransactions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
/****** Object:  ForeignKey [FK_RoleUsers_Roles1]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK_RoleUsers_Roles1] FOREIGN KEY([RoleName])
REFERENCES [dbo].[Roles] ([RoleName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK_RoleUsers_Roles1]
GO
/****** Object:  ForeignKey [FK_RoleUsers_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[RoleUsers]  WITH CHECK ADD  CONSTRAINT [FK_RoleUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleUsers] CHECK CONSTRAINT [FK_RoleUsers_Users]
GO
/****** Object:  ForeignKey [FK_Products_Roles]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Roles] FOREIGN KEY([RoleName])
REFERENCES [dbo].[Roles] ([RoleName])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Roles]
GO
/****** Object:  ForeignKey [FK_UserCreditCards_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserCreditCards]  WITH CHECK ADD  CONSTRAINT [FK_UserCreditCards_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserCreditCards] CHECK CONSTRAINT [FK_UserCreditCards_Users]
GO
/****** Object:  ForeignKey [FK_UserContracts_Contracts]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserContracts]  WITH NOCHECK ADD  CONSTRAINT [FK_UserContracts_Contracts] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contracts] ([ContractId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserContracts] CHECK CONSTRAINT [FK_UserContracts_Contracts]
GO
/****** Object:  ForeignKey [FK_UserContracts_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserContracts]  WITH NOCHECK ADD  CONSTRAINT [FK_UserContracts_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserContracts] CHECK CONSTRAINT [FK_UserContracts_Users]
GO
/****** Object:  ForeignKey [FK_UserBanks_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[UserBanks]  WITH CHECK ADD  CONSTRAINT [FK_UserBanks_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserBanks] CHECK CONSTRAINT [FK_UserBanks_Users]
GO
/****** Object:  ForeignKey [FK_ContactInfos_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[ContactInfos]  WITH CHECK ADD  CONSTRAINT [FK_ContactInfos_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactInfos] CHECK CONSTRAINT [FK_ContactInfos_Users]
GO
/****** Object:  ForeignKey [FK_AuthTokens_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AuthTokens]  WITH CHECK ADD  CONSTRAINT [FK_AuthTokens_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AuthTokens] CHECK CONSTRAINT [FK_AuthTokens_Users]
GO
/****** Object:  ForeignKey [FK_ApplicationInfo_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[ApplicationInfo]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationInfo_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ApplicationInfo] CHECK CONSTRAINT [FK_ApplicationInfo_Users]
GO
/****** Object:  ForeignKey [FK_ApiKeys_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[ApiKeys]  WITH CHECK ADD  CONSTRAINT [FK_ApiKeys_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ApiKeys] CHECK CONSTRAINT [FK_ApiKeys_Users]
GO
/****** Object:  ForeignKey [FK_Alerts_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Alerts]  WITH CHECK ADD  CONSTRAINT [FK_Alerts_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Alerts] CHECK CONSTRAINT [FK_Alerts_Users]
GO
/****** Object:  ForeignKey [FK_ContractQuestions_Contracts]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_ContractQuestions_Contracts] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contracts] ([ContractId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_ContractQuestions_Contracts]
GO
/****** Object:  ForeignKey [FK_AmenityOptions_AmenitiesWithOptions]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AmenityOptions]  WITH NOCHECK ADD  CONSTRAINT [FK_AmenityOptions_AmenitiesWithOptions] FOREIGN KEY([AmenityId])
REFERENCES [dbo].[AmenitiesWithOptions] ([AmenityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AmenityOptions] CHECK CONSTRAINT [FK_AmenityOptions_AmenitiesWithOptions]
GO
/****** Object:  ForeignKey [FK_Orders_UserCreditCards]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_UserCreditCards] FOREIGN KEY([UserCreditCardId])
REFERENCES [dbo].[UserCreditCards] ([UserCreditCardId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_UserCreditCards]
GO
/****** Object:  ForeignKey [FK_Orders_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
/****** Object:  ForeignKey [FK_AffiliateUsers_ApiKeys]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AffiliateUsers]  WITH CHECK ADD  CONSTRAINT [FK_AffiliateUsers_ApiKeys] FOREIGN KEY([ApiKey])
REFERENCES [dbo].[ApiKeys] ([ApiKeyId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AffiliateUsers] CHECK CONSTRAINT [FK_AffiliateUsers_ApiKeys]
GO
/****** Object:  ForeignKey [FK_AffiliateUsers_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[AffiliateUsers]  WITH CHECK ADD  CONSTRAINT [FK_AffiliateUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AffiliateUsers] CHECK CONSTRAINT [FK_AffiliateUsers_Users]
GO
/****** Object:  ForeignKey [FK_Answers_Questions]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Questions] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([QuestionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Questions]
GO
/****** Object:  ForeignKey [FK_Buildings_ContactInfos]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_ContactInfos] FOREIGN KEY([ContactInfoId])
REFERENCES [dbo].[ContactInfos] ([ContactInfoId])
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_ContactInfos]
GO
/****** Object:  ForeignKey [FK_Buildings_Photos]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_Photos] FOREIGN KEY([PrimaryPhotoId])
REFERENCES [dbo].[Photos] ([PhotoId])
ON UPDATE SET NULL
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_Photos]
GO
/****** Object:  ForeignKey [FK_Buildings_Ribbons]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_Ribbons] FOREIGN KEY([RibbonId])
REFERENCES [dbo].[Ribbons] ([RibbonId])
ON UPDATE SET NULL
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_Ribbons]
GO
/****** Object:  ForeignKey [FK_Buildings_Users]    Script Date: 04/26/2012 16:35:44 ******/
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_Users]
GO
/****** Object:  ForeignKey [FK_Subscriptions_Products]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Subscriptions]  WITH CHECK ADD  CONSTRAINT [FK_Subscriptions_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Subscriptions] CHECK CONSTRAINT [FK_Subscriptions_Products]
GO
/****** Object:  ForeignKey [FK_Subscriptions_UserCreditCards]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Subscriptions]  WITH CHECK ADD  CONSTRAINT [FK_Subscriptions_UserCreditCards] FOREIGN KEY([UserCreditCardId])
REFERENCES [dbo].[UserCreditCards] ([UserCreditCardId])
GO
ALTER TABLE [dbo].[Subscriptions] CHECK CONSTRAINT [FK_Subscriptions_UserCreditCards]
GO
/****** Object:  ForeignKey [FK_Subscriptions_Users]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Subscriptions]  WITH CHECK ADD  CONSTRAINT [FK_Subscriptions_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Subscriptions] CHECK CONSTRAINT [FK_Subscriptions_Users]
GO
/****** Object:  ForeignKey [FK_UserContractAnswers_Answers]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[UserContractAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserContractAnswers_Answers] FOREIGN KEY([AnswerId])
REFERENCES [dbo].[Answers] ([AnswerId])
GO
ALTER TABLE [dbo].[UserContractAnswers] CHECK CONSTRAINT [FK_UserContractAnswers_Answers]
GO
/****** Object:  ForeignKey [FK_UserContractAnswers_UserContracts]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[UserContractAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserContractAnswers_UserContracts] FOREIGN KEY([UserContractId])
REFERENCES [dbo].[UserContracts] ([UserContractId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserContractAnswers] CHECK CONSTRAINT [FK_UserContractAnswers_UserContracts]
GO
/****** Object:  ForeignKey [FK_SubscriptionOrders_Orders]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[SubscriptionOrders]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionOrders_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[SubscriptionOrders] CHECK CONSTRAINT [FK_SubscriptionOrders_Orders]
GO
/****** Object:  ForeignKey [FK_SubscriptionOrders_Subscriptions]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[SubscriptionOrders]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionOrders_Subscriptions] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscriptions] ([SubscriptionId])
GO
ALTER TABLE [dbo].[SubscriptionOrders] CHECK CONSTRAINT [FK_SubscriptionOrders_Subscriptions]
GO
/****** Object:  ForeignKey [FK_SavedBuildings_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[SavedBuildings]  WITH CHECK ADD  CONSTRAINT [FK_SavedBuildings_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[SavedBuildings] CHECK CONSTRAINT [FK_SavedBuildings_Buildings]
GO
/****** Object:  ForeignKey [FK_SavedBuildings_Users]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[SavedBuildings]  WITH CHECK ADD  CONSTRAINT [FK_SavedBuildings_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SavedBuildings] CHECK CONSTRAINT [FK_SavedBuildings_Users]
GO
/****** Object:  ForeignKey [FK_UserInterests_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[UserInterests]  WITH CHECK ADD  CONSTRAINT [FK_UserInterests_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[UserInterests] CHECK CONSTRAINT [FK_UserInterests_Buildings]
GO
/****** Object:  ForeignKey [FK_UserInterests_Users]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[UserInterests]  WITH CHECK ADD  CONSTRAINT [FK_UserInterests_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserInterests] CHECK CONSTRAINT [FK_UserInterests_Users]
GO
/****** Object:  ForeignKey [FK_BuildingCounts_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingCounts]  WITH CHECK ADD  CONSTRAINT [FK_BuildingCounts_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BuildingCounts] CHECK CONSTRAINT [FK_BuildingCounts_Buildings]
GO
/****** Object:  ForeignKey [FK_BuildingAmenitiesWithOptions_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingAmenitiesWithOptions]  WITH NOCHECK ADD  CONSTRAINT [FK_BuildingAmenitiesWithOptions_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BuildingAmenitiesWithOptions] CHECK CONSTRAINT [FK_BuildingAmenitiesWithOptions_Buildings]
GO
/****** Object:  ForeignKey [FK_HomeAmenitiesWithOptions_AmenitiesWithOptions]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingAmenitiesWithOptions]  WITH NOCHECK ADD  CONSTRAINT [FK_HomeAmenitiesWithOptions_AmenitiesWithOptions] FOREIGN KEY([AmenityId])
REFERENCES [dbo].[AmenitiesWithOptions] ([AmenityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BuildingAmenitiesWithOptions] CHECK CONSTRAINT [FK_HomeAmenitiesWithOptions_AmenitiesWithOptions]
GO
/****** Object:  ForeignKey [FK_BuildingAmenities_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingAmenities]  WITH NOCHECK ADD  CONSTRAINT [FK_BuildingAmenities_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BuildingAmenities] CHECK CONSTRAINT [FK_BuildingAmenities_Buildings]
GO
/****** Object:  ForeignKey [FK_HomeAmenities_Amenities]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[BuildingAmenities]  WITH NOCHECK ADD  CONSTRAINT [FK_HomeAmenities_Amenities] FOREIGN KEY([AmenityId])
REFERENCES [dbo].[Amenities] ([AmenityId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BuildingAmenities] CHECK CONSTRAINT [FK_HomeAmenities_Amenities]
GO
/****** Object:  ForeignKey [FK_CustomAmenities_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[CustomAmenities]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomAmenities_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomAmenities] CHECK CONSTRAINT [FK_CustomAmenities_Buildings]
GO
/****** Object:  ForeignKey [FK_ReservedRibbons_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[ReservedRibbons]  WITH CHECK ADD  CONSTRAINT [FK_ReservedRibbons_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[ReservedRibbons] CHECK CONSTRAINT [FK_ReservedRibbons_Buildings]
GO
/****** Object:  ForeignKey [FK_ReservedRibbons_Ribbons]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[ReservedRibbons]  WITH CHECK ADD  CONSTRAINT [FK_ReservedRibbons_Ribbons] FOREIGN KEY([RibbonId])
REFERENCES [dbo].[Ribbons] ([RibbonId])
GO
ALTER TABLE [dbo].[ReservedRibbons] CHECK CONSTRAINT [FK_ReservedRibbons_Ribbons]
GO
/****** Object:  ForeignKey [FK_ReservedFeaturedListings_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[ReservedFeaturedListings]  WITH CHECK ADD  CONSTRAINT [FK_ReservedFeaturedListings_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReservedFeaturedListings] CHECK CONSTRAINT [FK_ReservedFeaturedListings_Buildings]
GO
/****** Object:  ForeignKey [FK_ReservedFeaturedListings_FeaturedSections]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[ReservedFeaturedListings]  WITH CHECK ADD  CONSTRAINT [FK_ReservedFeaturedListings_FeaturedSections] FOREIGN KEY([FeaturedSectionId])
REFERENCES [dbo].[FeaturedSections] ([FeaturedSectionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReservedFeaturedListings] CHECK CONSTRAINT [FK_ReservedFeaturedListings_FeaturedSections]
GO
/****** Object:  ForeignKey [FK_ReportedBuildings_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[ReportedBuildings]  WITH CHECK ADD  CONSTRAINT [FK_ReportedBuildings_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReportedBuildings] CHECK CONSTRAINT [FK_ReportedBuildings_Buildings]
GO
/****** Object:  ForeignKey [FK_PropertyTransactions_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[PropertyTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PropertyTransactions_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[PropertyTransactions] CHECK CONSTRAINT [FK_PropertyTransactions_Buildings]
GO
/****** Object:  ForeignKey [FK_Leases_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD  CONSTRAINT [FK_Leases_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_Buildings]
GO
/****** Object:  ForeignKey [FK_Leases_UserBanks]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD  CONSTRAINT [FK_Leases_UserBanks] FOREIGN KEY([TenantBankId])
REFERENCES [dbo].[UserBanks] ([UserBankId])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_UserBanks]
GO
/****** Object:  ForeignKey [FK_Leases_UserBanks1]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD  CONSTRAINT [FK_Leases_UserBanks1] FOREIGN KEY([LandlordBankId])
REFERENCES [dbo].[UserBanks] ([UserBankId])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_UserBanks1]
GO
/****** Object:  ForeignKey [FK_Leases_Users]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD  CONSTRAINT [FK_Leases_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Leases] CHECK CONSTRAINT [FK_Leases_Users]
GO
/****** Object:  ForeignKey [FK_FeaturedListings_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[FeaturedListings]  WITH CHECK ADD  CONSTRAINT [FK_FeaturedListings_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FeaturedListings] CHECK CONSTRAINT [FK_FeaturedListings_Buildings]
GO
/****** Object:  ForeignKey [FK_FeaturedListings_FeaturedSection]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[FeaturedListings]  WITH CHECK ADD  CONSTRAINT [FK_FeaturedListings_FeaturedSection] FOREIGN KEY([FeaturedSectionId])
REFERENCES [dbo].[FeaturedSections] ([FeaturedSectionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FeaturedListings] CHECK CONSTRAINT [FK_FeaturedListings_FeaturedSection]
GO
/****** Object:  ForeignKey [FK_LandlordAlerts_Buildings]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LandlordAlerts]  WITH CHECK ADD  CONSTRAINT [FK_LandlordAlerts_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LandlordAlerts] CHECK CONSTRAINT [FK_LandlordAlerts_Buildings]
GO
/****** Object:  ForeignKey [FK_LandlordAlerts_Users]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LandlordAlerts]  WITH CHECK ADD  CONSTRAINT [FK_LandlordAlerts_Users] FOREIGN KEY([LandlordId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[LandlordAlerts] CHECK CONSTRAINT [FK_LandlordAlerts_Users]
GO
/****** Object:  ForeignKey [FK_LandlordAlerts_Users1]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LandlordAlerts]  WITH CHECK ADD  CONSTRAINT [FK_LandlordAlerts_Users1] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[LandlordAlerts] CHECK CONSTRAINT [FK_LandlordAlerts_Users1]
GO
/****** Object:  ForeignKey [FK_OrderItems_Orders]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders]
GO
/****** Object:  ForeignKey [FK_OrderItems_Products]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Products]
GO
/****** Object:  ForeignKey [FK_LeaseTransactions_Leases]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeaseTransactions]  WITH CHECK ADD  CONSTRAINT [FK_LeaseTransactions_Leases] FOREIGN KEY([LeaseId])
REFERENCES [dbo].[Leases] ([LeaseId])
GO
ALTER TABLE [dbo].[LeaseTransactions] CHECK CONSTRAINT [FK_LeaseTransactions_Leases]
GO
/****** Object:  ForeignKey [FK_LeaseTenants_Leases]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeaseTenants]  WITH CHECK ADD  CONSTRAINT [FK_LeaseTenants_Leases] FOREIGN KEY([LeaseId])
REFERENCES [dbo].[Leases] ([LeaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeaseTenants] CHECK CONSTRAINT [FK_LeaseTenants_Leases]
GO
/****** Object:  ForeignKey [FK_LeasePaymentItems_Leases]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeasePaymentItems]  WITH CHECK ADD  CONSTRAINT [FK_LeasePaymentItems_Leases] FOREIGN KEY([LeaseId])
REFERENCES [dbo].[Leases] ([LeaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeasePaymentItems] CHECK CONSTRAINT [FK_LeasePaymentItems_Leases]
GO
/****** Object:  ForeignKey [FK_LeaseContracts_Leases]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeaseContracts]  WITH CHECK ADD  CONSTRAINT [FK_LeaseContracts_Leases] FOREIGN KEY([LeaseId])
REFERENCES [dbo].[Leases] ([LeaseId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeaseContracts] CHECK CONSTRAINT [FK_LeaseContracts_Leases]
GO
/****** Object:  ForeignKey [FK_LeasePaymentPropertyTransactions_LeasePaymentItems]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeasePaymentPropertyTransactions]  WITH CHECK ADD  CONSTRAINT [FK_LeasePaymentPropertyTransactions_LeasePaymentItems] FOREIGN KEY([LeasePaymentItemId])
REFERENCES [dbo].[LeasePaymentItems] ([LeasePaymentItemId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeasePaymentPropertyTransactions] CHECK CONSTRAINT [FK_LeasePaymentPropertyTransactions_LeasePaymentItems]
GO
/****** Object:  ForeignKey [FK_LeasePaymentPropertyTransactions_PropertyTransactions]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[LeasePaymentPropertyTransactions]  WITH CHECK ADD  CONSTRAINT [FK_LeasePaymentPropertyTransactions_PropertyTransactions] FOREIGN KEY([PropertyTransactionId])
REFERENCES [dbo].[PropertyTransactions] ([PropertyTransactionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeasePaymentPropertyTransactions] CHECK CONSTRAINT [FK_LeasePaymentPropertyTransactions_PropertyTransactions]
GO
/****** Object:  ForeignKey [FK_AchFromTenantTransactions_LeaseTransactions]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[AchFromTenantTransactions]  WITH CHECK ADD  CONSTRAINT [FK_AchFromTenantTransactions_LeaseTransactions] FOREIGN KEY([LeaseTransactionId])
REFERENCES [dbo].[LeaseTransactions] ([LeaseTransactionId])
GO
ALTER TABLE [dbo].[AchFromTenantTransactions] CHECK CONSTRAINT [FK_AchFromTenantTransactions_LeaseTransactions]
GO
/****** Object:  ForeignKey [FK_AchToLandlordTransactions_AchFromTenantTransactions]    Script Date: 04/26/2012 16:35:45 ******/
ALTER TABLE [dbo].[AchToLandlordTransactions]  WITH CHECK ADD  CONSTRAINT [FK_AchToLandlordTransactions_AchFromTenantTransactions] FOREIGN KEY([AchFromTenantTransactionId])
REFERENCES [dbo].[AchFromTenantTransactions] ([AchFromTenantTransactionId])
GO
ALTER TABLE [dbo].[AchToLandlordTransactions] CHECK CONSTRAINT [FK_AchToLandlordTransactions_AchFromTenantTransactions]
GO
