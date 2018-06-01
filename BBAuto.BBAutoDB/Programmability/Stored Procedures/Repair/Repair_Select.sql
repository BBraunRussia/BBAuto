CREATE PROCEDURE [dbo].[Repair_Select]
AS
BEGIN
	SELECT repair_id, car_id, repairType_id, ServiceStantion_id, repair_date, repair_cost, repair_file
	FROM Repair
END
GO
