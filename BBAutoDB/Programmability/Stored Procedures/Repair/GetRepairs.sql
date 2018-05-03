create procedure [dbo].[GetRepairs]
as
begin
  select
    repair_id,
    car_id,
    repairType_id,
    ServiceStantion_id,
    repair_date,
    repair_cost,
    repair_file
  from
    Repair
end
