create procedure [dbo].[DeleteStatus]
  @id int
as
begin
  delete from Status
  where Id = @id
end
