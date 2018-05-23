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
    MypointId1 = @id
    or MypointId2 = @id

  if (@count = 0)
    delete from MyPoint
    where Id = @id
end
