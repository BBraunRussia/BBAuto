CREATE PROCEDURE [dbo].[ServiceStantion_Delete]
@idServiceStantion int
AS
BEGIN
	DELETE FROM ServiceStantion WHERE ServiceStantion_id=@idServiceStantion
END
GO
