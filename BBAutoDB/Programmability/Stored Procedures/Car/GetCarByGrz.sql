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
    m.MarkId,
    m.Id,
    c.gradeId,
    c.colorId,
    c.LisingDate,
    c.InvertoryNumber,
    c.OwnerId,
    c.RegionIdBuy,
    c.RegionIdUsing,
    c.DriverId,
    c.DateOrder,
    c.IsGet,
    c.DateGet,
    c.Cost,
    c.Dop,
    c.[Events],
    c.DealerId
  from
    Car c
    join Grade g
      on g.Id = c.gradeId
    join Model m
      on m.Id = g.ModelId
  where
    grz = @grz
