create procedure [dbo].[UpsertRegion]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(region_id)
    from
      Region
    where
      region_name = @Name

    if (@count = 0)
    begin
      insert into Region values(@Name)
      set @id = scope_identity()
    end
  end
  else
  begin
    update Region
    set region_name = @Name
    where region_id = @id
  end

  select @id
end
