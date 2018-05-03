create procedure [dbo].[GetPassports]
as
begin
  select
    passport_id,
    driver_id,
    passport_firstName,
    passport_lastName,
    passport_secondName,
    passport_number,
    passport_GiveOrg,
    passport_GiveDate,
    passport_address,
    passport_file
  from
    Passport
end
