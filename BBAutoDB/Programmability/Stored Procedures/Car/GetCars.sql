create procedure [dbo].[GetCars]
as
begin
  select
    c.Id,
    c.BBNumber,
    c.Grz,
    c.Vin,
    c.[Year],
    c.Enumber,
    c.Bodynumber,
    m.MarkId,
    m.Id,
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
      on g.Id = c.gradeId
    join model m
      on m.Id = g.ModelId
  order by
    BBNumber
end
