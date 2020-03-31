--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Logistics"
DROP CONSTRAINT "FK_Users_Logistics_CreateUserId"
go

ALTER TABLE "dbo"."Logistics"
DROP CONSTRAINT "FK_Users_Logistics_UpdateUserId"
go

ALTER TABLE "dbo"."Logistics"
DROP CONSTRAINT "FK_AttendeeCollaborators_Logistics_AttendeeCollaboratorId"
go

ALTER TABLE "dbo".Logistics
DROP CONSTRAINT "PK_Logistics"
go

ALTER TABLE "dbo"."Logistics"
DROP CONSTRAINT "IDX_UQ_Logistics_Uid"
go

DROP TABLE "dbo"."Logistics"
go

CREATE TABLE "dbo"."Logistics"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"AttendeeCollaboratorId" int  NOT NULL ,
	"IsAirfareSponsored" bit  NOT NULL ,
	"AirfareAttendeeLogisticSponsorId" int  NULL ,
	"AirfareOtherLogisticSponsor" varchar(100)  NULL ,
	"IsAccommodationSponsored" bit  NOT NULL ,
	"AccommodationAttendeeLogisticSponsorId" int  NULL ,
	"AccommodationOtherLogisticSponsor" varchar(100)  NULL ,
	"IsAirportTransferSponsored" bit  NOT NULL ,
	"AirportTransferAttendeeLogisticSponsorId" int  NULL ,
	"AirportTransferOtherLogisticSponsor" varchar(100)  NULL ,
	"IsCityTransferRequired" bit  NOT NULL ,
	"IsVehicleDisposalRequired" bit  NOT NULL ,
	"AdditionalInfo"	 varchar(300) NULL,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_Logistics_AttendeeCollaboratorId" ON "dbo"."Logistics"
( 
	"AttendeeCollaboratorId"  ASC
)
go

CREATE TABLE "LogisticSponsors"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(100)  NOT NULL ,
	"IsAirfareTicketRequired" bit  NOT NULL ,
	"IsOtherRequired"    bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE TABLE "AttendeeLogisticSponsors"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"LogisticSponsorId"  int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeLogisticSponsors_EditionId_IsDeleted" ON "AttendeeLogisticSponsors"
