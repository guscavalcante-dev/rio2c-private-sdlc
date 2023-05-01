--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		CREATE TABLE "AttendeeNegotiationCollaborators"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"NegotiationId"      int  NOT NULL ,
			"AttendeeCollaboratorId" int  NOT NULL ,
			"IsDeleted"          bit  NOT NULL,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateUserId"       int  NOT NULL
		);

		ALTER TABLE "AttendeeNegotiationCollaborators"
			ADD CONSTRAINT "PK_AttendeeNegotiationCollaborators" PRIMARY KEY  CLUSTERED ("Id" ASC);

		ALTER TABLE "AttendeeNegotiationCollaborators"
			ADD CONSTRAINT "IDX_UQ_AttendeeNegotiationCollaborators_NegotiationId_AttendeeCollaboratorId" UNIQUE ("AttendeeCollaboratorId"  ASC, "NegotiationId"  ASC);

		ALTER TABLE "AttendeeNegotiationCollaborators"
			ADD CONSTRAINT "IDX_UQ_AttendeeNegotiationCollaborators_Uid" UNIQUE ("Uid"  ASC);

		ALTER TABLE "AttendeeNegotiationCollaborators"
			ADD CONSTRAINT "FK_Negotiations_AttendeeNegotiationCollaborators_NegotiationId" FOREIGN KEY ("NegotiationId") REFERENCES "dbo"."Negotiations"("Id");

		ALTER TABLE "AttendeeNegotiationCollaborators"
			ADD CONSTRAINT "FK_AttendeeCollaborators_AttendeeNegotiationCollaborators_AttendeeCollaboratorId" FOREIGN KEY ("AttendeeCollaboratorId") REFERENCES "dbo"."AttendeeCollaborators"("Id");

		ALTER TABLE "AttendeeNegotiationCollaborators"
			ADD CONSTRAINT "FK_Users_AttendeeNegotiationCollaborators_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id");

		ALTER TABLE "AttendeeNegotiationCollaborators"
			ADD CONSTRAINT "FK_Users_AttendeeNegotiationCollaborators_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id");

	COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN --RollBack in case of Error

	-- Raise ERROR with RAISEERROR() Statement including the details of the exception
	DECLARE @ErrorLine INT;
	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT
		@ErrorLine = ERROR_LINE(),
		@ErrorMessage = ERROR_MESSAGE(),
		@ErrorSeverity = ERROR_SEVERITY(),
		@ErrorState = ERROR_STATE();
		 
	RAISERROR ('Error found in line %i: %s', @ErrorSeverity, @ErrorState, @ErrorLine, @ErrorMessage) WITH SETERROR
END CATCH