create procedure [dbo].[UpsertStatus]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(Status_id)
    from
      Status
    where
      Status_name = @Name

    if (@count = 0)
    begin
      insert into Status(Status_name) values(@Name)

      select scope_identity(), @Name
    end
  end
  else
  begin
    update Status
    set Status_name = @Name
    where Status_id = @id

    select @id, @Name
  end
end
