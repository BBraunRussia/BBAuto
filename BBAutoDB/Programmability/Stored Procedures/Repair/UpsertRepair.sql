create procedure [dbo].[UpsertRepair]
  @idRepair int,
  @idCar int,
  @idRepairType int,
  @idServiceStantion int,
  @date datetime,
  @cost float,
  @file nvarchar(200)
as
begin
  if (@idRepair = 0)
  begin
    insert into Repair values(@idCar, @idRepairType, @idServiceStantion, @date, @cost, @file)
    set @idRepair = scope_identity()
  end
  else
  begin
    update Repair
    set repairType_id = @idRepairType,
        ServiceStantion_id = @idServiceStantion,
        repair_date = @date,
        repair_cost = @cost,
        repair_file = @file
    where repair_id = @idRepair
  end

  select @idRepair
end
