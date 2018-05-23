create procedure [dbo].[UpsertRepair]
  @id int,
  @CarId int,
  @RepairTypeId int,
  @ServiceStantionId int,
  @date datetime,
  @cost float,
  @file nvarchar(200)
as
begin
  if (@id = 0)
  begin
    insert into Repair values(@CarId, @RepairTypeId, @ServiceStantionId, @date, @cost, @file)
    set @id = scope_identity()
  end
  else
  begin
    update Repair
    set RepairTypeId = @RepairTypeId,
        ServiceStantionId = @ServiceStantionId,
        [Date] = @date,
        Cost = @cost,
        [File] = @file
    where Id = @id
  end

  select @id
end
