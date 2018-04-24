create procedure [dbo].[InsertModels]
as
  insert into dbo.Model values(N'Logan', (select mark_id from dbo.Mark where mark_name = N'Renault'))
  insert into dbo.Model values(N'Symbol', (select mark_id from dbo.Mark where mark_name = N'Renault'))
  insert into dbo.Model values(N'Fluence', (select mark_id from dbo.Mark where mark_name = N'Renault'))
  insert into dbo.Model values(N'Megane', (select mark_id from dbo.Mark where mark_name = N'Renault'))
  insert into dbo.Model values(N'Lagrus', (select mark_id from dbo.Mark where mark_name = N'Lada'))
  insert into dbo.Model values(N'Sandero', (select mark_id from dbo.Mark where mark_name = N'Renault'))
  insert into dbo.Model values(N'Duster', (select mark_id from dbo.Mark where mark_name = N'Renault'))
  insert into dbo.Model values(N'Teana', (select mark_id from dbo.Mark where mark_name = N'Nissan'))
  insert into dbo.Model values(N'Kangoo', (select mark_id from dbo.Mark where mark_name = N'Renault'))
  insert into dbo.Model values(N'Master', (select mark_id from dbo.Mark where mark_name = N'Renault'))
  insert into dbo.Model values(N'Rav4', (select mark_id from dbo.Mark where mark_name = N'Toyota'))
  insert into dbo.Model values(N'Corolla', (select mark_id from dbo.Mark where mark_name = N'Toyota'))
  insert into dbo.Model values(N'Verso', (select mark_id from dbo.Mark where mark_name = N'Toyota'))
  insert into dbo.Model values(N'Golf', (select mark_id from dbo.Mark where mark_name = N'Volkswagen'))
  insert into dbo.Model values(N'Passat', (select mark_id from dbo.Mark where mark_name = N'Volkswagen'))
  insert into dbo.Model values(N'Camry', (select mark_id from dbo.Mark where mark_name = N'Toyota'))
  insert into dbo.Model values(N'X-TRAIL', (select mark_id from dbo.Mark where mark_name = N'Nissan'))
  insert into dbo.Model values(N'Focus', (select mark_id from dbo.Mark where mark_name = N'Ford'))
  insert into dbo.Model values(N'Sportage', (select mark_id from dbo.Mark where mark_name = N'Kia'))
  insert into dbo.Model values(N'Optima', (select mark_id from dbo.Mark where mark_name = N'Kia'))
  insert into dbo.Model values(N'Kaptur', (select mark_id from dbo.Mark where mark_name = N'Renault'))
  insert into dbo.Model values(N'Outback', (select mark_id from dbo.Mark where mark_name = N'Subaru'))
  insert into dbo.Model values(N'A6', (select mark_id from dbo.Mark where mark_name = N'Audi'))
  insert into dbo.Model values(N'X4 ', (select mark_id from dbo.Mark where mark_name = N'BMW'))
  insert into dbo.Model values(N'XC60', (select mark_id from dbo.Mark where mark_name = N'Volvo'))
  insert into dbo.Model values(N'NX 200T', (select mark_id from dbo.Mark where mark_name = N'Lexus'))
  insert into dbo.Model values(N'Vesta', (select mark_id from dbo.Mark where mark_name = N'Lada'))
  insert into dbo.Model values(N'Land Cruiser 150', (select mark_id from dbo.Mark where mark_name = N'Toyota'))
  insert into dbo.Model values(N'Х1', (select mark_id from dbo.Mark where mark_name = N'BMW'))
  insert into dbo.Model values(N'520', (select mark_id from dbo.Mark where mark_name = N'BMW'))
  insert into dbo.Model values(N'GLC ', (select mark_id from dbo.Mark where mark_name = N'Mercedes-Benz '))
  insert into dbo.Model values(N'Dokker', (select mark_id from dbo.Mark where mark_name = N'Renault'))
  insert into dbo.Model values(N'Х3', (select mark_id from dbo.Mark where mark_name = N'BMW'))
  insert into dbo.Model values(N'Q5', (select mark_id from dbo.Mark where mark_name = N'Audi'))
