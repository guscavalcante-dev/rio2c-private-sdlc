--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Logistics"
  DROP COLUMN "AirfareOtherLogisticSponsor"
go

ALTER TABLE "dbo"."Logistics"
  DROP COLUMN "AccommodationOtherLogisticSponsor"
go

ALTER TABLE "dbo"."Logistics"
  DROP COLUMN "AirportTransferOtherLogisticSponsor"
go

ALTER TABLE "dbo"."Logistics"
ADD AdditionalInfo  varchar(1000)  NULL
go

ALTER TABLE "AttendeeLogisticSponsors"
ADD IsOther  bit  NOT NULL
go

ALTER TABLE "Places"
ADD IsAirport  bit  NOT NULL
go