( 
	"EditionId"           ASC,
	"IsDeleted"           ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeLogisticSponsors_LogisticSponsorId_IsDeleted" ON "AttendeeLogisticSponsors"
( 
	"LogisticSponsorId"   ASC,
	"IsDeleted"           ASC
)
go

CREATE TABLE "Places"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(100)  NOT NULL ,
	"IsHotel"            bit  NOT NULL ,
	"AddressId"          int  NULL ,
	"Website"            varchar(300)  NULL ,
	"AdditionalInfo"     varchar(1000)  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE TABLE "AttendeePlaces"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"PlaceId"            int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeePlaces_EditionId_IsDeleted" ON "AttendeePlaces"
( 
	"EditionId"           ASC,
	"IsDeleted"           ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeePlaces_PlaceId_IsDeleted" ON "AttendeePlaces"
( 
	"PlaceId"             ASC,
	"IsDeleted"           ASC
)
go

CREATE TABLE "LogisticAirfares"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"LogisticId"         int  NOT NULL ,
	"IsNational"         bit  NOT NULL ,
	"From"               varchar(100)  NOT NULL ,
	"DepartureDate"      datetimeoffset  NOT NULL ,
	"To"                 varchar(100)  NOT NULL ,
	"ArrivalDate"        datetimeoffset  NOT NULL ,
	"TicketNumber"       varchar(20)  NULL ,
	"TicketUploadDate"   datetimeoffset  NULL ,
	"AdditionalInfo"       varchar(1000)  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_LogisticAirfares_LogisticId" ON "LogisticAirfares"
( 
	"LogisticId"          ASC
)
go

CREATE TABLE "LogisticAccommodations"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"LogisticId"         int  NOT NULL ,
	"AttendeePlaceId"    int  NOT NULL ,
	"CheckInDate"        datetimeoffset  NOT NULL ,
	"CheckOutDate"       datetimeoffset  NOT NULL ,
	"AdditionalInfo"     varchar(1000)  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_LogisticAccommodations_LogisticId" ON "LogisticAccommodations"
( 
	"LogisticId"          ASC
)
go

CREATE TABLE "LogisticTransfers"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"LogisticId"         int  NOT NULL ,
	"FromAttendeePlaceId" int  NOT NULL ,
	"ToAttendeePlaceId"  int  NOT NULL ,
	"Date"               datetimeoffset  NOT NULL ,
	"AdditionalInfo"     varchar(1000)  NULL ,
	"LogisticTransferStatusId" int  NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_LogisticTransfers_LogisticId" ON "LogisticTransfers"
( 
	"LogisticId"          ASC
)
go

CREATE TABLE "LogisticTransferStatuses"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_LogisticTransferStatuses_Name" ON "LogisticTransferStatuses"
( 
	"Name"                ASC
)
go

ALTER TABLE "dbo"."Logistics"
ADD CONSTRAINT "PK_Logistics" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "dbo"."Logistics"
ADD CONSTRAINT "IDX_UQ_Logistics_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "LogisticSponsors"
ADD CONSTRAINT "PK_LogisticSponsors" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "LogisticSponsors"
ADD CONSTRAINT "IDX_UQ_LogisticSponsors_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeLogisticSponsors"
ADD CONSTRAINT "PK_AttendeeLogisticSponsors_Uid" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "Places"
ADD CONSTRAINT "PK_Places" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "Places"
ADD CONSTRAINT "IDX_UQ_Places_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeePlaces"
ADD CONSTRAINT "PK_AttendeePlaces" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "AttendeePlaces"
ADD CONSTRAINT "IDX_UQ_AttendeePlaces" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "LogisticAirfares"
ADD CONSTRAINT "PK_LogisticAirfares" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "LogisticAirfares"
ADD CONSTRAINT "IDX_UQ_LogisticAirfares_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "LogisticAccommodations"
ADD CONSTRAINT "PK_LogisticAccommodations" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "LogisticAccommodations"
ADD CONSTRAINT "IDX_UQ_LogisticAccommodations_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "LogisticTransfers"
ADD CONSTRAINT "PK_LogisticTransfers" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "LogisticTransfers"
ADD CONSTRAINT "IDX_UQ_LogisticTransfers_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "LogisticTransferStatuses"
ADD CONSTRAINT "PK_LogisticTransferStatuses" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "LogisticTransferStatuses"
ADD CONSTRAINT "IDX_UQ_LogisticTransferStatuses_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "dbo"."Logistics"
	ADD CONSTRAINT "FK_Users_Logistics_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "dbo"."Logistics"
	ADD CONSTRAINT "FK_Users_Logistics_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "dbo"."Logistics"
	ADD CONSTRAINT "FK_AttendeeCollaborators_Logistics_AttendeeCollaboratorId" FOREIGN KEY ("AttendeeCollaboratorId") REFERENCES "dbo"."AttendeeCollaborators"("Id")
go

ALTER TABLE "dbo"."Logistics"
	ADD CONSTRAINT "FK_AttendeeLogisticSponsors_Logistics_AirfareAttendeeLogisticSponsorId" FOREIGN KEY ("AirfareAttendeeLogisticSponsorId") REFERENCES "AttendeeLogisticSponsors"("Id")
go

ALTER TABLE "dbo"."Logistics"
	ADD CONSTRAINT "FK_AttendeeLogisticSponsors_Logistics_AccommodationAttendeeLogisticSponsorId" FOREIGN KEY ("AccommodationAttendeeLogisticSponsorId") REFERENCES "AttendeeLogisticSponsors"("Id")
go

ALTER TABLE "dbo"."Logistics"
	ADD CONSTRAINT "FK_AttendeeLogisticSponsors_Logistics_AirportTransferAttendeeLogisticSponsorId" FOREIGN KEY ("AirportTransferAttendeeLogisticSponsorId") REFERENCES "AttendeeLogisticSponsors"("Id")
go

ALTER TABLE "LogisticSponsors"
	ADD CONSTRAINT "FK_Users_LogisticSponsors_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "LogisticSponsors"
	ADD CONSTRAINT "FK_Users_LogisticSponsors_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeLogisticSponsors"
	ADD CONSTRAINT "FK_Editions_AttendeeLogisticSponsors_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "AttendeeLogisticSponsors"
	ADD CONSTRAINT "FK_LogisticSponsors_AttendeeLogisticSponsors_LogisticSponsorId" FOREIGN KEY ("LogisticSponsorId") REFERENCES "LogisticSponsors"("Id")
go

ALTER TABLE "AttendeeLogisticSponsors"
	ADD CONSTRAINT "FK_Users_AttendeeLogisticSponsors_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeLogisticSponsors"
	ADD CONSTRAINT "FK_Users_AttendeeLogisticSponsors_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "Places"
	ADD CONSTRAINT "FK_Addresses_Places_AddressId" FOREIGN KEY ("AddressId") REFERENCES "dbo"."Addresses"("Id")
go

ALTER TABLE "Places"
	ADD CONSTRAINT "FK_Users_Places_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "Places"
	ADD CONSTRAINT "FK_Users_Places_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeePlaces"
	ADD CONSTRAINT "FK_Editions_AttendeePlaces_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "AttendeePlaces"
	ADD CONSTRAINT "FK_Places_AttendeePlaces_PlaceId" FOREIGN KEY ("PlaceId") REFERENCES "Places"("Id")
go

ALTER TABLE "AttendeePlaces"
	ADD CONSTRAINT "FK_Users_AttendeePlaces_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeePlaces"
	ADD CONSTRAINT "FK_Users_AttendeePlaces_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "LogisticAirfares"
	ADD CONSTRAINT "FK_Logistics_LogisticAirfares_LogisticId" FOREIGN KEY ("LogisticId") REFERENCES "dbo"."Logistics"("Id")
go

ALTER TABLE "LogisticAirfares"
	ADD CONSTRAINT "FK_Users_LogisticAirfares_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "LogisticAirfares"
	ADD CONSTRAINT "FK_Users_LogisticAirfares_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "LogisticAccommodations"
	ADD CONSTRAINT "FK_Logistics_LogisticAccommodations_LogisticId" FOREIGN KEY ("LogisticId") REFERENCES "dbo"."Logistics"("Id")
go

ALTER TABLE "LogisticAccommodations"
	ADD CONSTRAINT "FK_AttendeePlaces_LogisticAccommodations_AttendeePlaceId" FOREIGN KEY ("AttendeePlaceId") REFERENCES "AttendeePlaces"("Id")
go

ALTER TABLE "LogisticAccommodations"
	ADD CONSTRAINT "FK_Users_LogisticAccommodations_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "LogisticAccommodations"
	ADD CONSTRAINT "FK_Users_LogisticAccommodations_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "LogisticTransfers"
	ADD CONSTRAINT "FK_Logistics_LogisticTransfers_LogisticId" FOREIGN KEY ("LogisticId") REFERENCES "dbo"."Logistics"("Id")
go

ALTER TABLE "LogisticTransfers"
	ADD CONSTRAINT "FK_AttendeePlaces_LogisticTransfers_FromAttendeePlaceId" FOREIGN KEY ("FromAttendeePlaceId") REFERENCES "AttendeePlaces"("Id")
go

ALTER TABLE "LogisticTransfers"
	ADD CONSTRAINT "FK_AttendeePlaces_LogisticTransfers_ToAttendeePlaceId" FOREIGN KEY ("ToAttendeePlaceId") REFERENCES "AttendeePlaces"("Id")
go

ALTER TABLE "LogisticTransfers"
	ADD CONSTRAINT "FK_LogisticTransferStatuses_LogisticTransfers_LogisticTransferStatusId" FOREIGN KEY ("LogisticTransferStatusId") REFERENCES "LogisticTransferStatuses"("Id")
go

ALTER TABLE "LogisticTransfers"
	ADD CONSTRAINT "FK_Users_LogisticTransfers_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "LogisticTransfers"
	ADD CONSTRAINT "FK_Users_LogisticTransfers_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "LogisticTransferStatuses"
	ADD CONSTRAINT "FK_Users_LogisticTransferStatuses_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "LogisticTransferStatuses"
	ADD CONSTRAINT "FK_Users_LogisticTransferStatuses_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go
