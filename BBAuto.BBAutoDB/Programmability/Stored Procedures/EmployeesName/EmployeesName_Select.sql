CREATE PROCEDURE [dbo].[EmployeesName_Select]
AS
BEGIN
	SELECT employeesName_id, employeesName_name 'Название' FROM employeesName
END
GO
