create procedure [dbo].[UpsertServiceStantion]
  @idServiceStantion int,
  @name nvarchar(200)
as
begin
  if (@idServiceStantion = 0)
  begin
    insert into ServiceStantion values(@name)
  end
  else
  begin
    update ServiceStantion
    set ServiceStantion_name = @name
    where ServiceStantion_id = @idServiceStantion
  end
end
