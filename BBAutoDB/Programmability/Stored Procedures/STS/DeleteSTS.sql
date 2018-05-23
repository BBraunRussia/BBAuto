create procedure [dbo].[DeleteSTS]
  @CarId int
as
begin
  delete from STS
  where CarId = @CarId
end
