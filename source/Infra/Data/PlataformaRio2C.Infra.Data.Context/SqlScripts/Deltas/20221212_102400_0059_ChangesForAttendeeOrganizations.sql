--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
	
		ALTER TABLE "dbo"."Organizations"
		ADD IsVirtualMeeting  bit  NULL
		
		EXEC('
			UPDATE o SET o.IsVirtualMeeting = ao.IsVirtualMeeting
			FROM Organizations o
			LEFT JOIN AttendeeOrganizations ao ON o.Id = ao.OrganizationId
			WHERE ao.EditionId = 5
		');
		
		ALTER TABLE "dbo"."AttendeeOrganizations"
		DROP COLUMN "IsVirtualMeeting"

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