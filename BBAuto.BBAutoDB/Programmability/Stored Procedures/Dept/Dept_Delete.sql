CREATE PROCEDURE [dbo].[Dept_Delete]
@id int
AS
BEGIN
	DELETE FROM Dept WHERE dept_id=@id
END
GO
