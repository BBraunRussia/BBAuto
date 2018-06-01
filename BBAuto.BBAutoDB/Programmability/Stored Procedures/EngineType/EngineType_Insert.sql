CREATE PROCEDURE [dbo].[EngineType_Insert]
@id int,
@Name varchar(50),
@ShortName varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(EngineType_id) FROM EngineType WHERE EngineType_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO EngineType VALUES(@Name, @ShortName)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Такой тип двигателя уже существует'
	end
	else
	begin
		UPDATE EngineType
		SET EngineType_name=@Name, engineType_shortName=@ShortName
		WHERE EngineType_id=@id
		
		SELECT 'Обновлен'
	end
END
GO
