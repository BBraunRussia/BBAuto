create procedure [dbo].[Invoice_Delete]
  @idInvoice int
as
  delete from Invoice
  where invoice_id = @idInvoice
