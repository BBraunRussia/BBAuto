CREATE PROCEDURE [dbo].[Model_Insert]
@id int,
@Name varchar(50),
@idMark int
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(model_id) FROM Model WHERE model_name=@Name and mark_id=@idMark
	
		if (@count = 0)
		begin
			INSERT INTO Model VALUES(@Name, @idMark)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Модель автомобиля с таким названием уже существует'
	end
	else
	begin
		UPDATE Model SET model_name=@Name, mark_id=@idMark WHERE model_id=@id
		SELECT 'Обновлен'
	end
END
GO
