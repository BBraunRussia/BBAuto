create procedure [dbo].[UpsertDocument]
  @id int,
  @name nvarchar(50),
  @path nvarchar(500),
  @instruction bit
as
  if not exists(select 1 from Documents where Id = @id)
    insert into Documents (Name, Path, Instruction) values(@name, @path, @instruction)
  else
    update Documents 
    set
      Name = @name,
      Path = @path,
      Instruction = @instruction
    where
      Id = @id
