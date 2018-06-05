create table [dbo].[Customer] (
  [Id] int not null primary key,
  [FirstName] nvarchar(50) not null,
  [LastName] nvarchar(50) not null,
  [SecondName] nvarchar(50) not null,
  [PassportNumber] nvarchar(12) not null,
  [PassportGiveOrg] nvarchar(200) not null,
  [PassportGiveDate] datetime not null,
  [Address] nvarchar(200) not null,
  [Inn] nvarchar(12) not null
)
