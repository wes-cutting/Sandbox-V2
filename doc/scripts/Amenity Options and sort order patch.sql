
alter table AmenityOptions
add SortOrder int not null default ((0))

go

insert into AmenityOptions
values (4, 'Forced Air', getdate(), 'dusda', getdate(), 'dusda', 0, 0)

update	AmenityOptions
set		SortOrder = 1
where	[Option] = 'Radiant Heat'

update	AmenityOptions
set		SortOrder = 2
where	[Option] = 'Geothermal'

update	AmenityOptions
set		SortOrder = 5
where	[Option] = 'Other'

select	*
from	AmenityOptions
where	AmenityId = 4
order by SortOrder asc

update	AmenityOptions
set		SortOrder = 0
where	[Option] = 'Central Air'

update	AmenityOptions
set		SortOrder = 1
where	[Option] = 'Evaporative Cooler'