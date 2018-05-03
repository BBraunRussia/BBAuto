create procedure [dbo].[GetSsDTPs]
as
begin
  select mark_id, serviceStantion_id from ssDTP
end
