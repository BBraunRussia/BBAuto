CREATE PROCEDURE [dbo].[employeesName_Delete]
@id int
AS
BEGIN
	DELETE FROM employeesName WHERE employeesName_id=@id
END
GO
