CREATE PROCEDURE [dbo].[Policy_Delete]
@idPolicy int
AS
BEGIN
	DELETE FROM Policy WHERE policy_id=@idPolicy
END
GO
