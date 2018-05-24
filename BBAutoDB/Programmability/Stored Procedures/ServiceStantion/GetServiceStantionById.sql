create procedure [dbo].[GetServiceStantionById]
  @id int
as
  select
    Id,
    [Name]
  from
    ServiceStantion
  where
    Id = @id
