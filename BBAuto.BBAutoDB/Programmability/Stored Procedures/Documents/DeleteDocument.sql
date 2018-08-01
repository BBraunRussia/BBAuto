create procedure [dbo].[DeleteDocument]
  @id int
as
  delete from Documents where Id = @id
