create procedure [dbo].[UpsertCurrentStatusAfterDTP]
  @id int,
  @name nvarchar(100)
as
begin
  if (@id = 0)
  begin
    insert into CurrentStatusAfterDTP values(@name)

    set @id = scope_identity()
  end
  else
    update CurrentStatusAfterDTP
    set CurrentStatusAfterDTP_name = @name
    where CurrentStatusAfterDTP_id = @id

  select @id
end
