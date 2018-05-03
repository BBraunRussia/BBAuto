create procedure [dbo].[DeleteProxyType]
  @id int
as
begin
  delete from proxyType
  where ProxyType_id = @id
end
