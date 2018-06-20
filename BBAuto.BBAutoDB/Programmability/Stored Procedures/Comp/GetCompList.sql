create procedure [dbo].[GetCompList]
as
  select
    comp_id as Id,
    comp_name as Name,
    KaskoPaymentCount
  from
    Comp
  order by
    Comp_name
