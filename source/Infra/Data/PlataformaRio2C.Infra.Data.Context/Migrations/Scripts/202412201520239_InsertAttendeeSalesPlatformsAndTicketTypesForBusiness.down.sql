BEGIN TRY
    BEGIN TRANSACTION
        -----------------------------------------------
        -- Revert BUSINESS LOTE 1
        -----------------------------------------------
        DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
        WHERE [Uid] = 'DE5D4CD9-CEC9-4E18-8172-406FA8E07DAD';

        -----------------------------------------------
        -- Revert RIO2C 2025 | CREATOR
        -----------------------------------------------
        DELETE FROM [dbo].[AttendeeSalesPlatforms]
        WHERE [Uid] = '07937D79-3A74-455A-8FD7-BE0AD638F91B';

    COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN -- Rollback in case of error

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
         
    RAISERROR ('Error found in line %i: %s', @ErrorSeverity, @ErrorState, @ErrorLine, @ErrorMessage) WITH SETERROR;
END CATCH;
