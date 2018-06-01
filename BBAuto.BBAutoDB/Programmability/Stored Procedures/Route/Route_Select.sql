CREATE PROCEDURE [dbo].[Route_Select]
AS
BEGIN
	SELECT route_id, mypoint1_id, mypoint2_id, route_distance FROM Route
END
GO
