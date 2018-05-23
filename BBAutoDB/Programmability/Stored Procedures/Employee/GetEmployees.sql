create procedure [dbo].[GetEmployees]
as
begin
  select
    RegionId,
    EmployeesNameId,
    DriverId
  from
    Employees
  order by
    RegionId,
    EmployeesNameId
end
