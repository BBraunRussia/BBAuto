CREATE PROCEDURE [dbo].[Position_Insert]
@id int,
@Name varchar(100)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(Position_id) FROM Position WHERE Position_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO Position VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данная должность уже существует'
	end
	else
	begin
		UPDATE Position SET Position_name=@Name WHERE Position_id=@id
		SELECT 'Обновлен'
	end
END
GO
