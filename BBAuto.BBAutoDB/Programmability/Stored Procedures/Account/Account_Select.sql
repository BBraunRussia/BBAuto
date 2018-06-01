CREATE PROCEDURE [dbo].[Account_Select]
AS
BEGIN
	SELECT account_id, account_number, account_agreed, policyType_id, owner_id,
		account_paymentNumber, account_businessTrip, account_file FROM Account
END
GO
