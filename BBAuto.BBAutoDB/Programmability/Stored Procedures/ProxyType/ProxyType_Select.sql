CREATE PROCEDURE [dbo].[ProxyType_Select]
@actual int = 0
AS
BEGIN
	SELECT * FROM ProxyType
	ORDER BY ProxyType_name
END
GO
