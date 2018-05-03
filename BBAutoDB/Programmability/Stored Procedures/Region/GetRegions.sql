create procedure [dbo].[GetRegions]
  @all int = 0
as
begin
	if (@all = 1)
		select
      region_id,
      region_name as 'Название'
    from
      Region
		union
		select
      0,
      '(все)'
		order by
      region_name
	else
		select
      region_id,
      region_name as 'Название'
		from
      Region
		order by
      region_name
end
