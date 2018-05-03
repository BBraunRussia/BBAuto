create procedure [dbo].[UpsertEmployeeName]
  @id int,
  @name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    insert into EmployeesName values(@name)
    set @id = scope_identity()
  end
  else
    update EmployeesName
    set employeesName_name = @name
    where employeesName_id = @id

  select @id
end
