create procedure [dbo].[GetProxyTypes]
  @actual int = 0
as
begin
  select proxyType_id, proxyType_name from proxyType order by proxyType_name
end
