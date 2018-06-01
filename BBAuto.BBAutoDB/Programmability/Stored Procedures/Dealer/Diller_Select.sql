create procedure [dbo].[Diller_Select]
as
begin
	select
    diller_id,
    diller_name as 'Название',
    diller_contacts as 'Контакты'
	from
    Diller
	order by
    diller_name
end
