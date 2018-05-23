create procedure [dbo].[DeleteProxyType]
  @id int
as
begin
  delete from proxyType
  where Id = @id
end
