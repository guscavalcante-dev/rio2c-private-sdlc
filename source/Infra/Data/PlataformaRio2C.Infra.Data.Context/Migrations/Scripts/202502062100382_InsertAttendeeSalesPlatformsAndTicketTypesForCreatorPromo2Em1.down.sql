BEGIN TRY
    BEGIN TRANSACTION
        -- Remove the record from the AttendeeSalesPlatformTicketTypes table
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = 'DF04700C-63B7-4593-A010-402598A4AEC4')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = 'DF04700C-63B7-4593-A010-402598A4AEC4';
        END;

        -- Remove the record from the AttendeeSalesPlatforms table
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatforms] WHERE Uid = '18C99DF0-1515-4D35-A145-707112F84FF1')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatforms]
            WHERE Uid = '18C99DF0-1515-4D35-A145-707112F84FF1';
        END;

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