CREATE PROCEDURE [dbo].[UpsertAccount]
@idAccount int,
@Number nvarchar(50),
@Agreed int,
@idPolicyType int,
@idOwner int,
@paymentNumber int,
@businessTrip int,
@file nvarchar(100)
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
