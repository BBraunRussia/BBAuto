create procedure [dbo].[GetAccountListForAgree]
as
 select
    a.Id,
    a.Number,
    a.Agreed,
    a.PolicyTypeId,
    a.OwnerId,
		a.PaymentNumber,
    a.BusinessTrip,
    a.[File]
  from
    Account a
  join Policy p
    on p.AccountId = a.Id or p.AccountId2 = a.Id
  where
    Agreed = 0
