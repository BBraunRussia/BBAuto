CREATE PROCEDURE [dbo].[Dept_Insert]
@id int,
@Name varchar(100)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(dept_id) FROM Dept WHERE dept_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO Dept VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данный департамент уже существует'
	end
	else
	begin
		UPDATE Dept SET dept_name=@Name WHERE dept_id=@id
		SELECT 'Обновлен'
	end
END

GO
