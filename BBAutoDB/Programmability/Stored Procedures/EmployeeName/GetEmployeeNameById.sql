CREATE PROCEDURE [dbo].[GetEmployeeNameById]
  @id int
as
  select Id, [Name] from EmployeesName where Id = @id
