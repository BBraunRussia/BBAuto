create procedure [dbo].[DeletePolicy]
  @id int
as
begin
  delete from Policy
  where Id = @id
end
