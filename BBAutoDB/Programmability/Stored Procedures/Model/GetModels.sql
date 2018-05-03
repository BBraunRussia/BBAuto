create procedure [dbo].[GetModels]
as
begin
  select model_id, model_name, mark_id from Model
end
