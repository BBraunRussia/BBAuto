CREATE PROCEDURE [dbo].[Diller_Insert]
@idDiller int,
@Name varchar(100),
@Contacts varchar(500)
AS
BEGIN
	if (@idDiller = 0)
	begin
		INSERT INTO Diller VALUES(@Name, @Contacts)
		SELECT 'Add'
	end
	else
	begin
		UPDATE Diller SET diller_name=@Name, diller_contacts=@Contacts WHERE diller_id=@idDiller
		SELECT 'Update'
	end	
END
GO
