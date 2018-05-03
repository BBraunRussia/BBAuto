create procedure [dbo].[GetCarDocs]
as
begin
  select
    carDoc_id,
    car_id,
    carDoc_name,
    carDoc_file
  from
    carDoc
end
