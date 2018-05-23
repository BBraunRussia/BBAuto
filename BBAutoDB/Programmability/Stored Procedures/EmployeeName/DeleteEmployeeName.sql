create procedure [dbo].[DeleteEmployeeName]
  @id int
as
  delete from EmployeesName where Id = @id
