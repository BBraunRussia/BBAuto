CREATE PROCEDURE [dbo].[Color_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(Color_id) FROM Color WHERE Color_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO Color VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данный цвет кузова уже существует'
	end
	else
	begin
		UPDATE Color SET Color_name=@Name WHERE Color_id=@id
		SELECT 'Обновлен'
	end
END

GO
