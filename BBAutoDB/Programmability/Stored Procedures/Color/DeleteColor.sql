create procedure [dbo].[DeleteColor]
  @id int
as
  delete from Color where Id = @id