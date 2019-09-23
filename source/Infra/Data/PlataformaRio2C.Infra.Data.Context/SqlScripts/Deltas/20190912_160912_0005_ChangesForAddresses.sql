--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Addresses"
ADD CountryId  int  NULL
go

ALTER TABLE "dbo"."Addresses"
ADD StateId  int  NULL
go

ALTER TABLE "dbo"."Addresses"
ALTER COLUMN CityId  int  NULL
go

ALTER TABLE "dbo"."Addresses"
ALTER COLUMN Address1  varchar(200)  NULL
go

ALTER TABLE "dbo"."Addresses"
ALTER COLUMN ZipCode  varchar(10)  NULL
go

ALTER TABLE "dbo"."Addresses"
    ADD CONSTRAINT "FK_Countries_Addresses_CountryId" FOREIGN KEY ("CountryId") REFERENCES "dbo"."Countries"("Id")
go

ALTER TABLE "dbo"."Addresses"
    ADD CONSTRAINT "FK_States_Addresses_StateId" FOREIGN KEY ("StateId") REFERENCES "dbo"."States"("Id")
go
