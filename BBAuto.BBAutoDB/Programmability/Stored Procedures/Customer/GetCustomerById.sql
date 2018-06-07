create procedure [dbo].[GetCustomerById]
  @id int
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
  where
    Id = @id
