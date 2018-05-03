create procedure [dbo].[GetTabels]
as
begin
  select
    driver_id,
    tabel_date,
    tabel_comment
  from
    Tabel
  order by
    driver_id,
    tabel_date
end
