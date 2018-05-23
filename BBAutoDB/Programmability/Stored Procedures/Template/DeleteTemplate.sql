create procedure [dbo].[DeleteTemplate]
  @id int
as
begin
  delete from Template
  where Id = @id
end
