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
update Violation set Violation_file = replace(Violation_file, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')
update Violation set Violation_filePay = replace(Violation_filePay, '\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\', '')