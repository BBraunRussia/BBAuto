create procedure [dbo].[DeleteFuelCardType]
  @id int
as
  delete from FuelCardType where Id = @id
