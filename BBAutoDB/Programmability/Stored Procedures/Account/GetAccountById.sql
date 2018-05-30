create procedure [dbo].[GetAccountById]
  @id int
as
 select
    Id,
    Number,
    Agreed,
    PolicyTypeId,
    OwnerId,
		PaymentNumber,
    BusinessTrip,
    [File]
  from
    Account
  where
    Id = @id
