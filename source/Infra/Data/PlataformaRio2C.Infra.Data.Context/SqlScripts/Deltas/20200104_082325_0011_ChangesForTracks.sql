--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

CREATE TABLE "HorizontalTracks"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(500)  NOT NULL ,
	"DisplayOrder"       int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_HorizontalTracks_Name" ON "HorizontalTracks"
( 
	"Name"                ASC
)
go

CREATE TABLE "VerticalTracks"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(500)  NOT NULL ,
	"DisplayOrder"       int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_VerticalTracks_Name" ON "VerticalTracks"
( 
	"Name"                ASC
)
go

CREATE TABLE "ConferenceVerticalTracks"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"ConferenceId"       int  NOT NULL ,
	"VerticalTrackId"    int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferenceVerticalTracks_ConferenceId" ON "ConferenceVerticalTracks"
( 
	"ConferenceId"        ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferenceVerticalTracks_VerticalTrackId" ON "ConferenceVerticalTracks"
( 
	"VerticalTrackId"     ASC
)
go

CREATE TABLE "ConferenceHorizontalTracks"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"ConferenceId"       int  NOT NULL ,
	"HorizontalTrackId"  int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferenceHorizontalTracks_ConferenceId" ON "ConferenceHorizontalTracks"
( 
	"ConferenceId"        ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferenceHorizontalTracks_HorizontalTrackId" ON "ConferenceHorizontalTracks"
( 
	"HorizontalTrackId"   ASC
)
go

ALTER TABLE "HorizontalTracks"
ADD CONSTRAINT "PK_HorizontalTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "HorizontalTracks"
ADD CONSTRAINT "IDX_UQ_HorizontalTracks_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "VerticalTracks"
ADD CONSTRAINT "PK_VerticalTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "VerticalTracks"
ADD CONSTRAINT "IDX_UQ_VerticalTracks_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ConferenceVerticalTracks"
ADD CONSTRAINT "PK_ConferenceVerticalTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "ConferenceVerticalTracks"
ADD CONSTRAINT "IDX_UQ_ConferenceVerticalTracks_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ConferenceHorizontalTracks"
ADD CONSTRAINT "PK_ConferenceHorizontalTracks" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "ConferenceHorizontalTracks"
ADD CONSTRAINT "IDX_UQ_ConferenceHorizontalTracks_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "HorizontalTracks"
	ADD CONSTRAINT "FK_Users_HorizontalTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "HorizontalTracks"
	ADD CONSTRAINT "FK_Users_HorizontalTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "VerticalTracks"
	ADD CONSTRAINT "FK_Users_VerticalTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "VerticalTracks"
	ADD CONSTRAINT "FK_Users_VerticalTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferenceVerticalTracks"
	ADD CONSTRAINT "FK_Conferences_ConferenceVerticalTracks_ConferenceId" FOREIGN KEY ("ConferenceId") REFERENCES "dbo"."Conferences"("Id")
go

ALTER TABLE "ConferenceVerticalTracks"
	ADD CONSTRAINT "FK_VerticalTracks_ConferenceVerticalTracks_VerticalTrackId" FOREIGN KEY ("VerticalTrackId") REFERENCES "VerticalTracks"("Id")
go

ALTER TABLE "ConferenceVerticalTracks"
	ADD CONSTRAINT "FK_Users_ConferenceVerticalTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferenceVerticalTracks"
	ADD CONSTRAINT "FK_Users_ConferenceVerticalTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferenceHorizontalTracks"
	ADD CONSTRAINT "FK_Conferences_ConferenceHorizontalTracks_ConferenceId" FOREIGN KEY ("ConferenceId") REFERENCES "dbo"."Conferences"("Id")
go

ALTER TABLE "ConferenceHorizontalTracks"
	ADD CONSTRAINT "FK_HorizontalTracks_ConferenceHorizontalTracks_HorizontalTrackId" FOREIGN KEY ("HorizontalTrackId") REFERENCES "HorizontalTracks"("Id")
go

ALTER TABLE "ConferenceHorizontalTracks"
	ADD CONSTRAINT "FK_Users_ConferenceHorizontalTracks_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferenceHorizontalTracks"
	ADD CONSTRAINT "FK_Users_ConferenceHorizontalTracks_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go
