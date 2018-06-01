CREATE PROCEDURE [dbo].[ServiceStantion_Select]
@actual int = 0
AS
BEGIN
	SELECT ServiceStantion_id, ServiceStantion_name 'Название'
	FROM ServiceStantion
	ORDER BY ServiceStantion_name
END
GO
