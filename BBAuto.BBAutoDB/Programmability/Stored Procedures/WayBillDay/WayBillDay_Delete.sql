CREATE PROCEDURE [dbo].[WayBillDay_Delete]
@id int
AS
BEGIN
	DELETE FROM WayBillDay WHERE wayBillDay_id=@id
END
GO
