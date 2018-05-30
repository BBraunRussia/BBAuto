create procedure [dbo].[GetPolicyListByAccountId]
  @accountId int
as
  select
    Id,
    CarId,
    PolicyTypeId,
    OwnerId,
    CompId,
    Number,
    DateBegin,
    DateEnd,
    Pay1,
    [File],
    LimitCost,
    Pay2,
    Pay2Date,
    AccountId,
    AccountId2,
    NotificationSent,
    Comment,
    DateCreate
  from
    Policy
  where
    AccountId = @accountId or AccountId2 = @accountId
