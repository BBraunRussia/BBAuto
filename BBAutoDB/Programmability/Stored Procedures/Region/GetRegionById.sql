create procedure [dbo].[GetRegionById]
  @id int
as
  select Id, [Name] from Region where Id = @id
