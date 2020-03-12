--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"


ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "ValuePerEpisode" varchar(50)
go

ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "TotalValueOfProject" varchar(50)
go

ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "ValueAlreadyRaised" varchar(50)
go

ALTER TABLE "dbo"."Projects" 
   ALTER COLUMN "ValueStillNeeded" varchar(50)
go
