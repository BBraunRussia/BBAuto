create procedure [dbo].[GetLastInstraction]
  @DriverId int
as
begin
  declare @last nvarchar(100)

  select top 1
    @last = '№ ' + cast(Number as nvarchar(50)) + ' до ' +
    +case when day([Date]) < 10 then '0' else '' end
    + cast(day([Date]) as nvarchar(50)) + '.'
    + case when month([Date]) < 10 then '0' else '' end
    + cast(month([Date]) as nvarchar(50))
    + '.' + cast(year([Date]) as nvarchar(50))
  from
    Instraction
  where
    DriverId = @DriverId
  order by
    [Date] desc

  if (@last is null)
    select 'нет данных'
  else
    select @last
end
