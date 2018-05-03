CREATE PROCEDURE [dbo].[CulpritSelectWithUser]
@idCar int,
@date datetime
AS
BEGIN
	SELECT * INTO #table1 FROM GetDriverCars()
	
	Declare @idDriver int
	
	SELECT @idDriver=driver_id
	FROM #table1
	WHERE car_id=@idCar and @date >= date1 and @date < date2
	
	SELECT culprit_id, culprit_name FROM Culprit WHERE culprit_id != 4
	UNION
	SELECT 4, Fio culprit_name FROM Driver WHERE Id=@idDriver
END
GO
