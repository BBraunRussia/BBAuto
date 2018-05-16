create procedure [dbo].[DeleteSaleCar]
  @carId int
as
  delete from CarSale where CarId = @carId