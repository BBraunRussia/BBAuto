create procedure [dbo].[DeleteEmployee]
  @idRegion int,
  @idEmployeeName int
as
begin
  delete from Employees
  where region_id = @idRegion
    and EmployeesName_id = @idEmployeeName
end
