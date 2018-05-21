create procedure [dbo].[DeleteSaleCar]
  @carId int
as
  delete from SaleCar where CarId = @carId
