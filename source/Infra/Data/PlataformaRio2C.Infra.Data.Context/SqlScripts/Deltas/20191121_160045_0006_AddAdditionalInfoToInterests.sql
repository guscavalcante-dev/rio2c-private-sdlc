--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Interests"
ADD HasAdditionalInfo  bit  NULL
go

UPDATE "dbo"."Interests" SET HasAdditionalInfo = 0
go
UPDATE "dbo"."Interests" SET HasAdditionalInfo = 1 WHERE Name = 'Outras Mídias | Other Media'
go

ALTER TABLE "dbo"."Interests"
ALTER COLUMN HasAdditionalInfo  bit  NOT NULL
go

ALTER TABLE "dbo"."OrganizationInterests"
ADD AdditionalInfo  varchar(200)  NULL
go
