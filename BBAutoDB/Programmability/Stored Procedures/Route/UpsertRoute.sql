create procedure [dbo].[UpsertRoute]
  @id int,
  @MyPointId1 int,
  @MyPointId2 int,
  @distance int
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      Route
    where
      (MypointId1 = @MyPointId1
      and MypointId2 = @MyPointId2)
      or (MypointId1 = @MyPointId2
      and MypointId2 = @MyPointId1)

    if (@count = 0)
    begin
      insert into Route values(@MyPointId1, @MyPointId2, @distance)
      set @id = scope_identity()
    end
  end
  else
  begin
    update Route
    set Distance = @distance
    where id = @id
  end

  select @id
end
