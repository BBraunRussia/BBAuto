create procedure [dbo].[DeleteGrade]
  @id int
as
  delete from Grade where Id = @id
