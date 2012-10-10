/****** Object:  Table [dbo].[ListingCounts]    Script Date: 01/30/2012 16:42:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListingCounts](
	[ListingId] [int] NOT NULL,
	[SearchCount] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
 CONSTRAINT [PK_ListingCounts] PRIMARY KEY CLUSTERED 
(
	[ListingId] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Default [DF_ListingCounts_SearchCount]    Script Date: 01/30/2012 16:42:15 ******/
ALTER TABLE [dbo].[ListingCounts] ADD  CONSTRAINT [DF_ListingCounts_SearchCount]  DEFAULT ((0)) FOR [SearchCount]
GO
/****** Object:  Default [DF_ListingCounts_ViewCount]    Script Date: 01/30/2012 16:42:15 ******/
ALTER TABLE [dbo].[ListingCounts] ADD  CONSTRAINT [DF_ListingCounts_ViewCount]  DEFAULT ((0)) FOR [ViewCount]
GO
/****** Object:  ForeignKey [FK_ListingCounts_Listings]    Script Date: 01/30/2012 16:42:15 ******/
ALTER TABLE [dbo].[ListingCounts]  WITH CHECK ADD  CONSTRAINT [FK_ListingCounts_Listings] FOREIGN KEY([ListingId])
REFERENCES [dbo].[Listings] ([ListingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ListingCounts] CHECK CONSTRAINT [FK_ListingCounts_Listings]

GO

--import listing counts to new place
insert into ListingCounts(ListingId, SearchCount, ViewCount)
select	ListingId, SearchCount, ViewCount
from	Listings

--drop the old beast
alter table Listings
drop constraint DF_Listings_SearchCount

alter table Listings
drop column SearchCount

alter table Listings
drop constraint DF_Listings_ViewCount

alter table Listings
drop column ViewCount