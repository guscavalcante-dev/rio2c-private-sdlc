--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Editions"
  DROP COLUMN "AudiovisualNegotiationsCreateDate"
go

ALTER TABLE "dbo"."Editions"
ADD AudiovisualNegotiationsCreateStartDate  datetimeoffset  NULL
go

ALTER TABLE "dbo"."Editions"
ADD AudiovisualNegotiationsCreateEndDate  datetimeoffset  NULL
go
