create procedure [dbo].[GetAccounts]
as
begin
	select
    account_id,
    account_number,
    account_agreed,
    policyType_id,
    owner_id,
		account_paymentNumber,
    account_businessTrip,
    account_file
  from
    Account
end
