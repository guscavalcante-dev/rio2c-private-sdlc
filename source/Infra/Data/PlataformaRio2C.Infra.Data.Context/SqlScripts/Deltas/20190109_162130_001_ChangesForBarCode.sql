--must run on deploy: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"


ALTER TABLE "dbo"."AttendeeCollaboratorTickets"
ADD Barcode  varchar(40)  NULL
go



ALTER TABLE "dbo"."AttendeeCollaboratorTickets"
ADD IsBarcodeUsed  bit  NOT NULL
go



ALTER TABLE "dbo"."AttendeeCollaboratorTickets"
ADD IsBarcodePrinted  bit  NOT NULL
go



ALTER TABLE "dbo"."AttendeeCollaboratorTickets"
ADD BarcodeUpdateDate  datetime  NULL
go



ALTER TABLE "dbo"."AttendeeCollaboratorTickets"
ADD SalesPlatformUpdateDate  datetime  NOT NULL
go