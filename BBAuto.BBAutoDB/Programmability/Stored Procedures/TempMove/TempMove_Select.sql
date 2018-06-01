CREATE PROCEDURE [dbo].[TempMove_Select]
AS
BEGIN
	SELECT tempMove_id, car_id, driver_id, tempMove_dateBegin, tempMove_dateEnd FROM TempMove
END
GO
