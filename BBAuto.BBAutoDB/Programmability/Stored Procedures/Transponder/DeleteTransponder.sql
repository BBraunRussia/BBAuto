create procedure [dbo].[DeleteTransponder]
  @id int
as
  delete from DriverTransponder where TransponderId = @id
  delete from Transponder where Id = @id
