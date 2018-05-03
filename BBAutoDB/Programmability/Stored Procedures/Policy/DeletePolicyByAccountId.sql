create procedure [dbo].[DeletePolicyByAccountId]
  @idPolicy int,
  @idNumber int
as
begin
  if (@idNumber = 1)
    update Policy
    set account_id = 0
    where policy_id = @idPolicy
  else
    update Policy
    set account_id2 = 0
    where policy_id = @idPolicy
end
