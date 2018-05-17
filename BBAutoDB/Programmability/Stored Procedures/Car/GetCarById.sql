create procedure [dbo].[GetCarById]
  @idCar int
as
  select
    c.Id,
    c.BBNumber,
    c.Grz,
    c.Vin,
    c.[Year],
    c.Enumber,
    c.Bodynumber,
    m.MarkId,
    m.Id as ModelId,
    c.GradeId,
    c.ColorId,
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
      on g.Id = c.GradeId
    join Model m
      on m.Id = g.ModelId
  where
    c.Id = @idCar
