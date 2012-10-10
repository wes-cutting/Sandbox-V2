/****** Object:  Table [dbo].[ApiKeys]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiKeys](
	[ApiKeyId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](40) NULL,
 CONSTRAINT [PK_ApiKeys] PRIMARY KEY CLUSTERED 
(
	[ApiKeyId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[ZipInfoes]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZipInfoes](
	[ZipInfoId] [bigint] IDENTITY(1,1) NOT NULL,
	[Latitude] [real] NOT NULL,
	[Longitude] [real] NOT NULL,
	[CityAliasName] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[StateCode] [nvarchar](4) NOT NULL,
	[ZipCode] [int] NOT NULL,
 CONSTRAINT [PK_ZipInfoes] PRIMARY KEY CLUSTERED 
(
	[ZipInfoId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[UpdateDateUtc] [datetime] NOT NULL,
	[CreateDateUtc] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[OldUserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[UserCreditCards]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCreditCards](
	[UserCreditCardId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Alias] [bigint] NULL,
	[AccountReference] [uniqueidentifier] NULL,
	[CardNumber] [nvarchar](16) NOT NULL,
	[CardName] [nvarchar](50) NOT NULL,
	[ExpirationMonth] [int] NOT NULL,
	[ExpirationYear] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Address1] [nvarchar](50) NOT NULL,
	[Address2] [nvarchar](50) NULL,
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
/****** Object:  Table [dbo].[UserApplications]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserApplications](
	[UserId] [int] NOT NULL,
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
	[EmergencyContact] [nvarchar](50) NULL,
	[PreviousEmployer] [nvarchar](50) NULL,
	[PreviousEmployerPhone] [nvarchar](50) NULL,
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
	[HasBeenConvicted] [bit] NOT NULL,
	[ConvictedExplaination] [nvarchar](500) NULL,
	[HasEverBeenUnlawfulDetainer] [bit] NOT NULL,
	[CreateDateUtc] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdateDateUtc] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserApplications] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[AffiliateUsers]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AffiliateUsers](
	[UserId] [int] NOT NULL,
	[AffiliateUserKey] [nvarchar](50) NULL,
	[ApiKey] [uniqueidentifier] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_AffiliateUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[ContactInfoes]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactInfoes](
	[ContactInfoId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ContactInfoTypeCode] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CompanyName] [nvarchar](100) NULL,
	[Email] [nvarchar](250) NOT NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[ShowPhoneNumber] [bit] NOT NULL,
	[ShowEmailAddress] [bit] NOT NULL,
 CONSTRAINT [PK_ContactInfoes] PRIMARY KEY CLUSTERED 
(
	[ContactInfoId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Buildings]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buildings](
	[BuildingId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreateDateUtc] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[UpdateDateUtc] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[RibbonId] [nvarchar](50) NULL,
	[ReservedRibbonId] [nvarchar](50) NULL,
	[ContactInfoId] [int] NOT NULL,
	[Address1] [nvarchar](250) NOT NULL,
	[Address2] [nvarchar](250) NULL,
	[City] [nvarchar](250) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[Zip] [nvarchar](10) NOT NULL,
	[Latitude] [real] NOT NULL,
	[Longitude] [real] NOT NULL,
	[PropertyTypeCode] [int] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](4000) NULL,
	[SquareFeet] [int] NOT NULL,
	[Acres] [real] NOT NULL,
	[YearBuilt] [int] NOT NULL,
	[Bedrooms] [int] NOT NULL,
	[Bathrooms] [real] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DateAvailableUtc] [datetime] NULL,
	[DateActivatedUtc] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[IsCreditCheckRequired] [bit] NOT NULL,
	[IsBackgroundCheckRequired] [bit] NOT NULL,
	[Deposit] [decimal](18, 2) NOT NULL,
	[RefundableDeposit] [decimal](18, 2) NOT NULL,
	[LeaseLengthCode] [int] NOT NULL,
	[IsSmokingAllowed] [bit] NOT NULL,
	[ArePetsAllowed] [bit] NOT NULL,
	[IsRemovedByAdmin] [bit] NOT NULL,
	[IsReported] [bit] NOT NULL,
	[ReportedText] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[PetFee] [decimal](18, 2) NOT NULL,
	[PrimaryPhotoId] [uniqueidentifier] NULL,
	[PrimaryPhotoExtension] [nvarchar](max) NULL,
 CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[CustomAmenities]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomAmenities](
	[BuildingId] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CustomAmenities] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC,
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[BuildingAmenities]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuildingAmenities](
	[BuildingId] [bigint] NOT NULL,
	[AmenityId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_BuildingAmenities] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC,
	[AmenityId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[SavedBuildings]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SavedBuildings](
	[UserId] [int] NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[CreateDateUtc] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_SavedBuildings] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[BuildingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Photos]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photos](
	[PhotoId] [uniqueidentifier] NOT NULL,
	[BuildingId] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[Extension] [nvarchar](4) NOT NULL,
	[CreateDateUtc] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[UpdateDateUtc] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[PhotoId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NULL,
	[UserId] [int] NOT NULL,
	[UserCreditCardId] [int] NOT NULL,
	[Status] [nvarchar](300) NOT NULL,
	[OrderTotal] [decimal](18, 2) NOT NULL,
	[PromoCode] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[Building_BuildingId] [bigint] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 07/04/2012 13:24:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[OrderItemId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ProductId] [nvarchar](100) NOT NULL,
	[ProductDescription] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  ForeignKey [FK_AffiliateUsers_Users_UserId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[AffiliateUsers]  WITH CHECK ADD  CONSTRAINT [FK_AffiliateUsers_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[AffiliateUsers] CHECK CONSTRAINT [FK_AffiliateUsers_Users_UserId]
GO
/****** Object:  ForeignKey [FK_BuildingAmenities_Buildings_BuildingId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[BuildingAmenities]  WITH CHECK ADD  CONSTRAINT [FK_BuildingAmenities_Buildings_BuildingId] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BuildingAmenities] CHECK CONSTRAINT [FK_BuildingAmenities_Buildings_BuildingId]
GO
/****** Object:  ForeignKey [FK_Buildings_ContactInfoes_ContactInfoId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_ContactInfoes_ContactInfoId] FOREIGN KEY([ContactInfoId])
REFERENCES [dbo].[ContactInfoes] ([ContactInfoId])
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_ContactInfoes_ContactInfoId]
GO
/****** Object:  ForeignKey [FK_Buildings_Users_UserId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_Users_UserId]
GO
/****** Object:  ForeignKey [FK_ContactInfoes_Users_UserId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[ContactInfoes]  WITH CHECK ADD  CONSTRAINT [FK_ContactInfoes_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactInfoes] CHECK CONSTRAINT [FK_ContactInfoes_Users_UserId]
GO
/****** Object:  ForeignKey [FK_CustomAmenities_Buildings_BuildingId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[CustomAmenities]  WITH CHECK ADD  CONSTRAINT [FK_CustomAmenities_Buildings_BuildingId] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomAmenities] CHECK CONSTRAINT [FK_CustomAmenities_Buildings_BuildingId]
GO
/****** Object:  ForeignKey [FK_OrderItems_Orders_OrderId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders_OrderId]
GO
/****** Object:  ForeignKey [FK_Orders_Buildings_Building_BuildingId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Buildings_Building_BuildingId] FOREIGN KEY([Building_BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Buildings_Building_BuildingId]
GO
/****** Object:  ForeignKey [FK_Orders_UserCreditCards_UserCreditCardId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_UserCreditCards_UserCreditCardId] FOREIGN KEY([UserCreditCardId])
REFERENCES [dbo].[UserCreditCards] ([UserCreditCardId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_UserCreditCards_UserCreditCardId]
GO
/****** Object:  ForeignKey [FK_Orders_Users_UserId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_UserId]
GO
/****** Object:  ForeignKey [FK_Photos_Buildings_BuildingId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[Photos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_Buildings_BuildingId] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Photos] CHECK CONSTRAINT [FK_Photos_Buildings_BuildingId]
GO
/****** Object:  ForeignKey [FK_SavedBuildings_Buildings_BuildingId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[SavedBuildings]  WITH CHECK ADD  CONSTRAINT [FK_SavedBuildings_Buildings_BuildingId] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[SavedBuildings] CHECK CONSTRAINT [FK_SavedBuildings_Buildings_BuildingId]
GO
/****** Object:  ForeignKey [FK_SavedBuildings_Users_UserId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[SavedBuildings]  WITH CHECK ADD  CONSTRAINT [FK_SavedBuildings_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SavedBuildings] CHECK CONSTRAINT [FK_SavedBuildings_Users_UserId]
GO
/****** Object:  ForeignKey [FK_UserApplications_Users_UserId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[UserApplications]  WITH CHECK ADD  CONSTRAINT [FK_UserApplications_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserApplications] CHECK CONSTRAINT [FK_UserApplications_Users_UserId]
GO
/****** Object:  ForeignKey [FK_UserCreditCards_Users_UserId]    Script Date: 07/04/2012 13:24:09 ******/
ALTER TABLE [dbo].[UserCreditCards]  WITH CHECK ADD  CONSTRAINT [FK_UserCreditCards_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCreditCards] CHECK CONSTRAINT [FK_UserCreditCards_Users_UserId]
GO
