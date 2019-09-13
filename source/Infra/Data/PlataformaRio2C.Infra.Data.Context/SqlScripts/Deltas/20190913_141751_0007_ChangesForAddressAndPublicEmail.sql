--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Collaborators"
ADD PublicEmail  varchar(256)  NULL
go

ALTER TABLE "dbo"."Addresses"
DROP COLUMN Address2
go
