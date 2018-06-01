CREATE PROCEDURE [dbo].[Employees_Select]
AS
BEGIN
	SELECT region_id, EmployeesName_id, driver_id FROM Employees
	ORDER BY region_id, EmployeesName_id
END
GO
