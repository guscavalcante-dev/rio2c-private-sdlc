--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

CREATE TABLE "Connections"
( 
	"ConnectionId"       uniqueidentifier  NOT NULL ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"UserId"             int  NOT NULL ,
	"UserAgent"          varchar(500)  NULL ,
	"CreateDate"         datetimeoffset  NOT NULL ,
	"UpdateDate"         datetimeoffset  NOT NULL 
)
go

CREATE NONCLUSTERED INDEX "IDX_Connections_UserId" ON "Connections"
( 
	"UserId"              ASC
)
go

ALTER TABLE "Connections"
ADD CONSTRAINT "PK_Connections" PRIMARY KEY  CLUSTERED ("ConnectionId" ASC)
go

ALTER TABLE "Connections"
ADD CONSTRAINT "IDX_UQ_Connections_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "Connections"
	ADD CONSTRAINT "FK_Users_Connections_UserId" FOREIGN KEY ("UserId") REFERENCES "dbo"."Users"("Id")
go
