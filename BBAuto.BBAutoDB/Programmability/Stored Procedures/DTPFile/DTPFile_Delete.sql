CREATE PROCEDURE [dbo].[DTPFile_Delete]
@idDtpFile int
AS
BEGIN
	DELETE FROM dtpFile WHERE dtpFile_id=@idDtpFile
END
GO
