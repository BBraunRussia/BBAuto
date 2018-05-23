create procedure [dbo].[DeleteViolationType]
  @id int
as
begin
  delete from ViolationType
  where Id = @id
end
