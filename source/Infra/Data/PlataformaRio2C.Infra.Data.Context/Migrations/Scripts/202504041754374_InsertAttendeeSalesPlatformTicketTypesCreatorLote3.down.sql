BEGIN TRY
    BEGIN TRANSACTION
        -- Remove the record from the AttendeeSalesPlatformTicketTypes table
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = 'EBA3F1EE-346C-4303-A828-BCAA88C79EF9')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = 'EBA3F1EE-346C-4303-A828-BCAA88C79EF9';
        END;

        -- Remove the record from the AttendeeSalesPlatformTicketTypes table
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '9AA24767-D47D-4A11-9CFC-12EC30E4A823')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = '9AA24767-D47D-4A11-9CFC-12EC30E4A823';
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