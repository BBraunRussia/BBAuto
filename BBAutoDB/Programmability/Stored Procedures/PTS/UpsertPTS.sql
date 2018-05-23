create procedure [dbo].[UpsertPTS]
  @CarId int,
  @number nvarchar(50),
  @date datetime,
  @giveOrg nvarchar(100),
  @file nvarchar(200)
as
begin
  if (@CarId = 0)
    return

  declare @count int
  select
    @count = count(*)
  from
    PTS
  where
    CarId = @CarId

  if (@count = 0)
  begin
    insert into PTS values(@CarId, @number, @date, @giveOrg, @file)
  end
  else
  begin
    update PTS
    set Number = @number,
        [Date] = @date,
        GiveOrg = @giveOrg,
        [File] = @file
    where CarId = @CarId
  end
end
