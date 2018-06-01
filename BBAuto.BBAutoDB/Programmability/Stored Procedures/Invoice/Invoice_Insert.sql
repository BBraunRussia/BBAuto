CREATE PROCEDURE [dbo].[Invoice_Insert]
@idInvoice int,
@idCar int,
@number int,
@idDriverFrom int,
@idDriverTo int,
@date datetime,
@dateMoveText varchar(50),
@idRegionFrom int,
@idRegionTo int,
@file varchar(500)
AS
BEGIN	
	Declare @dateMove datetime
	
	if (@dateMoveText = '')
		SET @dateMove = null
	else
		SET @dateMove = CAST(@dateMoveText as datetime)	
		
	if (@idInvoice = 0)
	begin
		SELECT @number=max(invoice_number) + 1
		FROM Invoice
		WHERE YEAR(invoice_date)=YEAR(@date)
		
		if (@number is null)
			SET @number = 1
		
		INSERT INTO Invoice Values(@idCar, @number, @idDriverFrom, @idDriverTo, @date, @dateMove,
			@idRegionFrom, @idRegionTo, @file)
		SET @idInvoice = @@IDENTITY
	end
	else
	begin		
		UPDATE Invoice
		SET driver_id_From=@idDriverFrom, driver_id_To=@idDriverTo,
			invoice_date=@date, invoice_dateMove=@dateMove,
			region_id_From=@idRegionFrom, region_id_To=@idRegionTo,
			invoice_file=@file
		WHERE invoice_id=@idInvoice
	end
	
	SELECT @idInvoice
END
GO
