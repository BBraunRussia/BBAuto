create procedure [dbo].[UpsertSsDTP]
  @MarkId int,
  @ServiceStantionId int
as
begin
  declare @count int
  select
    @count = count(MarkId)
  from
    ssDTP
  where
    MarkId = @MarkId
  if (@count = 0)
    insert into ssDTP values(@MarkId, @ServiceStantionId)
  else
    update ssDTP
    set ServiceStantionId = @ServiceStantionId
end
