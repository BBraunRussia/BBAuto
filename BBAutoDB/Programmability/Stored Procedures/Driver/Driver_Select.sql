CREATE PROCEDURE [dbo].[Driver_Select]
AS
BEGIN
	SELECT dr.Id, Fio, RegionId, DateBirth, Mobile, Email,
		Fired, ExpSince, dr.PositionId, dr.DeptId, Login, OwnerId,
		SuppyAddress, Sex, Decret, DateStopNotification,
		Number, IsDriver, From1C
	FROM Driver dr
		left join Position pos on pos.position_id=dr.PositionId
		left join Dept on dept.dept_id=dr.DeptId
	ORDER BY Fio
END
GO
