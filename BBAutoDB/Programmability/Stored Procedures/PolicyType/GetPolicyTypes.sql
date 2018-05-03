create procedure [dbo].[GetPolicyTypes]
  @all int = 0
as
begin
  if (@all = 0)
    select policyType_id, policyType_name 'Название' from policyType
  else
    select
      0 policyType_id,
      '(все)' 'Название'
    union
    select
      policyType_id,
      policyType_name 'Название'
    from
      policyType
end
