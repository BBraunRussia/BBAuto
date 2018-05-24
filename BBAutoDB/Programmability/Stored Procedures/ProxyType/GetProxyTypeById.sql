create procedure [dbo].[GetProxyTypeById]
  @id int
as
  select
    Id,
    [Name]
  from
    proxyType
  where
    Id = @id
