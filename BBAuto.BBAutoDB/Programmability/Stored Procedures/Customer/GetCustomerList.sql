create procedure [dbo].[Customer_Select]
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
