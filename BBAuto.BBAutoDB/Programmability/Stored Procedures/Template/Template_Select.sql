CREATE PROCEDURE [dbo].[Template_Select]
AS
BEGIN
	SELECT template_id, template_name, template_path FROM Template
END
GO
