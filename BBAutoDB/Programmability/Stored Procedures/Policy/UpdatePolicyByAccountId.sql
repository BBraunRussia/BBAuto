create procedure [dbo].[UpdatePolicyByAccountId]
  @idPolicy int,
  @idAccount int,
  @idNumber int
as
begin
  if (@idNumber = 1)
    update Policy
    set account_id = @idAccount
    where policy_id = @idPolicy
  else
    update Policy
    set account_id2 = @idAccount
    where policy_id = @idPolicy
end
