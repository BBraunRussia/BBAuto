CREATE PROCEDURE [dbo].[Account_Insert]
@idAccount int,
@Number varchar(50),
@Agreed int,
@idPolicyType int,
@idOwner int,
@paymentNumber int,
@businessTrip int,
@file varchar(100)
AS
BEGIN
	if (@idAccount = 0)
	begin
		INSERT INTO Account VALUES(@Number, 0, @idPolicyType, @idOwner, @paymentNumber, 
			@businessTrip, @file)
		
		SET @idAccount = SCOPE_IDENTITY()
	end
	else
		UPDATE Account SET account_number=@Number, account_agreed=@Agreed,
			policyType_id=@idPolicyType, owner_id=@idOwner, account_paymentNumber=@paymentNumber,
			account_businessTrip=@businessTrip, account_file=@file
		WHERE account_id=@idAccount
	
	SELECT @idAccount
END
GO
