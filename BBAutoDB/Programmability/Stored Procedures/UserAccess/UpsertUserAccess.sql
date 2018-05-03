create procedure [dbo].[UpsertUserAccess]
  @idDriver int,
  @idRole int
as
begin
  declare @count int
  select
    @count = count(*)
  from
    UserAccess
  where
    driver_id = @idDriver

  if (@count = 0)
    insert into UserAccess values(@idDriver, @idRole)
  else
    update UserAccess
    set role_id = @idRole
    where driver_id = @idDriver
end
