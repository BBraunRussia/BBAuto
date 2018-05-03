create procedure [dbo].[UpsertRoute]
  @id int,
  @idMyPoint1 int,
  @idMyPoint2 int,
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
      (mypoint1_id = @idMyPoint1
      and mypoint2_id = @idMyPoint2)
      or (mypoint1_id = @idMyPoint2
      and mypoint2_id = @idMyPoint1)

    if (@count = 0)
    begin
      insert into Route values(@idMyPoint1, @idMyPoint2, @distance)
      set @id = scope_identity()
    end
  end
  else
  begin
    update Route
    set route_distance = @distance
    where route_id = @id
  end

  select @id
end
