CREATE FUNCTION [dbo].[DateTime_To_Date]
	(@date datetime)
RETURNS varchar(50)
AS
BEGIN
	Declare @result varchar(50)
	SELECT @result = CASE WHEN DAY(@date) < 10 THEN '0' ELSE '' END
		+ CAST(DAY(@date) as varchar(50)) + '.'
		+ CASE WHEN MONTH(@date) < 10 THEN '0' ELSE '' END
		+ CAST(MONTH(@date) as varchar(50))
		+ '.' + CAST(YEAR(@date) as varchar(50));
		
	RETURN @result
END
GO
