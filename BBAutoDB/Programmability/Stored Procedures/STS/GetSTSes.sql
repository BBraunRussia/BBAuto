create procedure [dbo].[GetSTSes]
as
begin
  select
    car_id,
    sts_number,
    sts_date,
    sts_giveOrg,
    sts_file
  from
    STS
end
