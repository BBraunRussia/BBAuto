create procedure [dbo].[UpsertSuppyAddress]
  @MyPointId int
as
begin
  declare @RegionId int
  select @RegionId = RegionId from MyPoint where Id = @MyPointId

  declare @count int

  select
    @count = count(*)
  from
    SuppyAddress sa
    join MyPoint mp
      on sa.myPointId = mp.Id
  where
    sa.myPointId = @MyPointId
    or mp.RegionId = @RegionId

  if (@count = 0)
    insert into SuppyAddress values(@MyPointId)
end
