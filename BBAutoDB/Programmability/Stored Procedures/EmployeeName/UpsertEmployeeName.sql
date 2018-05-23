create procedure [dbo].[UpsertEmployeeName]
  @id int,
  @name nvarchar(MAX)
as
  if (@id = 0)
  begin
    insert into EmployeesName values(@name)
    set @id = scope_identity()
  end
  else
    update
      EmployeesName
    set
      [Name] = @name
    where
      Id = @id

  exec dbo.GetEmployeeNameById @id
