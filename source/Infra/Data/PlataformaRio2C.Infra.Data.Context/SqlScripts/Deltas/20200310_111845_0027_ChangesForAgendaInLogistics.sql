--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

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


ALTER TABLE "dbo"."AttendeeLogisticSponsors"
ADD IsLogisticListDisplayed  bit  NULL
go

UPDATE "dbo"."AttendeeLogisticSponsors"
SET IsLogisticListDisplayed = 0
go

UPDATE "dbo"."AttendeeLogisticSponsors"
SET IsLogisticListDisplayed = 1
WHERE [Uid] IN ('616A9D09-9AA7-47A8-959C-7D14AC897A3F', '4D1583A0-9FB0-47AC-B159-B77530930F0B')
go

ALTER TABLE "dbo"."AttendeeLogisticSponsors"
ALTER COLUMN IsLogisticListDisplayed  bit  NOT NULL
go
