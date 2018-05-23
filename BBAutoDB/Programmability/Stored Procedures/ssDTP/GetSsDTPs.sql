create procedure [dbo].[GetSsDTPs]
as
begin
  select MarkId, ServiceStantionId from ssDTP
end
