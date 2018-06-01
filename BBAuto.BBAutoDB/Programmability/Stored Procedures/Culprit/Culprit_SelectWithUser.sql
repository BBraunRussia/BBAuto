CREATE PROCEDURE [dbo].[Culprit_SelectWithUser]
@idCar int,
@date datetime
AS
BEGIN
	SELECT * INTO #table1 FROM DriverCar_Select()
	
	Declare @idDriver int
	
	SELECT @idDriver=driver_id
	FROM #table1
	WHERE car_id=@idCar and @date >= date1 and @date < date2
	
	SELECT culprit_id, culprit_name FROM Culprit WHERE culprit_id != 4
	UNION
	SELECT 4, driver_fio culprit_name FROM Driver WHERE driver_id=@idDriver
END
GO
