create procedure [dbo].[DeleteMyPoint]
  @id int
as
begin
  declare @count int
  select
    @count = count(*)
  from
    Route
  where
    mypoint1_id = @id
    or mypoint2_id = @id

  if (@count = 0)
    delete from MyPoint
    where myPoint_id = @id
end
