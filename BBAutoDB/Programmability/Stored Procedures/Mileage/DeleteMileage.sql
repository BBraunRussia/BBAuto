create procedure [dbo].[DeleteMileage]
  @id int
as
  delete from Mileage where Id = @id
