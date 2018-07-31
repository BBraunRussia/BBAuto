create procedure [dbo].[UpsertTransponder]
  @id int,
  @number nvarchar(50),
  @regionId int,
  @lost bit,
  @comment nvarchar(500)
as
  if not exists(select 1 from Transponder t where t.Id = @id)
  begin
    insert into Transponder (Number, RegionId, Lost, Comment) values(@number, @regionId, @lost, @comment)
    set @id = scope_identity()
  end
  else
    update Transponder 
    set
      Number = @number,
      RegionId = @regionId,
      Lost = @lost,
      Comment = @comment
    where
      Id = @id

  exec dbo.GetTransponderById @id
