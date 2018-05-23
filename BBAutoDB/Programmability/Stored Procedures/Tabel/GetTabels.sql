create procedure [dbo].[GetTabels]
as
begin
  select
    DriverId,
    [Date],
    Comment
  from
    Tabel
  order by
    DriverId,
    [Date]
end
