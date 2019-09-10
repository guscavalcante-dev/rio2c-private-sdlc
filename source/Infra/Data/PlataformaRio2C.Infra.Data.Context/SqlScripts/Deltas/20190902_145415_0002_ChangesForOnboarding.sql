--must run on deploy: yes, done
--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Users"
ADD UserInterfaceLanguageId  int  NULL
go

UPDATE "dbo"."Users"
SET UserInterfaceLanguageId  = 2
go

ALTER TABLE "dbo"."Countries"
ADD DefaultLanguageId  int  NULL
go

update "dbo"."Countries"
SET DefaultLanguageId  = 1
go

update "dbo"."Countries"	
SET DefaultLanguageId  = 2
WHERE Code = 'BR'
go


ALTER TABLE "dbo"."Countries"
ALTER COLUMN DefaultLanguageId  int  NOT NULL
go


ALTER TABLE "dbo"."AttendeeOrganizations"
ADD OnboardingFinishDate  datetime  NULL
go

ALTER TABLE "dbo"."AttendeeCollaborators"
ADD WelcomeEmailSendDate  datetime  NULL
go

ALTER TABLE "dbo"."AttendeeCollaborators"
ADD OnboardingFinishDate  datetime  NULL
go

CREATE TABLE "SentEmails"
( 
    "Id"                 int IDENTITY ( 1,1 ) ,
    "Uid"                uniqueidentifier  NOT NULL ,
    "RecipientUserId"    int  NOT NULL ,
    "EditionId"          int  NULL ,
    "EmailType"          varchar(50)  NOT NULL ,
    "EmailSendDate"      datetime  NOT NULL ,
    "EmailReadDate"      datetime  NULL 
)
go

ALTER TABLE "SentEmails"
ADD CONSTRAINT "PK_SentEmails" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "SentEmails"
    ADD CONSTRAINT "FK_Users_SentEmails_RecipientUserId" FOREIGN KEY ("RecipientUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "SentEmails"
    ADD CONSTRAINT "FK_Editions_SentEmails_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
go

ALTER TABLE "dbo"."Users"
    ADD CONSTRAINT "FK_Languages_Users_UserInterfaceLanguageId" FOREIGN KEY ("UserInterfaceLanguageId") REFERENCES "dbo"."Languages"("Id")
go

ALTER TABLE "dbo"."Countries"
    ADD CONSTRAINT "FK_Languages_Countries_DefaultLanguageId" FOREIGN KEY ("DefaultLanguageId") REFERENCES "dbo"."Languages"("Id")
go




