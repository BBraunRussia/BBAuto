create procedure [dbo].[GetCustomerList]
as
  select
    Id,
    FirstName,
    LastName,
    SecondName,
    PassportNumber,
    PassportGiveOrg,
    PassportGiveDate,
    Address,
    Inn
  from
    Customer
  order by
    LastName, FirstName, SecondName
