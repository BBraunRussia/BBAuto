create procedure [dbo].[ssDTP_Insert]
  @markId int,
  @serviceStantionId int
as
  if not exists(select * from ssDTP where mark_id = @markId)
    insert into ssDTP values(@markId, @serviceStantionId)
  else
    update
      ssDTP
    set
      serviceStantion_id = @serviceStantionId
    where
      mark_id = @markId
