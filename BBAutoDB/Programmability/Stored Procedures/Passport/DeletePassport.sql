create procedure [dbo].[DeletePassport]
  @idPassport int
as
begin
  delete from Passport
  where passport_id = @idPassport
end
