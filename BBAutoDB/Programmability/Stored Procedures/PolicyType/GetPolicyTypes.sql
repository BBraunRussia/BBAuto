create procedure [dbo].[GetPolicyTypes]
  @all int = 0
as
begin
  if (@all = 0)
    select Id, [Name] from policyType
  else
    select
      0 as 'Id',
      '(все)' as 'Name'
    union
    select
      Id,
      [Name]
    from
      policyType
end
