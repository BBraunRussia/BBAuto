create procedure [dbo].[UpsertSuppyAddress]
  @idMyPoint int
as
begin
  declare @idRegion int
  select region_id from MyPoint where myPoint_id = @idMyPoint

  declare @count int

  select
    @count = count(*)
  from
    SuppyAddress sa
    join MyPoint mp
      on sa.myPoint_id = mp.myPoint_id
  where
    sa.myPoint_id = @idMyPoint
    or mp.region_id = @idRegion

  if (@count = 0)
    insert into SuppyAddress values(@idMyPoint)
end
