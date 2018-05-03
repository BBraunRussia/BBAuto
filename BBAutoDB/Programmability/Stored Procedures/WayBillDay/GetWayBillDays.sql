create procedure [dbo].[GetWayBillDays]
as
begin
  select
    wayBillDay_id,
    car_id,
    driver_id,
    wayBillDay_date
  from
    WayBillDay
end
