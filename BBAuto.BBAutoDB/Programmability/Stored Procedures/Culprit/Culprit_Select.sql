CREATE PROCEDURE [dbo].[Culprit_Select]
@actual int = 0
AS
BEGIN
	SELECT culprit_id, culprit_name 'Название' FROM culprit ORDER BY culprit_name
END
GO
