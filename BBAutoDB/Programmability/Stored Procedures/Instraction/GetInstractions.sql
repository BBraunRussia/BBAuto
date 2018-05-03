create procedure [dbo].[GetInstractions]
as
begin
  select
    Instraction_id,
    Instraction_number,
    Instraction_date,
    driver_id,
    instraction_file
  from
    Instraction
end
