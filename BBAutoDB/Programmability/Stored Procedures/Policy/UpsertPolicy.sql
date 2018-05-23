create procedure [dbo].[UpsertPolicy]
  @id int,
  @PolicyTypeId int,
  @CarId int,
  @OwnerId int,
  @CompId int,
  @Number nvarchar(50),
  @dateBegin datetime,
  @dateEnd datetime,
  @pay1 float,
  @cost float,
  @pay2 float,
  @pay2DateText nvarchar(50),
  @file nvarchar(100),
  @notificationSent bit,
  @comment nvarchar(100)
as
begin
  declare @pay2Date datetime

  if (@pay2DateText = '')
    set @pay2Date = null
  else
    set @pay2Date = cast(@pay2DateText as datetime)

  if (@id = 0)
  begin
    insert into dbo.[Policy]
      values(@CarId, @PolicyTypeId, @OwnerId, @CompId, @Number, @dateBegin, @dateEnd, @pay1, @cost, @pay2, @pay2Date, @file, null, null, 0, @comment, current_timestamp)

    set @id = scope_identity()
  end
  else
  begin
    update Policy
    set OwnerId = @OwnerId,
        CompId = @CompId,
        Number = @Number,
        DateBegin = @dateBegin,
        DateEnd = @dateEnd,
        Pay1 = @pay1,
        LimitCost = @cost,
        Pay2 = @pay2,
        Pay2Date = @pay2Date,
        [File] = @file,
        NotificationSent = @notificationSent,
        Comment = @comment
    where Id = @id
  end

  select @id
end
