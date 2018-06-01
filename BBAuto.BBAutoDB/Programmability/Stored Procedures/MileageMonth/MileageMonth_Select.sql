CREATE PROCEDURE [dbo].[MileageMonth_Select]
@car int, 
@date datetime
AS
BEGIN
	select MileageMonth_count, psn_count, psk_count, gas_count, gas_begin, gas_end, gas_norm from MileageMonth
	where car_id = @car and MileageMonth_date = @date	
END
GO
