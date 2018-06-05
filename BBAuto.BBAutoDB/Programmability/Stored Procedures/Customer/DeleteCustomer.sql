create procedure [dbo].[Customer_Delete]
  @id int
as
  delete from Customer where Id = @id
