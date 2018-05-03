create procedure [dbo].[GetLastInstraction]
  @idDriver int
as
begin
  declare @last nvarchar(100)

  select top 1
    @last = '№ ' + cast(Instraction_number as nvarchar(50)) + ' до ' +
    +case when day(Instraction_date) < 10 then '0' else '' end
    + cast(day(Instraction_date) as nvarchar(50)) + '.'
    + case when month(Instraction_date) < 10 then '0' else '' end
    + cast(month(Instraction_date) as nvarchar(50))
    + '.' + cast(year(Instraction_date) as nvarchar(50))
  from
    Instraction
  where
    driver_id = @idDriver
  order by
    Instraction_date desc

  if (@last is null)
    select 'нет данных'
  else
    select @last
end
