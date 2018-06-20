create procedure [dbo].[GetCompById]
  @id int
as
  select
    comp_id as id,
    comp_name as Name,
    KaskoPaymentCount
  from
    Comp
  where
    comp_id = @id
