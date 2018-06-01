CREATE PROCEDURE [dbo].[Invoice_Select]
AS
BEGIN
	SELECT invoice_id, car_id, invoice_number, driver_id_From, driver_id_To, invoice_date,
		invoice_DateMove, region_id_From, region_id_To, invoice_file
	FROM Invoice
		join Driver d1 on d1.driver_id = driver_id_From
		join Driver d2 on d2.driver_id = driver_id_To
	
	order by invoice_id
END
GO
