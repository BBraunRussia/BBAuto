CREATE PROCEDURE [dbo].[SuppyAddress_Delete]
@idMyPoint int
AS
BEGIN
	DELETE FROM SuppyAddress WHERE myPoint_id=@idMyPoint
END
GO
