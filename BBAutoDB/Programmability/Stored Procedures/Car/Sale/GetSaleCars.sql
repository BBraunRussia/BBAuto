create procedure [dbo].[GetSaleCars]
as
begin
  select CarId, Date, Comment from CarSale
end
