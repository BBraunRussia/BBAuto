create procedure [dbo].[DeleteEmployeeName]
  @id int
as
begin
  delete from EmployeesName
  where employeesName_id = @id
end
