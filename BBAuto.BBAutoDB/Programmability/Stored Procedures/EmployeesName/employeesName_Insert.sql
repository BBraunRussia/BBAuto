CREATE PROCEDURE [dbo].[employeesName_Insert]
@id int,
@name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		INSERT INTO employeesName VALUES(@name)
		SET @id = SCOPE_IDENTITY()
	end
	else
		UPDATE employeesName SET employeesName_name=@name WHERE employeesName_id=@id
		
	SELECT @id
END
GO
