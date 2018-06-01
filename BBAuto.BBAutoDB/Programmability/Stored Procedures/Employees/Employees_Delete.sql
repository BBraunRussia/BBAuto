CREATE PROCEDURE [dbo].[Employees_Delete]
@idRegion int,
@idEmployeesName int
AS
BEGIN
	DELETE FROM Employees WHERE region_id=@idRegion and EmployeesName_id=@idEmployeesName
END
GO
