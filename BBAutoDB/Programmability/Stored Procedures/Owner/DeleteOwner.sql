create procedure [dbo].[DeleteOwner]
  @id int
as
  delete from dbo.[Owner] where Id = @id
