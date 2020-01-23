--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

CREATE TABLE "LogisticSponsors"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(100)  NOT NULL ,
	"IsAirfareTicketRequired" bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
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
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
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
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
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
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
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

ALTER TABLE "LogisticSponsors"
ADD CONSTRAINT "PK_LogisticSponsors" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "LogisticSponsors"
ADD CONSTRAINT "IDX_UQ_LogisticSponsors_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "AttendeeLogisticSponsors"
ADD CONSTRAINT "PK_AttendeeLogisticSponsors_Uid" PRIMARY KEY  CLUSTERED ("Id" ASC,"Uid" ASC)
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
