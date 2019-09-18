--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Activities"
ADD HasAdditionalInfo  bit  NULL
go

ALTER TABLE "dbo"."OrganizationActivities"
ADD AdditionalInfo  nvarchar(200)  NULL
go

UPDATE "dbo"."Activities" SET HasAdditionalInfo = 0
go

UPDATE "dbo"."Activities" SET HasAdditionalInfo = 1 WHERE Uid = '15ee708c-b79c-4d21-a8e1-c86b92027707'
go

ALTER TABLE "dbo"."Activities"
ALTER COLUMN HasAdditionalInfo  bit  NOT NULL
go
