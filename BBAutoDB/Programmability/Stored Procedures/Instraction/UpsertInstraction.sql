create procedure [dbo].[UpsertInstraction]
  @id int,
  @idDriver int,
  @number nvarchar(50),
  @date datetime,
  @file nvarchar(100)
as
begin
  if (@id = 0)
  begin
    insert into Instraction values(@number, @date, @idDriver, @file)

    set @id = scope_identity()
  end
  else
    update Instraction
    set Instraction_number = @number,
        Instraction_date = @date,
        instraction_file = @file
    where Instraction_id = @id

  select @id
end
