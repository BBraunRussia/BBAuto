create procedure [dbo].[UpsertEmployee]
  @RegionId int,
  @EmployeesNameId int,
  @DriverId int
as
begin
  declare @count int

  select
    @count = count(*)
  from
    Employees
  where
    RegionId = @RegionId
    and EmployeesNameId = @EmployeesNameId

  if (@count = 0)
    insert into Employees values(@RegionId, @EmployeesNameId, @DriverId)
  else
  begin
    update Employees
    set DriverId = @DriverId
    where RegionId = @RegionId
    and EmployeesNameId = @EmployeesNameId
  end
end
