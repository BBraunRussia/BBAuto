CREATE PROCEDURE [dbo].[ViolationType_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(ViolationType_id) FROM ViolationType WHERE ViolationType_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO ViolationType VALUES(@Name)
			SELECT 'Добавлен'
		end
		else
			SELECT 'Данный тип нарушения уже существует'
	end
	else
	begin
		UPDATE ViolationType SET ViolationType_name=@Name WHERE ViolationType_id=@id
		SELECT 'Обновлен'
	end
END
GO
