CREATE PROCEDURE [dbo].[Grade_Insert]
@id int,
@Name varchar(50),
@ePower int,
@eVol int,
@maxLoad int,
@noLoad int,
@idEngineType int,
@idModel int
AS
BEGIN
	if (@id = 0)
	begin
		INSERT INTO Grade VALUES(@Name, @ePower, @eVol, @maxLoad, @noLoad, @idEngineType, @idModel)
		SET @id = scope_identity()
	end
	else
	begin
		UPDATE Grade 
		SET grade_name=@Name, grade_epower=@ePower, grade_evol=@eVol, grade_maxLoad=@maxLoad,
			grade_noLoad=@noLoad, engineType_id=@idEngineType
		WHERE grade_id=@id and model_id=@idModel
	end
	
	SELECT @id
END
GO
