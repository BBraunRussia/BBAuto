create procedure [dbo].[DeleteLicense]
  @id int
as
  delete from License where Id = @id