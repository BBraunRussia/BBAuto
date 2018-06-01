CREATE PROCEDURE [dbo].[Policy_Insert]
@idPolicy int,
@idPolicyType int,
@idCar int,
@idOwner int,
@idComp int,
@Number varchar(50),
@dateBegin datetime,
@dateEnd datetime,
@pay1 float,
@cost float,
@pay2 float,
@pay2DateText varchar(50),
@file varchar(100),
@notificationSent int,
@comment varchar(100)
AS
BEGIN
	Declare @pay2Date datetime
	
	if (@pay2DateText = '')
		SET @pay2Date = null
	else
		SET @pay2Date = CAST(@pay2DateText as datetime)	
		
	if (@idPolicy = 0)
	begin
		INSERT INTO Policy
		VALUES(@idCar, @idPolicyType, @idOwner, @idComp, @Number, @dateBegin, @dateEnd, @pay1, @cost, @pay2,
			@pay2Date, @file, NULL, NULL, 0, @comment, CURRENT_TIMESTAMP)
		
		SET @idPolicy = SCOPE_IDENTITY()
	end
	else
	begin
		UPDATE Policy
		SET owner_id=@idOwner, comp_id=@idComp, policy_number=@Number, policy_dateBegin=@dateBegin,
			policy_dateEnd=@dateEnd, policy_pay1=@pay1, policy_LimitCost=@cost, policy_pay2=@pay2,
			policy_pay2Date=@pay2Date, policy_file=@file, policy_notificationSent=@notificationSent,
			policy_comment=@comment
		WHERE policy_id=@idPolicy
	end
	
	SELECT @idPolicy
END
GO
