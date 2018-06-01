CREATE PROCEDURE [dbo].[ProxyType_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(ProxyType_id) FROM ProxyType WHERE ProxyType_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO ProxyType VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данный тип доверенности уже существует'
	end
	else
	begin
		UPDATE ProxyType SET ProxyType_name=@Name WHERE ProxyType_id=@id
		SELECT 'Обновлен'
	end
END
GO
