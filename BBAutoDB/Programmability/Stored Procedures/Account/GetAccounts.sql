create procedure [dbo].[GetAccounts]
as
begin
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
end
