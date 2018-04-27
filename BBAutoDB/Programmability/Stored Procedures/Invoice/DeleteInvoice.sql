create procedure [dbo].[DeleteInvoice]
  @idInvoice int
as
  delete from Invoice
  where Id = @idInvoice
