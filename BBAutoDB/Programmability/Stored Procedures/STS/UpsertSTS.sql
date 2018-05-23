create procedure [dbo].[UpsertSTS]
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
    STS
  where
    CarId = @CarId

  if (@count = 0)
    insert into STS values(@CarId, @number, @date, @giveOrg, @file)
  else
    update STS
    set Number = @number,
        [Date] = @date,
        GiveOrg = @giveOrg,
        [File] = @file
    where
      CarId = @CarId
end
