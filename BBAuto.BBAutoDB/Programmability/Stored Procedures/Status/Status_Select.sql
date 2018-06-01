CREATE PROCEDURE [dbo].[Status_Select]
@all int = 0
AS
BEGIN
	SELECT status_id, status_name 'Название'
	FROM Status
	ORDER BY Status_seq
END
GO
