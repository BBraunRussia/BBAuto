create procedure [dbo].[GetAccounts]
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
