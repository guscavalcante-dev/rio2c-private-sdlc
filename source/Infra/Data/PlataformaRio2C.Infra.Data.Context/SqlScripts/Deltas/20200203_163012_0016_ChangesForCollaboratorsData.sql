--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Collaborators"
ADD BirthDate  datetime  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorGenderId  int  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorGenderAdditionalInfo  varchar(300)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorRoleId  int  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorRoleAdditionalInfo  varchar(300)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorIndustryId  int  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD CollaboratorIndustryAdditionalInfo  varchar(300)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD HasAnySpecialNeeds  bit  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD SpecialNeedsDescription  varchar(300)  NULL
go

CREATE TABLE "CollaboratorRoles"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"HasAdditionalInfo"  bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_CollaboratorRoles_Name" ON "CollaboratorRoles"
( 
	"Name"                ASC
)
go

CREATE TABLE "CollaboratorIndustries"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"HasAdditionalInfo"  bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_CollaboratorIndustries_Name" ON "CollaboratorIndustries"
( 
	"Name"                ASC
)
go

CREATE TABLE "CollaboratorGenders"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(300)  NOT NULL ,
	"HasAdditionalInfo"  bit  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_CollaboratorGenders_Name" ON "CollaboratorGenders"
( 
	"Name"                ASC
)
go

CREATE TABLE "CollaboratorEditionParticipations"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"CollaboratorId"     int  NOT NULL ,
	"EditionId"          int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

ALTER TABLE "CollaboratorRoles"
ADD CONSTRAINT "PK_CollaboratorRoles" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "CollaboratorRoles"
ADD CONSTRAINT "IDX_UQ_CollaboratorRoles_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "CollaboratorIndustries"
ADD CONSTRAINT "PK_CollaboratorIndustries" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "CollaboratorIndustries"
ADD CONSTRAINT "IDX_UQ_CollaboratorIndustries_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "CollaboratorGenders"
ADD CONSTRAINT "PK_CollaboratorGenders" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "CollaboratorGenders"
ADD CONSTRAINT "IDX_UQ_CollaboratorGenders_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "CollaboratorEditionParticipations"
ADD CONSTRAINT "PK_CollaboratorEditionParticipations" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "CollaboratorEditionParticipations"
ADD CONSTRAINT "IDX_UQ_CollaboratorEditionParticipations_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "CollaboratorEditionParticipations"
ADD CONSTRAINT "IDX_UQ_CollaboratorEditionParticipations_CollaboratorId_EditionId" UNIQUE ("CollaboratorId"  ASC,"EditionId"  ASC)
go

ALTER TABLE "CollaboratorRoles"
	ADD CONSTRAINT "FK_Users_CollaboratorRoles_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorRoles"
	ADD CONSTRAINT "FK_Users_CollaboratorRoles_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorIndustries"
	ADD CONSTRAINT "FK_Users_CollaboratorIndustries_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorIndustries"
	ADD CONSTRAINT "FK_Users_CollaboratorIndustries_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorGenders"
	ADD CONSTRAINT "FK_Users_CollaboratorGenders_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorGenders"
	ADD CONSTRAINT "FK_Users_CollaboratorGenders_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "dbo"."Collaborators"
	ADD CONSTRAINT "FK_CollaboratorGenders_Collaborators_CollaboratorGenderId" FOREIGN KEY ("CollaboratorGenderId") REFERENCES "CollaboratorGenders"("Id")
go

ALTER TABLE "dbo"."Collaborators"
	ADD CONSTRAINT "FK_CollaboratorRoles_Collaborators_CollaboratorRoleId" FOREIGN KEY ("CollaboratorRoleId") REFERENCES "CollaboratorRoles"("Id")
go

ALTER TABLE "dbo"."Collaborators"
	ADD CONSTRAINT "FK_CollaboratorIndustries_Collaborators_CollaboratorIndustryId" FOREIGN KEY ("CollaboratorIndustryId") REFERENCES "CollaboratorIndustries"("Id")
go

ALTER TABLE "CollaboratorEditionParticipations"
	ADD CONSTRAINT "FK_Collaborators_CollaboratorEditionParticipations_CollaboratorId" FOREIGN KEY ("CollaboratorId") REFERENCES "dbo"."Collaborators"("Id")
go

ALTER TABLE "CollaboratorEditionParticipations"
	ADD CONSTRAINT "FK_Editions_CollaboratorEditionParticipations_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "CollaboratorEditionParticipations"
	ADD CONSTRAINT "FK_Users_CollaboratorEditionParticipations_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "CollaboratorEditionParticipations"
	ADD CONSTRAINT "FK_Users_CollaboratorEditionParticipations_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go
