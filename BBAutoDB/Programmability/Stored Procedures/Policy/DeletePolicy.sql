create procedure [dbo].[DeletePolicy]
  @idPolicy int
as
begin
  delete from Policy
  where policy_id = @idPolicy
end
