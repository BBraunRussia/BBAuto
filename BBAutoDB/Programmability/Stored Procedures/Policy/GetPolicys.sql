create procedure [dbo].[GetPolicys]
as
begin
  select
    policy_id,
    car_id,
    policyType_id,
    owner_id,
    comp_id,
    policy_number,
    policy_dateBegin,
    policy_dateEnd,
    policy_pay1,
    policy_file,
    policy_limitCost,
    policy_pay2,
    policy_pay2Date,
    account_id,
    account_id2,
    policy_notificationSent,
    policy_comment,
    policy_dateCreate
  from
    Policy
end
