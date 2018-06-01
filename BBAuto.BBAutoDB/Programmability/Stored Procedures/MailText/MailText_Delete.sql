CREATE PROCEDURE [dbo].[MailText_Delete]
@id int
AS
BEGIN
	DELETE FROM MailText WHERE mailText_id=@id
END
GO
