CREATE PROCEDURE [dbo].[Model_Delete]
@id int
AS
BEGIN
	Declare @count int
	
	SELECT @count=count(model_id)
	FROM Grade
	WHERE model_id=@id
	
	if (@count = 0)
	begin
		DELETE FROM Model WHERE model_id=@id
		SELECT 'Удален'
	end
	else
	begin
		SELECT 'Удаление невозможно. Надено зависимых записей: ' + CAST(@count as Varchar(50)) + ' шт'
	end
END
GO
