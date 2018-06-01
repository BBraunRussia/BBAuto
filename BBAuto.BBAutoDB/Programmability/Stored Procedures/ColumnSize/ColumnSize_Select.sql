CREATE PROCEDURE [dbo].[ColumnSize_Select]
AS
BEGIN
	SELECT driver_id, status_id,
			column0, column1, column2, column3, column4, column5, column6,
			column7, column8, column9, column10, column11, column12, column13,
			column14, column15, column16
	FROM ColumnSize
END
GO
