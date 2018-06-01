CREATE PROCEDURE [dbo].[Comp_Delete]
@id int
AS
BEGIN
	DELETE FROM Comp WHERE Comp_id=@id
END
GO
