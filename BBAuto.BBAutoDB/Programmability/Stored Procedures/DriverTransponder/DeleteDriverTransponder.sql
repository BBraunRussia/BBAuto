create procedure [dbo].[DeleteDriverTransponder]
  @id int
as
  delete from DriverTransponder where Id = @id
