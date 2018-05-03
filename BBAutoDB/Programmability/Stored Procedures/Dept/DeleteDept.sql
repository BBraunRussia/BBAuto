create procedure [dbo].[DeleteDept]
  @id int
as
begin
  delete from Dept
  where dept_id = @id
end
