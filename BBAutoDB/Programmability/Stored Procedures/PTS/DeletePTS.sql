create procedure [dbo].[DeletePTS]
  @CarId int
as
begin
  delete from PTS
  where CarId = @CarId
end
