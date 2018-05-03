create procedure [dbo].[GetDiagCards]
as
begin
  select
    diagCard_id,
    car_id,
    diagCard_number,
    diagCard_date,
    diagCard_file,
    diagCard_notificationSent
  from
    diagCard
end
