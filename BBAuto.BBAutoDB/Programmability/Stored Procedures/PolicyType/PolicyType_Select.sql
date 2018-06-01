CREATE PROCEDURE [dbo].[PolicyType_Select]
@all int = 0
AS
BEGIN
	if (@all = 0)
		SELECT policyType_id, policyType_name 'Название'
		FROM PolicyType
	else
		SELECT 0 policyType_id, '(все)' 'Название'
		UNION
		SELECT policyType_id, policyType_name 'Название'
		FROM PolicyType
END
GO
