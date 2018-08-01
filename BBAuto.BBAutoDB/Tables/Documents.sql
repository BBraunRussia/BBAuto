create table [dbo].[Documents] (
  Id int not null identity primary key,
  [Name] nvarchar(50) not null,
  [Path] nvarchar(500) null,
  Instruction bit not null
)
