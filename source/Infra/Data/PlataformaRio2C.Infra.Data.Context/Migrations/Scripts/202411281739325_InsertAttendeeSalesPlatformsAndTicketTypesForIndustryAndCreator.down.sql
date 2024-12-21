BEGIN TRY
    BEGIN TRANSACTION
        -----------------------------------------------
        -- Revert CREATOR LOTE 1
        -----------------------------------------------
        DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
        WHERE [Uid] = '0BF41BD7-DCDC-4957-BF7B-DF56979A6996';

        -----------------------------------------------
        -- Revert CREATOR PRÉ-VENDA
        -----------------------------------------------
        DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
        WHERE [Uid] = '7A63E214-E9A8-4CA2-BAFD-880E6648F184';

        -----------------------------------------------
        -- Revert RIO2C 2025 | CREATOR
        -----------------------------------------------
        DELETE FROM [dbo].[AttendeeSalesPlatforms]
        WHERE [Uid] = 'B77A43B9-5DD3-4C0A-88EB-BABE62AACC18';

        -----------------------------------------------
        -- Revert INDUSTRY LOTE 1
        -----------------------------------------------
        DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
        WHERE [Uid] = 'A0673F26-5D38-4ABA-9B7F-3F3FF1C15898';

        -----------------------------------------------
        -- Revert INDUSTRY PRÉ-VENDA
        -----------------------------------------------
        DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
        WHERE [Uid] = 'B94E8A51-041A-4628-B328-7192D7F3E866';

        -----------------------------------------------
        -- Revert RIO2C 2025 | INDUSTRY
        -----------------------------------------------
        DELETE FROM [dbo].[AttendeeSalesPlatforms]
        WHERE [Uid] = '06177CC4-B5AD-4FAD-A774-C8F40F3F2EED';

        -----------------------------------------------
        -- Reactivate consulting the 2024 Edition events on Sympla
        -----------------------------------------------
        UPDATE AttendeeSalesPlatforms SET IsActive = 1 WHERE EditionId = 6;

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
