CREATE PROCEDURE [dbo].[StatusAfterDTP_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(StatusAfterDTP_id) FROM StatusAfterDTP WHERE StatusAfterDTP_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO StatusAfterDTP VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данный статус уже существует'
	end
	else
	begin
		UPDATE StatusAfterDTP SET StatusAfterDTP_name=@Name WHERE StatusAfterDTP_id=@id
		SELECT 'Обновлен'
	end
END
GO
