CREATE PROCEDURE [dbo].[Comp_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(Comp_id) FROM Comp WHERE Comp_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO Comp VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данная компания уже существует'
	end
	else
	begin
		UPDATE Comp SET Comp_name=@Name WHERE Comp_id=@id
		SELECT 'Обновлен'
	end
END
GO
