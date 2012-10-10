/****** Object:  Table [dbo].[ReportedListings]    Script Date: 01/30/2012 10:24:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportedListings](
	[ReportedListingId] [int] IDENTITY(1,1) NOT NULL,
	[ListingId] [int] NOT NULL,
	[ReportContent] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_ReportedListings] PRIMARY KEY CLUSTERED 
(
	[ReportedListingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  ForeignKey [FK_ReportedListings_Listings]    Script Date: 01/30/2012 10:24:10 ******/
ALTER TABLE [dbo].[ReportedListings]  WITH CHECK ADD  CONSTRAINT [FK_ReportedListings_Listings] FOREIGN KEY([ListingId])
REFERENCES [dbo].[Listings] ([ListingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReportedListings] CHECK CONSTRAINT [FK_ReportedListings_Listings]
GO

--remove the flagged column from Listings
alter table Listings
drop column Flagged