CREATE PROCEDURE [dbo].[Grade_Select]
AS
BEGIN
	SELECT grade_id, grade_name, grade_epower, grade_evol, grade_maxload, grade_noload,
		engineType_id, model_id
	FROM Grade
END
GO
