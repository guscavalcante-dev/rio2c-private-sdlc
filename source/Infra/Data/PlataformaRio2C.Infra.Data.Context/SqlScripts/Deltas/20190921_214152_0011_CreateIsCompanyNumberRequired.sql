--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Countries"
ADD IsCompanyNumberRequired  bit  NULL
go

UPDATE "dbo"."Countries" SET IsCompanyNumberRequired = 0
go

UPDATE "dbo"."Countries" SET IsCompanyNumberRequired = 1 WHERE Code = 'BR'
go

ALTER TABLE "dbo"."Countries"
ALTER COLUMN IsCompanyNumberRequired  bit  NOT NULL
go
