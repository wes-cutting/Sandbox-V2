ALTER TABLE dbo.Buildings
ADD
	[ListingRemovedByAdmin] [bit] NOT NULL DEFAULT 0
	
ALTER TABLE dbo.Listings
ADD
	[Flagged] [bit] NOT NULL DEFAULT 0