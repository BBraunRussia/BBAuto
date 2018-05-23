create procedure [dbo].[GetProxyTypes]
  @actual int = 0
as
begin
  select Id, [Name] from proxyType order by [Name]
end
