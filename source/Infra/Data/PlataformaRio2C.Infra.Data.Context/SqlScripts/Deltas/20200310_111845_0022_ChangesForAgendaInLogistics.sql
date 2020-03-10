--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."LogisticAirfares"
ADD IsArrival  bit  NULL
go

UPDATE "dbo"."LogisticAirfares"
SET IsArrival = 0
go

ALTER TABLE "dbo"."LogisticAirfares"
ALTER COLUMN IsArrival  bit  NOT NULL
go