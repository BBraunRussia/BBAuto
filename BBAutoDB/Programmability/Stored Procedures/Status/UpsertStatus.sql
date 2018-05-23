create procedure [dbo].[UpsertStatus]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      Status
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into Status([Name]) values(@Name)

      select scope_identity(), @Name
    end
  end
  else
  begin
    update Status
    set [Name] = @Name
    where Id = @id

    select @id, @Name
  end
end
