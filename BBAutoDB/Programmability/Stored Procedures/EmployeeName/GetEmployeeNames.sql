create procedure [dbo].[GetEmployeeNames]
as
begin
  select employeesName_id, employeesName_name 'Название' from EmployeesName
end
