CREATE PROCEDURE [dbo].[Mark_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(mark_id) FROM Mark WHERE mark_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO Mark VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Марка автомобиля с таким названием уже существует'
	end
	else
	begin
		UPDATE Mark SET mark_name=@Name WHERE mark_id=@id
		SELECT 'Обновлен'
	end
END
GO
