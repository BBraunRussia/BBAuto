CREATE PROCEDURE [dbo].[Repair_Insert]
@idRepair int,
@idCar int,
@idRepairType int,
@idServiceStantion int,
@date datetime,
@cost float,
@file varchar(200)
AS
BEGIN
	if (@idRepair = 0)
	begin
		INSERT INTO Repair VALUES(@idCar, @idRepairType, @idServiceStantion, @date, @cost, @file)
		SET @idRepair = SCOPE_IDENTITY()
	end
	else
	begin
		UPDATE Repair SET repairType_id=@idRepairType, ServiceStantion_id=@idServiceStantion,
			repair_date=@date, repair_cost=@cost, repair_file=@file
		WHERE repair_id=@idRepair
	end
	
	SELECT @idRepair
END
GO
