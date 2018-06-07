create procedure [dbo].[DeleteCustomer]
  @id int
as
  delete from Customer where Id = @id
