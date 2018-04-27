CREATE PROCEDURE [dbo].[GetCarByGrz]
  @grz nvarchar(50)
as
  select
    c.Id,
    c.bbnumber,
    c.grz,
    c.vin,
    c.[year],
    c.enumber,
    c.bodynumber,
    m.mark_id,
    m.model_id,
    c.gradeId,
    c.colorId,
    cb.owner_id,
    cb.region_id_buy,
    cb.region_id_using,
    cb.driver_id,
    cb.carBuy_dateOrder,
    cb.carBuy_isGet,
    cb.carBuy_dateGet,
    cb.carBuy_cost,
    cb.carBuy_dop,
    cb.carBuy_events,
    cb.dealerId,
    c.LisingDate,
    c.InvertoryNumber
  from
    Car c
    join Grade g
      on g.grade_Id = c.gradeId
    join Model m
      on m.model_id = g.model_id
    join CarBuy cb
      on cb.car_id = c.Id
  where
    grz = @grz
