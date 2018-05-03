create procedure [dbo].[DeleteWayBillDay]
  @id int
as
begin
  delete from WayBillDay
  where wayBillDay_id = @id
end
