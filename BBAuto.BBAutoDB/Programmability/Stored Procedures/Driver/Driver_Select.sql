CREATE PROCEDURE [dbo].[Driver_Select]
AS
BEGIN
	SELECT dr.driver_id, driver_fio, region_id, driver_dateBirth, driver_mobile, driver_email,
		driver_fired, driver_expSince, dr.position_id, dr.dept_id, driver_login, owner_id,
		driver_suppyAddress, driver_sex, driver_decret, driver_dateStopNotification,
		driver_number, driver_isDriver, driver_from1C
	FROM Driver dr
		left join Position pos on pos.position_id=dr.position_id
		left join Dept on dept.dept_id=dr.dept_id
	ORDER BY driver_fio
END
GO
