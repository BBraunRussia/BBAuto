create procedure [dbo].[InsertWayBillDay]
  @id int,
  @idCar int,
  @idDriver int,
  @date datetime
as
begin
  if (@id = 0)
  begin
    insert into WayBillDay values(@idCar, @idDriver, @date)
    set @id = scope_identity()
  end

  select @id
end
