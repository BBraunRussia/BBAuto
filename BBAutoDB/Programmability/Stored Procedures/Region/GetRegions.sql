create procedure [dbo].[GetRegions]
  @all int = 0
as
begin
	if (@all = 1)
		select
      Id,
      [Name]
    from
      Region
		union
		select
      0,
      '(все)'
		order by
      [Name]
	else
		select
      Id,
      [Name]
		from
      Region
		order by
      [Name]
end
