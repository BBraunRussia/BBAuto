create procedure [dbo].[DeletePassport]
  @id int
as
begin
  delete from Passport
  where id = @id
end
