create procedure [dbo].[DeleteCarSale]
  @id int
as
  delete from CarSale where car_id = @id
