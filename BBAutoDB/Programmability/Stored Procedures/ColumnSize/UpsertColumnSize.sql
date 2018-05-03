create procedure [dbo].[UpsertColumnSize]
  @idDriver int,
  @idStatus int,
  @column0 int,
  @column1 int,
  @column2 int,
  @column3 int,
  @column4 int,
  @column5 int,
  @column6 int,
  @column7 int,
  @column8 int,
  @column9 int,
  @column10 int,
  @column11 int,
  @column12 int,
  @column13 int,
  @column14 int,
  @column15 int,
  @column16 int
as
begin
  declare @count int
  select
    @count = count(*)
  from
    ColumnSize
  where
    driver_id = @idDriver
    and status_id = @idStatus

  if (@count = 0)
  begin
    insert into ColumnSize
    values(@idDriver, @idStatus, @column0, @column1, @column2, @column3, @column4, @column5, @column6, @column7, @column8, @column9, @column10, @column11, @column12, @column13, @column14, @column15, @column16)
  end
  else
  begin
    update ColumnSize
    set column0 = @column0,
        Column1 = @column1,
        column2 = @column2,
        column3 = @column3,
        column4 = @column4,
        column5 = @column5,
        column6 = @column6,
        column7 = @column7,
        column8 = @column8,
        column9 = @column9,
        column10 = @column10,
        column11 = @column11,
        column12 = @column12,
        column13 = @column13,
        column14 = @column14,
        column15 = @column15,
        column16 = @column16
    where driver_id = @idDriver
    and status_id = @idStatus
  end
end
