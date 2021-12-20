--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		-- Drop constraint
		ALTER TABLE AttendeeSalesPlatformTicketTypes DROP CONSTRAINT IDX_UQ_AttendeeSalesPlatformTicketTypes_AttendeeSalesPlatformId_TicketClassId

		-- Alter Table
		ALTER TABLE AttendeeSalesPlatformTicketTypes
		ALTER COLUMN TicketClassId varchar(200) NOT NULL

		-- Add constraint
		ALTER TABLE AttendeeSalesPlatformTicketTypes ADD CONSTRAINT [IDX_UQ_AttendeeSalesPlatformTicketTypes_AttendeeSalesPlatformId_TicketClassId] UNIQUE NONCLUSTERED 
		(
			[AttendeeSalesPlatformId] ASC,
			[TicketClassId] ASC
		)
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