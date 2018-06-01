CREATE PROCEDURE [dbo].[DTPFile_Select]
AS
BEGIN
	SELECT dtpFile_id, dtp_id, dtpFile_name, dtpFile_File FROM dtpFile
END
GO
