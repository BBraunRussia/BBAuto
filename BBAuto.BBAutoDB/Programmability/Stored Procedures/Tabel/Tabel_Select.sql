CREATE PROCEDURE [dbo].[Tabel_Select]
AS
BEGIN
	SELECT driver_id, tabel_date, tabel_comment FROM Tabel
	order by driver_id, tabel_date
END
GO
