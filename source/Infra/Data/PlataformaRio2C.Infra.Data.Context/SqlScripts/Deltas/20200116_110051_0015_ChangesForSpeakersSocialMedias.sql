--must run on deploy | test: yes, hot done
--must run on deploy | prod: yes, hot done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Organizations" 
   ALTER COLUMN "Website" varchar(300)
go

ALTER TABLE "dbo"."Organizations"
ADD Linkedin  varchar(100)  NULL
go

ALTER TABLE "dbo"."Organizations"
ADD Instagram  varchar(100)  NULL
go

ALTER TABLE "dbo"."Organizations"
ADD Twitter  varchar(100)  NULL
go

ALTER TABLE "dbo"."Organizations"
ADD Youtube  varchar(300)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD Website  varchar(300)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD Linkedin  varchar(100)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD Instagram  varchar(100)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD Twitter  varchar(100)  NULL
go

ALTER TABLE "dbo"."Collaborators"
ADD Youtube  varchar(300)  NULL
go
