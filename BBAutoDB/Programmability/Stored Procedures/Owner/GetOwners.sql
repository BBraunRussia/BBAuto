create procedure [dbo].[GetOwners]
  @all int = 0
as
begin
  if (@all = 1)
  begin
    select
      Id,
      [Name]
    from
      Owner
    union
    select
      0,
      '(все)'
    order by
      [Name]
  end
  else
  begin
    select Id, [Name] from Owner
  end
end
