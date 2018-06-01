CREATE PROCEDURE [dbo].[Violation_Insert]
@idViolation int,
@idCar int,
@date datetime,
@number varchar(50),
@file varchar(200),
@datePay varchar(50),
@filePay varchar(200),
@idViolationType int,
@sum int,
@sent int,
@noDeduction int,
@agreed varchar(5) = 'False'
AS
BEGIN
	declare @event varchar(50)

	if (@datePay = '')
		SET @datePay = NULL
	else
		SET @datePay = CAST(@datePay as DateTime)
	
	if (@idViolation = 0)
	begin
		Declare @count int
		SELECT @count=count(violation_id)
		FROM Violation
		WHERE violation_date=@date and violation_number=@number
		
		INSERT INTO Violation
			VALUES(@idCar, @date, @number, @file, @datePay, @filePay, @idViolationType, @sum, 0,
				@noDeduction, @agreed, CURRENT_TIMESTAMP)
		
		set @event = 'insert'
		
		SET @idViolation = SCOPE_IDENTITY()
	end
	else
	begin
		UPDATE Violation
		SET violation_date=@date, violation_number=@number, violation_file=@file,
			violation_datePay=@datePay, violation_filePay=@filePay, violationType_id=@idViolationType,
			violation_sum=@sum, violation_sent=@sent, violation_noDeduction=@noDeduction,
			violation_agreed = @agreed
		WHERE violation_id=@idViolation
		
		set @event = 'update'
	end	
	
	exec History_Insert 'Violation', @idViolation, @event, @filePay
	
	SELECT @idViolation
END
GO
