create procedure [dbo].[DeletePolicyByAccountId]
  @id int,
  @NumberId int
as
begin
  if (@NumberId = 1)
    update Policy
    set AccountId = 0
    where Id = @id
  else
    update Policy
    set AccountId2 = 0
    where Id = @id
end
