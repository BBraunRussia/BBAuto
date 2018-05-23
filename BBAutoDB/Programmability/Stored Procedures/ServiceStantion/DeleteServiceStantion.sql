create procedure [dbo].[DeleteServiceStantion]
  @id int
as
begin
  delete from ServiceStantion
  where Id = @id
end
