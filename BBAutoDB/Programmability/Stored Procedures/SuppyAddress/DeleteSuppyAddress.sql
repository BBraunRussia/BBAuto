create procedure [dbo].[DeleteSuppyAddress]
  @MyPointId int
as
begin
  delete from SuppyAddress
  where myPointId = @MyPointId
end
