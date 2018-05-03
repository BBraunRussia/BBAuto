create procedure [dbo].[InsertTabel]
  @idDriver int,
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
    driver_id = @idDriver
    and tabel_date = @date

  if (@comment = '')
    set @comment = null

  if (@count = 0)
  begin
    insert into Tabel values(@idDriver, @date, @comment)
  end
end
