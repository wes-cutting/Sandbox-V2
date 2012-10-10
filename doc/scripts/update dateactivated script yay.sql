--add dateactivated to our listings
alter table Listings add DateActivated datetime null

go

--determine a default dateactivated timestamp for our
--existing listings, which were around before this new
--system was implemented
declare @activated datetime = getutcdate();
declare @RowsToProcess  int
declare @CurrentRow     int
declare @selectCol1     int

declare @table1 table (RowID int not null primary key identity(1,1), col1 int )  
insert into @table1 (col1) select ListingId from Listings order by CreateDate desc
set @RowsToProcess=@@ROWCOUNT

set @CurrentRow=0
while @CurrentRow<@RowsToProcess
begin
    set @CurrentRow=@CurrentRow+1
    select 
        @selectCol1=col1
        from @table1
        where RowID=@CurrentRow

	update Listings
	set		DateActivated = @activated
	where	ListingId = @selectCol1
	
	set @activated = dateadd(mi, -1, @activated);
end

select	*
from	Listings
order by CreateDate desc