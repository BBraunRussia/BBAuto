create procedure [dbo].[Car_Select]
as
  select
    c.car_id,
    car_bbnumber,
    car_grz,
    car_vin,
    car_year,
    car_enumber,
    car_bodynumber,
    mark_id,
    g.model_id,
    c.grade_id,
    color_id,
    owner_id,
    region_id_buy,
    region_id_using,
    driver_id,
    carBuy_dateOrder,
    carBuy_isGet,
    carBuy_dateGet,
    carBuy_cost,
    carBuy_dop,
    carBuy_events,
    diller_id,
    car_lisingDate,
    car_InvertoryNumber,
    case when cs.car_id is not null then 1 else 0 end as IsSale
  from
    Car c
    join Grade g
      on g.grade_id = c.grade_id
    join Model
      on Model.model_id = g.model_id
    join CarBuy cb
      on cb.car_id = c.car_id
    left join CarSale cs
      on cs.car_id = c.car_id
  order by
    car_bbnumber
