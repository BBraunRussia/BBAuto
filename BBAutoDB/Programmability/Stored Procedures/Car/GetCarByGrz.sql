create procedure [dbo].[GetCarByGrz]
  @grz nvarchar(50)
as
  select
    c.id,
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
    cb.OwnerId,
    cb.RegionIdBuy,
    cb.RegionIdUsing,
    cb.DriverId,
    cb.DateOrder,
    cb.IsGet,
    cb.DateGet,
    cb.Cost,
    cb.Dop,
    cb.[Events],
    cb.DealerId,
    c.LisingDate,
    c.InvertoryNumber
  from
    Car c
    join Grade g
      on g.grade_Id = c.gradeId
    join model m
      on m.model_id = g.model_id
    join CarBuy cb
      on cb.CarId = c.id
  where
    grz = @grz
