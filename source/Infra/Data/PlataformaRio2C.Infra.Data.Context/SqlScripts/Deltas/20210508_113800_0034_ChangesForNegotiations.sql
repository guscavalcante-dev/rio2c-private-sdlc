﻿--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		--Changes for Negotiations
		ALTER TABLE "dbo"."Negotiations"
		ADD EditionId  int  NULL
		;

		EXEC('UPDATE "dbo"."Negotiations" SET EditionId = (SELECT Id FROM "dbo"."Editions" WHERE UrlCode = 2020)')
		;

		ALTER TABLE "dbo"."Negotiations"
		ALTER COLUMN EditionId  int  NOT NULL
		;

		--Changes for Rooms
		ALTER TABLE "dbo"."Rooms"
		ADD IsVirtualMeeting  bit  NULL
		;

		ALTER TABLE "dbo"."Rooms"
		ADD VirtualMeetingUrl  varchar(400)  NULL
		;

		EXEC('UPDATE "dbo"."Rooms" SET IsVirtualMeeting = 0')
		;

		ALTER TABLE "dbo"."Rooms"
		ALTER COLUMN IsVirtualMeeting  bit  NOT NULL
		;

		--Changes for AttendeeOrganizations
		ALTER TABLE "dbo"."AttendeeOrganizations"
		ADD IsVirtualMeeting  bit  NULL
		;

		EXEC('UPDATE dbo.AttendeeOrganizations 
					SET IsVirtualMeeting = 0
					WHERE Id in
					(
						select ao.Id
						from 
							dbo.AttendeeOrganizations ao
							INNER JOIN dbo.AttendeeOrganizationTypes aot ON ao.Id = aot.AttendeeOrganizationId
							INNER JOIN dbo.OrganizationTypes ot ON aot.OrganizationTypeId = ot.Id
						WHERE
							ot.Name = ''Player''
					)');

		--Create Indexes
		CREATE NONCLUSTERED INDEX "IDX_Negotiations_EditionId_ProjectBuyerEvaluationId" ON "dbo"."Negotiations"
		( 
			"EditionId"           ASC,
			"ProjectBuyerEvaluationId"  ASC
		)
		;

		CREATE NONCLUSTERED INDEX "IDX_Negotiations_EditionId_StartDate" ON "dbo"."Negotiations"
		( 
			"EditionId"           ASC,
			"StartDate"           ASC
		)
		;

		CREATE NONCLUSTERED INDEX "IDX_Negotiations_EditionId" ON "dbo"."Negotiations"
		( 
			"EditionId"           ASC
		)
		;

		ALTER TABLE "dbo"."Negotiations"
			ADD CONSTRAINT "FK_Editions_Negotiations_EditionId" FOREIGN KEY ("EditionId") REFERENCES "dbo"."Editions"("Id")
		;

	-- Commands End
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
