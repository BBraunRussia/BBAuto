CREATE PROCEDURE [dbo].[Policy_Insert_AccountID]
@idPolicy int,
@idAccount int,
@idNumber int
AS
BEGIN
	if (@idNumber = 1)
		UPDATE Policy SET account_id=@idAccount WHERE policy_id=@idPolicy
	else
		UPDATE Policy SET account_id2=@idAccount WHERE policy_id=@idPolicy
END
GO
