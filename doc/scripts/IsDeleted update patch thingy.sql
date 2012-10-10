exec sp_msforeachtable 'alter table ? add IsDeleted bit not null default((0))'
