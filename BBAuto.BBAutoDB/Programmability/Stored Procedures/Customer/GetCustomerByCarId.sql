create procedure [dbo].[GetCustomerByCarId]
  @carId int
as
  select
    cust.Id,
    cust.FirstName,
    cust.LastName,
    cust.SecondName,
    cust.PassportNumber,
    cust.PassportGiveOrg,
    cust.PassportGiveDate,
    cust.Address,
    cust.Inn
  from
    Customer cust
    join CarSale cs
      on cs.CustomerId = cust.Id
  where
    cs.car_id = @carId
