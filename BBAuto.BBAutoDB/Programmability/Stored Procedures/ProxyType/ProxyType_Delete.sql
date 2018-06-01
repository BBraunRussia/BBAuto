CREATE PROCEDURE [dbo].[ProxyType_Delete]
@id int
AS
BEGIN
	DELETE FROM ProxyType WHERE ProxyType_id=@id
END
GO
