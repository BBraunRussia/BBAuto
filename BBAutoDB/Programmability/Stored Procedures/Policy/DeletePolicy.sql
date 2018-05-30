create procedure [dbo].[DeletePolicy]
  @id int
as
  delete from Policy
  where Id = @id
