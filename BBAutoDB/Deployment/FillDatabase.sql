create procedure dbo.FillDatabase
as
  exec dbo.InsertRoles
      
  exec dbo.InsertStatuses
  exec dbo.InsertRegions
  exec dbo.InsertPositions
  exec dbo.InsertDepts
  exec dbo.InsertOwners
  exec dbo.InsertColors
  exec dbo.InsertComps
  exec dbo.InsertCulprits
  exec dbo.InsertDealers
  
  exec dbo.InsertDrivers

  exec dbo.InsertEngineTypes
  exec dbo.InsertMarks
  exec dbo.InsertModels
  exec dbo.InsertGrades

  exec dbo.InsertStatusAfterDtps
  exec dbo.InsertRepairTypes
  exec dbo.InsertPolicyType
  exec dbo.InsertMailTexts
  exec dbo.InsertFuelCardTypes
