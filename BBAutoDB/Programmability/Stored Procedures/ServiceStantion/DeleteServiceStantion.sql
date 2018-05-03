create procedure [dbo].[DeleteServiceStantion]
  @idServiceStantion int
as
begin
  delete from ServiceStantion
  where ServiceStantion_id = @idServiceStantion
end
