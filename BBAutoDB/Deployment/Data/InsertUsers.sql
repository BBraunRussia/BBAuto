create procedure dbo.InsertUsers
as
  declare @adminRole int
  select @adminRole = role_id from Role where role_name = '�������������'
  insert into dbo.Users
    values ('maslparu', @adminRole)
