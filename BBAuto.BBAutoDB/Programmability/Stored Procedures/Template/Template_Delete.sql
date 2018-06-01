CREATE PROCEDURE [dbo].[Template_Delete]
@idTemplate int
AS
BEGIN
	DELETE FROM Template WHERE template_id=@idTemplate
END
GO
