create procedure [dbo].[GetProxyTypes]
as
  select
    Id,
    [Name]
  from
    proxyType
  order by
    [Name]
