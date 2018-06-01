CREATE PROCEDURE [dbo].[MailText_Select]
AS
BEGIN
	SELECT mailText_id, mailText_name, mailText_text FROM MailText
END
GO
