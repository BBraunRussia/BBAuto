create procedure [dbo].[DeleteWayBillDay]
  @id int
as
begin
  delete from WayBillDay
  where Id = @id
end
