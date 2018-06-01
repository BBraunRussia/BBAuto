create procedure [dbo].[Owner_Select]
  @all int = 0
as
begin
	if (@all = 1)
	begin
		select
      owner_id,
      owner_name as 'Название'
    from
      Owner
		union
		select
      0,
      '(все)'	
		order by
      owner_name
	end
	else
	begin
		select
      owner_id,
      owner_name as 'Название'
    from
      Owner
	end
end
