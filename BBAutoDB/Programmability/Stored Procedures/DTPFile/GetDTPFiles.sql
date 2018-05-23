create procedure [dbo].[GetDTPFiles]
as
begin
  select
    Id,
    DtpId,
    [Name],
    [File]
  from
    dtpFile
end
