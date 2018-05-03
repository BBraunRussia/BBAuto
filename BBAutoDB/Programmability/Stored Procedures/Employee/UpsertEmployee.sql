create procedure [dbo].[UpsertEmployee]
  @idRegion int,
  @idEmployeesName int,
  @idDriver int
as
begin
  declare @count int

  select
    @count = count(region_id)
  from
    Employees
  where
    region_id = @idRegion
    and employeesName_id = @idEmployeesName

  if (@count = 0)
    insert into Employees values(@idRegion, @idEmployeesName, @idDriver)
  else
  begin
    update Employees
    set driver_id = @idDriver
    where region_id = @idRegion
    and employeesName_id = @idEmployeesName
  end
end
