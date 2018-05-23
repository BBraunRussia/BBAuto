create procedure [dbo].[DeleteDept]
  @id int
as
begin
  delete from Dept
  where Id = @id
end
