create procedure [dbo].[DeleteMainPoint]
  @id int
as
begin
  delete from MainPoint
  where point_id = @id
end
