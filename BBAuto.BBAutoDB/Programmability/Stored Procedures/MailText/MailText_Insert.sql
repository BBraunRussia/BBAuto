CREATE PROCEDURE [dbo].[MailText_Insert]
@id int,
@name varchar(50),
@text varchar(500)
AS
BEGIN
	if (@id = 0)
	begin
		INSERT INTO MailText VALUES(@name, @text)
		
		SET @id = SCOPE_IDENTITY()
	end
	else
	begin
		UPDATE MailText
		SET mailText_name=@name, mailText_text=@text
		WHERE mailText_id=@id
	end
	
	SELECT @id
END
GO
