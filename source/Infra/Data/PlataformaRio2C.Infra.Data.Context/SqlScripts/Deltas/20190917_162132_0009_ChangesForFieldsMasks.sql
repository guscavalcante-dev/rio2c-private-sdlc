--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Countries"
ADD CompanyNumberMask  varchar(50)  NULL
go


ALTER TABLE "dbo"."Countries"
ADD ZipCodeMask  varchar(50)  NULL
go


ALTER TABLE "dbo"."Countries"
ADD PhoneNumberMask  varchar(50)  NULL
go


ALTER TABLE "dbo"."Countries"
ADD MobileMask  varchar(50)  NULL
go


update countries set ZipCodeMask = '9999' where code ='US'
update countries set ZipCodeMask = '9999' where code ='AU'
update countries set ZipCodeMask = '9999' where code ='BE'
update countries set CompanyNumberMask = '99.999.999/9999-99', ZipCodeMask = '99999-999' where code ='BR'
update countries set ZipCodeMask = 'a9a 9a9' where code ='CA'
update countries set ZipCodeMask = '99999' where code ='DE'
update countries set ZipCodeMask = '9999' where code ='HU'
update countries set ZipCodeMask = '99999' where code ='IT'
update countries set ZipCodeMask = '999-9999' where code ='JP'
update countries set ZipCodeMask = '99-999' where code ='PL'
update countries set ZipCodeMask = '99999' where code ='ES'