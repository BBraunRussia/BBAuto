CREATE PROCEDURE [dbo].[Invoice_Delete]
@idInvoice int
AS
BEGIN
	DELETE FROM Invoice WHERE invoice_id=@idInvoice
END
GO
