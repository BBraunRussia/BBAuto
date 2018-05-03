create procedure [dbo].[InsertWayBillRoute]
  @id int,
  @idWayBillDay int,
  @idMyPoint1 int,
  @idMyPoint2 int,
  @distance int
as
begin
  if (@id = 0)
  begin
    insert into WayBillRoute values(@idWayBillDay, @idMyPoint1, @idMyPoint2, @distance)
    set @id = scope_identity()
  end

  select @id
end
