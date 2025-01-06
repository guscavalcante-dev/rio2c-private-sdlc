BEGIN TRY
	BEGIN TRANSACTION
		   -----------------------------------------------
        -- Update IsDeleted to 1 produtor musical in TargetAudiences records
        -----------------------------------------------
        UPDATE [dbo].[TargetAudiences]
        SET [IsDeleted] = 0
        WHERE [Uid] IN (
            'ABF2CDE5-88FF-4B8E-82F2-5E8F0F16D9A4' -- Produtor Musical
        );
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