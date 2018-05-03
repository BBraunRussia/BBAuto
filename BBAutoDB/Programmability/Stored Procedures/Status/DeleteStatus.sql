create procedure [dbo].[DeleteStatus]
  @id int
as
begin
  delete from Status
  where status_id = @id
end
