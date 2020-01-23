--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."ConferenceParticipants"
ADD CreateUserId  int  NULL
go

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

CREATE TABLE "Hotels"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(100)  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE TABLE "AttendeeHotels"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"HotelId"            int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeHotels_EditionId_IsDeleted" ON "AttendeeHotels"
( 
	"EditionId"           ASC,
	"IsDeleted"           ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_AttendeeHotels_HotelId_IsDeleted" ON "AttendeeHotels"
( 
	"HotelId"             ASC,
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

ALTER TABLE "Hotels"
ADD CONSTRAINT "XPKHotels" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "AttendeeHotels"
ADD CONSTRAINT "PK_AttendeeHotels" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "AttendeeHotels"
ADD CONSTRAINT "IDX_UQ_AttendeeHotels_Uid" UNIQUE ("Uid"  ASC)
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

ALTER TABLE "Hotels"
	ADD CONSTRAINT "FK_Users_Hotels_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "Hotels"
	ADD CONSTRAINT "FK_Users_Hotels_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeHotels"
	ADD CONSTRAINT "FK_Editions_AttendeeHotels_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "AttendeeHotels"
	ADD CONSTRAINT "FK_Hotels_AttendeeHotels_HotelId" FOREIGN KEY ("HotelId") REFERENCES "Hotels"("Id")
go

ALTER TABLE "AttendeeHotels"
	ADD CONSTRAINT "FK_Users_AttendeeHotels_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "AttendeeHotels"
	ADD CONSTRAINT "FK_Users_AttendeeHotels_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go
