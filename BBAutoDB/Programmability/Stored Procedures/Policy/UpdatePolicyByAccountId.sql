create procedure [dbo].[UpdatePolicyByAccountId]
  @id int,
  @AccountId int,
  @NumberId int
as
  if (@NumberId = 1)
    update Policy
    set AccountId = @AccountId
    where Id = @id
  else
    update Policy
    set AccountId2 = @AccountId
    where Id = @id
