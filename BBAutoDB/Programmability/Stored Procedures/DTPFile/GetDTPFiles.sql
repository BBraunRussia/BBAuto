create procedure [dbo].[GetDTPFiles]
as
begin
  select
    dtpFile_id,
    dtp_id,
    dtpFile_name,
    dtpFile_file
  from
    dtpFile
end
