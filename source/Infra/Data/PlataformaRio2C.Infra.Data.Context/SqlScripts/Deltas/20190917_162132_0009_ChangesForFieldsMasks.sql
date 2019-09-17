--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Countries"
ADD ZipCodeMask  varchar(50)  NULL
go


ALTER TABLE "dbo"."Countries"
ADD PhoneNumberMask  varchar(50)  NULL
go


ALTER TABLE "dbo"."Countries"
ADD MobileMask  varchar(50)  NULL
go
