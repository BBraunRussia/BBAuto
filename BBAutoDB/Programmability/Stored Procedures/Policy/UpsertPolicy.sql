create procedure [dbo].[UpsertPolicy]
  @idPolicy int,
  @idPolicyType int,
  @idCar int,
  @idOwner int,
  @idComp int,
  @Number nvarchar(50),
  @dateBegin datetime,
  @dateEnd datetime,
  @pay1 float,
  @cost float,
  @pay2 float,
  @pay2DateText nvarchar(50),
  @file nvarchar(100),
  @notificationSent int,
  @comment nvarchar(100)
as
begin
  declare @pay2Date datetime

  if (@pay2DateText = '')
    set @pay2Date = null
  else
    set @pay2Date = cast(@pay2DateText as datetime)

  if (@idPolicy = 0)
  begin
    insert into Policy values(@idCar, @idPolicyType, @idOwner, @idComp, @Number, @dateBegin, @dateEnd, @pay1, @cost, @pay2, @pay2Date, @file, null, null, 0, @comment, current_timestamp)

    set @idPolicy = scope_identity()
  end
  else
  begin
    update Policy
    set owner_id = @idOwner,
        comp_id = @idComp,
        policy_number = @Number,
        policy_dateBegin = @dateBegin,
        policy_dateEnd = @dateEnd,
        policy_pay1 = @pay1,
        policy_limitCost = @cost,
        policy_pay2 = @pay2,
        policy_pay2Date = @pay2Date,
        policy_file = @file,
        policy_notificationSent = @notificationSent,
        policy_comment = @comment
    where policy_id = @idPolicy
  end

  select @idPolicy
end
