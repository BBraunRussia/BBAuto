create procedure [dbo].[UpsertSsDTP]
  @idMark int,
  @idServiceStantion int
as
begin
  declare @count int
  select
    @count = count(mark_id)
  from
    ssDTP
  where
    mark_id = @idMark
  if (@count = 0)
    insert into ssDTP values(@idMark, @idServiceStantion)
  else
    update ssDTP
    set serviceStantion_id = @idServiceStantion
end
