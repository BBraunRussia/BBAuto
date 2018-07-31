create procedure [dbo].[GetTransponderById]
  @id int
as
  select
    t.Id,
    t.Number,
    t.RegionId,
    t.Lost,
    t.Comment
  from
    Transponder t
  where
    t.Id = @id
