create procedure [dbo].[GetPTSes]
as
begin
  select
    car_id,
    pts_number,
    pts_date,
    pts_giveOrg,
    pts_file
  from
    PTS
end
