CREATE PROCEDURE [dbo].[Policy_Delete_AccountID]
@idPolicy int,
@idNumber int
AS
BEGIN
	if (@idNumber = 1)
		UPDATE Policy SET account_id=0 WHERE policy_id=@idPolicy
	else
		UPDATE Policy SET account_id2=0 WHERE policy_id=@idPolicy
END
GO
