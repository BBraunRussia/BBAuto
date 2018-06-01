CREATE PROCEDURE [dbo].[WayBillRoute_Select]
AS
BEGIN		
	SELECT wayBillRoute_id, wayBillDay_id, myPoint1_id, myPoint2_id, wayBillRoute_distance
	FROM WayBillRoute
END
GO
