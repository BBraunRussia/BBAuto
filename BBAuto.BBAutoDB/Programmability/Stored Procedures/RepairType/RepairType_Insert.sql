CREATE PROCEDURE [dbo].[RepairType_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(RepairType_id) FROM RepairType WHERE RepairType_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO RepairType VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данный вид ремонта уже существует'
	end
	else
	begin
		UPDATE RepairType SET RepairType_name=@Name WHERE RepairType_id=@id
		SELECT 'Обновлен'
	end
END

GO
