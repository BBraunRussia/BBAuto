create procedure [dbo].[DeleteEmployee]
  @RegionId int,
  @EmployeeNameId int
as
begin
  delete from Employees
  where RegionId = @RegionId
    and EmployeesNameId = @EmployeeNameId
end
