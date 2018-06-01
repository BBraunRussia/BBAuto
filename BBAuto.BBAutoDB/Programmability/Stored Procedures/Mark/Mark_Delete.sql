CREATE PROCEDURE [dbo].[Mark_Delete]
@id int
AS
BEGIN
	Declare @count int
	
	SELECT @count=count(mark_id)
	FROM Model
	WHERE mark_id=@id
	
	if (@count = 0)
	begin
		DELETE FROM Mark WHERE mark_id=@id
		SELECT 'Удален'
	end
	else
	begin
		SELECT 'Удаление невозможно. Надено зависимых записей: ' + CAST(@count as Varchar(50)) + ' шт'
	end
END
GO
