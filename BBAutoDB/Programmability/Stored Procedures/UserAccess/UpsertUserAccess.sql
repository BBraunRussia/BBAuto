create procedure [dbo].[UpsertUserAccess]
  @DriverId int,
  @RoleId int
as
begin
  declare @count int
  select
    @count = count(*)
  from
    UserAccess
  where
    DriverId = @DriverId

  if (@count = 0)
    insert into UserAccess values(@DriverId, @RoleId)
  else
    update UserAccess
    set RoleId = @RoleId
    where DriverId = @DriverId
end
