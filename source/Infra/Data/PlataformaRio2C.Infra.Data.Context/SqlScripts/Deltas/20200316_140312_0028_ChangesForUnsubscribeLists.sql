--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."UserUnsubscribedLists"
DROP CONSTRAINT "IDX_UQ_UserUnsubscribedLists_UserId_SubscribeListId"
go

ALTER TABLE "dbo"."UserUnsubscribedLists"
DROP CONSTRAINT "IDX_UserUnsubscribedLists_SubscribeListId"
go

CREATE NONCLUSTERED INDEX "IDX_UserUnsubscribedLists_SubscribeListId" ON "dbo"."UserUnsubscribedLists"
( 
	"SubscribeListId"     ASC
)
go

ALTER TABLE "dbo"."UserUnsubscribedLists"
ADD CONSTRAINT "IDX_UQ_UserUnsubscribedLists_UserId_SubscribeListId" UNIQUE ("UserId"  ASC,"SubscribeListId"  ASC)
go
