create procedure [dbo].[DeleteCarDoc]
  @id int
as
  delete from CarDoc
  where Id = @id
