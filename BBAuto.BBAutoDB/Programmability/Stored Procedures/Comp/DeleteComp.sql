create procedure [dbo].[DeleteComp]
  @id int
as
  delete from Comp where Comp_id = @id
