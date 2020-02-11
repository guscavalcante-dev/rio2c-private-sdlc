--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

CREATE TABLE "Pillars"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"Name"               varchar(600)  NOT NULL ,
	"Color"              varchar(10)  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_Pillars_Name" ON "Pillars"
( 
	"Name"                ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_Pillars_EditionId" ON "Pillars"
( 
	"EditionId"           ASC
)
go

CREATE TABLE "ConferencePillars"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"ConferenceId"       int  NOT NULL ,
	"PillarId"           int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_ConferencePillars_PillarId" ON "ConferencePillars"
( 
	"PillarId"            ASC
)
go

ALTER TABLE "Pillars"
ADD CONSTRAINT "PK_Pillars" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "Pillars"
ADD CONSTRAINT "IDX_UQ_Pillars_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ConferencePillars"
ADD CONSTRAINT "PK_ConferencePillars" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "ConferencePillars"
ADD CONSTRAINT "IDX_UQ_ConferencePillars_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ConferencePillars"
ADD CONSTRAINT "IDX_UQ_ConferencePillars_ConferenceId_PillarId" UNIQUE ("ConferenceId"  ASC,"PillarId"  ASC)
go

ALTER TABLE "Pillars"
	ADD CONSTRAINT "FK_Editions_Pillars_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "Pillars"
	ADD CONSTRAINT "FK_Users_Pillars_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "Pillars"
	ADD CONSTRAINT "FK_Users_Pillars_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferencePillars"
	ADD CONSTRAINT "FK_Conferences_ConferencePillars_ConferenceId" FOREIGN KEY ("ConferenceId") REFERENCES "dbo"."Conferences"("Id")
go

ALTER TABLE "ConferencePillars"
	ADD CONSTRAINT "FK_Pillars_ConferencePillars_PillarId" FOREIGN KEY ("PillarId") REFERENCES "Pillars"("Id")
go

ALTER TABLE "ConferencePillars"
	ADD CONSTRAINT "FK_Users_ConferencePillars_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ConferencePillars"
	ADD CONSTRAINT "FK_Users_ConferencePillars_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go
