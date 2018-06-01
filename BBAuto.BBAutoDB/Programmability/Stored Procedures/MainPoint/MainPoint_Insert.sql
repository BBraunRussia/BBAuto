CREATE PROCEDURE [dbo].[MainPoint_Insert]
@id int
AS
BEGIN
	INSERT INTO MainPoint VALUES(@id)
END
GO
