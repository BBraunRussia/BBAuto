create procedure [dbo].[UpsertDTPFile]
  @id int,
  @DtpId int,
  @name nvarchar(100),
  @file nvarchar(300)
as
begin
  if (@id = 0)
  begin
    insert into dtpFile values(@DtpId, @name, @file)
    select @id = @@identity
  end
  else
    update dtpFile
    set [Name] = @name,
        [File] = @file
    where Id = @id

  select @id
end
