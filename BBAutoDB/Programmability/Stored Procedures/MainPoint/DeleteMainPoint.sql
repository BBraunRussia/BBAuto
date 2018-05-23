create procedure [dbo].[DeleteMainPoint]
  @id int
as
begin
  delete from MainPoint
  where PointId = @id
end
