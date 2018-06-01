CREATE PROCEDURE [dbo].[Employees_Insert]
@idRegion int,
@idEmployeesName int,
@idDriver int
AS
BEGIN
	Declare @count int
	
	SELECT @count = count(region_id)
	FROM Employees
	WHERE region_id=@idRegion and EmployeesName_id=@idEmployeesName
	
	if (@count = 0)
		INSERT INTO Employees VALUES(@idRegion, @idEmployeesName, @idDriver)
	else
	begin
		UPDATE Employees
		SET driver_id=@idDriver
		WHERE region_id=@idRegion and EmployeesName_id=@idEmployeesName
	end
END
GO
