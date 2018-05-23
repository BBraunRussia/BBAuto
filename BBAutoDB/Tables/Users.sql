create table dbo.Users (
  Id int identity (1, 1) not null,
  [Login] nvarchar(50) not null, 
  RoleId int not null,
  constraint [PK_Users] primary key clustered (Id),
  constraint FK_Users_Roles foreign key (RoleId) references dbo.Role ([Id])
)
