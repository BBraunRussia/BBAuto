CREATE PROCEDURE [dbo].[Template_Insert]
@idTemplate int,
@name varchar(50),
@path varchar(200)
AS
BEGIN
	if (@idTemplate = 0)
		INSERT INTO Template VALUES(@name, @path)
	else
		UPDATE Template
		SET template_name=@name, template_path=@path
		WHERE template_id=@idTemplate
END
GO
