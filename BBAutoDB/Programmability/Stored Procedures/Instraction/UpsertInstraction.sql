create procedure [dbo].[UpsertInstraction]
  @id int,
  @DriverId int,
  @number nvarchar(50),
  @date datetime,
  @file nvarchar(100)
as
begin
  if (@id = 0)
  begin
    insert into Instraction values(@number, @date, @DriverId, @file)

    set @id = scope_identity()
  end
  else
    update Instraction
    set Number = @number,
        [Date] = @date,
        [File] = @file
    where Id = @id

  select @id
end
