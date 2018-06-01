CREATE PROCEDURE [dbo].[Template_Select_ByName]
@name varchar(50)
AS
BEGIN
	SELECT template_id, template_path FROM Template WHERE template_name=@name
END
GO
