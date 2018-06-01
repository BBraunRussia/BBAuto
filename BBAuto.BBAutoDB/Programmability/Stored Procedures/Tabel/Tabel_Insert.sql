CREATE PROCEDURE [dbo].[Tabel_Insert]
@idDriver int,
@date datetime,
@comment varchar(50) = NULL
AS
BEGIN
	Declare @count int
	SELECT @count=COUNT(*) FROM Tabel WHERE driver_id=@idDriver and tabel_date=@date
	
	if (@comment = '')
		SET @comment = NULL
	
	if (@count = 0)
	begin
		INSERT INTO Tabel VALUES(@idDriver, @date, @comment)
	end
END
GO
