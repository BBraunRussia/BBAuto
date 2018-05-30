create procedure [dbo].[GetPolicyById]
  @id int
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
