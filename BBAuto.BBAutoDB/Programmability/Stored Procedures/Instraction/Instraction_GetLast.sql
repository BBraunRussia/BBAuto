CREATE PROCEDURE [dbo].[Instraction_GetLast]
@idDriver int
AS
BEGIN
	Declare @last varchar(100)
	
	SELECT top 1 @last = '№ '+ CAST(Instraction_number as varchar(50)) + ' до ' + 
		+ CASE WHEN DAY(Instraction_date) < 10 THEN '0' ELSE '' END
		+ CAST(DAY(Instraction_date) as varchar(50)) + '.'
		+ CASE WHEN MONTH(Instraction_date) < 10 THEN '0' ELSE '' END
		+ CAST(MONTH(Instraction_date) as varchar(50))
		+ '.' + CAST(YEAR(Instraction_date) as varchar(50))
	FROM Instraction
	WHERE driver_id=@idDriver
	ORDER BY Instraction_date desc
	
	if (@last is null)
		SELECT 'нет данных'
	else
		SELECT @last
END
GO
