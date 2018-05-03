create procedure [dbo].[DeleteRegion]
  @id int
as
begin
  delete from Region
  where region_id = @id
end
