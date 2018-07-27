create procedure [dbo].[GetTransponderById]
  @id int
as
  select
    t.Id,
    t.Number,
    t.RegionId,
    t.Comment
  from
    Transponder t
