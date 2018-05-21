create procedure dbo.GetSaleCars
as
  select CarId, [Date], Comment from SaleCar
