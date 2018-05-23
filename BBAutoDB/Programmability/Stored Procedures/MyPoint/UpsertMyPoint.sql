create procedure [dbo].[UpsertMyPoint]
  @id int,
  @RegionId int,
  @name nvarchar(100)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      MyPoint
    where
      [Name] = @name
      and RegionId = @RegionId

    if (@count = 0)
    begin
      insert into MyPoint values(@RegionId, @name)
      set @id = scope_identity()
    end
  end
  else
  begin
    update MyPoint
    set RegionId = @RegionId,
        [Name] = @name
    where Id = @id
  end

  select @id
end
