--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

CREATE NONCLUSTERED INDEX "IDX_Messages_SenderId" ON "dbo"."Messages"
( 
	"SenderId"            ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_Messages_RecipientId" ON "dbo"."Messages"
( 
	"RecipientId"         ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_Messages_NotificationEmailSendDate" ON "dbo"."Messages"
( 
	"NotificationEmailSendDate"  ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_Messages_ReadDate" ON "dbo"."Messages"
( 
	"ReadDate"            ASC
)
go

CREATE NONCLUSTERED INDEX "IDX_Users_IsDeleted" ON "dbo"."Users"
(
	"IsDeleted" ASC
)
GO
