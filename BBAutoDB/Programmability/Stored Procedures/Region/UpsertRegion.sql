create procedure [dbo].[UpsertRegion]
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
      Region
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into Region values(@Name)
      set @id = scope_identity()
    end
  end
  else
  begin
    update Region
    set [Name] = @Name
    where Id = @id
  end

  select @id
end
