CREATE PROCEDURE [dbo].[Owner_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(Owner_id) FROM Owner WHERE Owner_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO Owner VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данный владелец уже существует'
	end
	else
	begin
		UPDATE Owner SET Owner_name=@Name WHERE Owner_id=@id
		SELECT 'Обновлен'
	end
END
GO
