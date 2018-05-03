create procedure [dbo].[DeleteSuppyAddress]
  @idMyPoint int
as
begin
  delete from SuppyAddress
  where mypoint_id = @idMyPoint
end
