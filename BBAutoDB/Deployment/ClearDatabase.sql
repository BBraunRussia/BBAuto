create procedure dbo.ClearDatabase
as
  delete from dbo.Account
  if ident_current('dbo.Account') > 1
    dbcc checkident ('dbo.Account', reseed, 0);
  
  delete from dbo.SaleCar
  
  delete from dbo.CarDoc
  if ident_current('dbo.CarDoc') > 1
    dbcc checkident ('dbo.CarDoc', reseed, 0);

  delete from dbo.Car
  if ident_current('dbo.Car') > 1
    dbcc checkident ('dbo.Car', reseed, 0);

  delete from dbo.PolicyType
  if ident_current('dbo.PolicyType') > 1
    dbcc checkident ('dbo.PolicyType', reseed, 0);

  delete from dbo.Owner
  if ident_current('dbo.Owner') > 1
    dbcc checkident ('dbo.Owner', reseed, 0);

  delete from dbo.Region
  if ident_current('dbo.Region') > 1
    dbcc checkident ('dbo.Region', reseed, 0);  
  
  delete from dbo.Color
  if ident_current('dbo.Color') > 1
    dbcc checkident ('dbo.Color', reseed, 0);

  delete from dbo.ColumnSize
  
  delete from dbo.Comp
  if ident_current('dbo.Comp') > 1
    dbcc checkident ('dbo.Comp', reseed, 0);

  delete from dbo.Culprit
  if ident_current('dbo.Culprit') > 1
    dbcc checkident ('dbo.Culprit', reseed, 0);

  delete from dbo.CurrentStatusAfterDTP
  if ident_current('dbo.CurrentStatusAfterDTP') > 1
    dbcc checkident ('dbo.CurrentStatusAfterDTP', reseed, 0);

  delete from dbo.Dealer
  if ident_current('dbo.Dealer') > 1
    dbcc checkident ('dbo.Dealer', reseed, 0);

  delete from dbo.Dept
  if ident_current('dbo.Dept') > 1
    dbcc checkident ('dbo.Dept', reseed, 0);

  delete from dbo.DiagCard
  if ident_current('dbo.DiagCard') > 1
    dbcc checkident ('dbo.DiagCard', reseed, 0);

  delete from dbo.Driver
  if ident_current('dbo.Driver') > 1
    dbcc checkident ('dbo.Driver', reseed, 0);

  delete from dbo.DriverLicense
  if ident_current('dbo.DriverLicense') > 1
    dbcc checkident ('dbo.DriverLicense', reseed, 0);

  delete from dbo.DTP
  if ident_current('dbo.DTP') > 1
    dbcc checkident ('dbo.DTP', reseed, 0);

  delete from dbo.DtpFile
  if ident_current('dbo.DtpFile') > 1
    dbcc checkident ('dbo.DtpFile', reseed, 0);

  delete from dbo.Employees
  
  delete from dbo.EmployeesName
  if ident_current('dbo.EmployeesName') > 1
    dbcc checkident ('dbo.EmployeesName', reseed, 0);

  delete from dbo.Grade
  if ident_current('dbo.Grade') > 1
    dbcc checkident ('dbo.Grade', reseed, 0);

  delete from dbo.Model
  if ident_current('dbo.Model') > 1
    dbcc checkident ('dbo.Model', reseed, 0);

  delete from dbo.Mark
  if ident_current('dbo.Mark') > 1
    dbcc checkident ('dbo.Mark', reseed, 0);

  delete from dbo.EngineType
  if ident_current('dbo.EngineType') > 1
    dbcc checkident ('dbo.EngineType', reseed, 0);

  delete from dbo.Fuel
  if ident_current('dbo.Fuel') > 1
    dbcc checkident ('dbo.Fuel', reseed, 0);

  delete from dbo.FuelCard
  if ident_current('dbo.FuelCard') > 1
    dbcc checkident ('dbo.FuelCard', reseed, 0);

  delete from dbo.FuelCardDriver
  if ident_current('dbo.FuelCardDriver') > 1
    dbcc checkident ('dbo.FuelCardDriver', reseed, 0);

  delete from dbo.FuelCardType
  if ident_current('dbo.FuelCardType') > 1
    dbcc checkident ('dbo.FuelCardType', reseed, 0);

  delete from dbo.History
  if ident_current('dbo.History') > 1
    dbcc checkident ('dbo.History', reseed, 0);

  delete from dbo.Instraction
  if ident_current('dbo.Instraction') > 1
    dbcc checkident ('dbo.Instraction', reseed, 0);

  delete from dbo.Invoice
  if ident_current('dbo.Invoice') > 1
    dbcc checkident ('dbo.Invoice', reseed, 0);

  delete from dbo.MailText
  if ident_current('dbo.MailText') > 1
    dbcc checkident ('dbo.MailText', reseed, 0);

  delete from dbo.MainPoint
  
  delete from dbo.MedicalCert
  if ident_current('dbo.MedicalCert') > 1
    dbcc checkident ('dbo.MedicalCert', reseed, 0);

  delete from dbo.Mileage
  if ident_current('dbo.Mileage') > 1
    dbcc checkident ('dbo.Mileage', reseed, 0);

  delete from dbo.MileageMonth
  if ident_current('dbo.MileageMonth') > 1
    dbcc checkident ('dbo.MileageMonth', reseed, 0);
    
  delete from dbo.MyPoint
  if ident_current('dbo.MyPoint') > 1
    dbcc checkident ('dbo.MyPoint', reseed, 0);

  delete from dbo.Owner
  if ident_current('dbo.Owner') > 1
    dbcc checkident ('dbo.Owner', reseed, 0);

  delete from dbo.Passport
  if ident_current('dbo.Passport') > 1
    dbcc checkident ('dbo.Passport', reseed, 0);

  delete from dbo.Policy
  if ident_current('dbo.Policy') > 1
    dbcc checkident ('dbo.Policy', reseed, 0);

  delete from dbo.Position
  if ident_current('dbo.Position') > 1
    dbcc checkident ('dbo.Position', reseed, 0);

  delete from dbo.Proxy
  if ident_current('dbo.Proxy') > 1
    dbcc checkident ('dbo.Proxy', reseed, 0);

  delete from dbo.ProxyType
  if ident_current('dbo.ProxyType') > 1
    dbcc checkident ('dbo.ProxyType', reseed, 0);

  delete from dbo.PTS
  
  delete from dbo.Repair
  if ident_current('dbo.Repair') > 1
    dbcc checkident ('dbo.Repair', reseed, 0);

  delete from dbo.RepairType
  if ident_current('dbo.RepairType') > 1
    dbcc checkident ('dbo.RepairType', reseed, 0);
    
  delete from dbo.Route
  if ident_current('dbo.Route') > 1
    dbcc checkident ('dbo.Route', reseed, 0);

  delete from dbo.ServiceStantion
  if ident_current('dbo.ServiceStantion') > 1
    dbcc checkident ('dbo.ServiceStantion', reseed, 0);

  delete from dbo.ShipPart
  if ident_current('dbo.ShipPart') > 1
    dbcc checkident ('dbo.ShipPart', reseed, 0);

  delete from dbo.ssDTP
  
  delete from dbo.Status
  if ident_current('dbo.Status') > 1
    dbcc checkident ('dbo.Status', reseed, 0);

  delete from dbo.StatusAfterDTP
  if ident_current('dbo.StatusAfterDTP') > 1
    dbcc checkident ('dbo.StatusAfterDTP', reseed, 0);

  delete from dbo.STS
  delete from dbo.SuppyAddress
  delete from dbo.Tabel
  
  delete from dbo.Template
  if ident_current('dbo.Template') > 1
    dbcc checkident ('dbo.Template', reseed, 0);

  delete from dbo.TempMove
  if ident_current('dbo.TempMove') > 1
    dbcc checkident ('dbo.TempMove', reseed, 0);

  delete from dbo.UserAccess
  
  delete from dbo.Violation
  if ident_current('dbo.Violation') > 1
    dbcc checkident ('dbo.Violation', reseed, 0);

  delete from dbo.ViolationType
  if ident_current('dbo.ViolationType') > 1
    dbcc checkident ('dbo.ViolationType', reseed, 0);

  delete from dbo.WayBillDay
  if ident_current('dbo.WayBillDay') > 1
    dbcc checkident ('dbo.WayBillDay', reseed, 0);

  delete from dbo.WayBillRoute
  if ident_current('dbo.WayBillRoute') > 1
    dbcc checkident ('dbo.WayBillRoute', reseed, 0);

  delete from dbo.Users
  if ident_current('dbo.Users') > 1
    dbcc checkident ('dbo.Users', reseed, 0);

  delete from dbo.Role
  if ident_current('dbo.Role') > 1
    dbcc checkident ('dbo.Role', reseed, 0);
go
