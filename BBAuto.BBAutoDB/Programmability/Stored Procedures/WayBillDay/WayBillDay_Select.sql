CREATE PROCEDURE [dbo].[WayBillDay_Select]
AS
BEGIN
	SELECT wayBillDay_id, car_id, driver_id, wayBillDay_date FROM WayBillDay
END
GO
