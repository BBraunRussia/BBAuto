create procedure [dbo].[DeleteRegion]
  @id int
as
  delete from Region where Id = @id
