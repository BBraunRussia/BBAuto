create procedure [dbo].[InsertTabel]
  @DriverId int,
  @date datetime,
  @comment nvarchar(50) = null
as
begin
  declare @count int
  select
    @count = count(*)
  from
    Tabel
  where
    DriverId = @DriverId
    and [Date] = @date

  if (@comment = '')
    set @comment = null

  if (@count = 0)
  begin
    insert into Tabel values(@DriverId, @date, @comment)
  end
end
