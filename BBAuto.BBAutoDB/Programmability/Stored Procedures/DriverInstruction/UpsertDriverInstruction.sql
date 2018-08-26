create procedure [dbo].[UpsertDriverInstruction]
  @id int,
  @documentId int,
  @driverId int,
  @date datetime
as
  if (@id = 0)
  begin
    insert into DriverInstruction values(@documentId, @driverId, @date)

    set @id = scope_identity()
  end
  else
    update
      DriverInstruction
    set
      DocumentId = @documentId,
      @driverId = @driverId,
      Date = @date
    where
      Id = @id

  select @id
