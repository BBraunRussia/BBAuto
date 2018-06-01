CREATE PROCEDURE [dbo].[Culprit_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(culprit_id) FROM culprit WHERE culprit_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO culprit VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данный виновник уже существует'
	end
	else
	begin
		UPDATE culprit SET culprit_name=@Name WHERE culprit_id=@id
		SELECT 'Обновлен'
	end
END
GO
