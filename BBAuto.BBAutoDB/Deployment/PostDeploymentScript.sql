/*
Post-Deployment Script Template              
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.    
 Use SQLCMD syntax to include a file in the post-deployment script.      
 Example:      :r .\myfile.sql                
 Use SQLCMD syntax to reference a variable in the post-deployment script.    
 Example:      :setvar TableName MyTable              
               SELECT * FROM [$(TableName)]          
--------------------------------------------------------------------------------------
*/
print 'start postDeploymentScript'

update Account set account_file = replace(account_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update carDoc set carDoc_file = replace(carDoc_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update diagCard set diagCard_file = replace(diagCard_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update DriverLicense set DriverLicense_file = replace(DriverLicense_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update dtpFile set dtpFile_file = replace(dtpFile_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update History set his_file = replace(his_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update Instraction set Instraction_file = replace(Instraction_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update Invoice set Invoice_file = replace(Invoice_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update MedicalCert set MedicalCert_file = replace(MedicalCert_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update Passport set Passport_file = replace(Passport_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update Policy set Policy_file = replace(Policy_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update PTS set PTS_file = replace(PTS_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update Repair set Repair_file = replace(Repair_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update ShipPart set ShipPart_file = replace(ShipPart_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update STS set STS_file = replace(STS_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update Template set Template_path = replace(Template_path, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update Violation set Violation_file = replace(Violation_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update Violation set Violation_filePay = replace(Violation_filePay, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')

update Comp set KaskoPaymentCount = 2 where KaskoPaymentCount is null

update Status set Status_name = N'Транспондеры' where Status_id = 16

if not exists(select 1 from Status where Status_id = 16)
begin
  insert into Status(Status_name, Status_seq) values(N'Транспондеры', 16)
end

if not exists(select 1 from Documents)
begin
  insert into Documents (Name, Instruction)
  select distinct i.Instraction_number, 1 FROM Instraction i
end
if not exists(select 1 from DriverInstruction)
begin
  insert into DriverInstruction (DocumentId, DriverId, Date)
  select (select Id from Documents d where d.Name = i.Instraction_number) as DocumentId, driver_id, instraction_date from Instraction i
end
