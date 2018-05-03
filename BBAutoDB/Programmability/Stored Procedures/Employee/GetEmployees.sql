create procedure [dbo].[GetEmployees]
as
begin
  select
    region_id,
    employeesName_id,
    driver_id
  from
    Employees
  order by
    region_id,
    employeesName_id
end
