BEGIN TRY
    BEGIN TRANSACTION
        -----------------------------------------------
        -- Remove FESTIVALIA - 31.05
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '8E19E553-4A82-4BB8-916D-6E30432B6456')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = '8E19E553-4A82-4BB8-916D-6E30432B6456'
        END;

        -----------------------------------------------
        -- Remove FESTIVALIA - 01.06
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '0139653C-E572-4F00-BB80-80FD406CD039')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = '0139653C-E572-4F00-BB80-80FD406CD039'
        END;

        -----------------------------------------------
        -- Remove DAY PASS - 27.05
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = 'FB7004AA-83D1-4162-BE78-FD643DD86B3E')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = 'FB7004AA-83D1-4162-BE78-FD643DD86B3E'
        END;

        -----------------------------------------------
        -- Remove DAY PASS - 28.05
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '4F5ADCBF-C9BD-4AEF-8C8A-FA92A43CE954')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = '4F5ADCBF-C9BD-4AEF-8C8A-FA92A43CE954'
        END;

        -----------------------------------------------
        -- Remove DAY PASS - 29.05
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '8CD65D5B-8D4D-4304-B25C-D5270DF26195')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = '8CD65D5B-8D4D-4304-B25C-D5270DF26195'
        END;

        -----------------------------------------------
        -- Remove DAY PASS - 30.05
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '63290832-F7F1-47FE-81A5-A0C2350753F2')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = '63290832-F7F1-47FE-81A5-A0C2350753F2'
        END;

        -----------------------------------------------
        -- Remove BUSINESS DAY 27.05 - CORTESIA
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '4D752391-1508-4FCE-9CB7-628FB5102E10')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = '4D752391-1508-4FCE-9CB7-628FB5102E10'
        END;

        -----------------------------------------------
        -- Remove BUSINESS DAY 28.05 - CORTESIA
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '75C0921D-937B-4BF7-A9EB-AF86D36B7F17')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = '75C0921D-937B-4BF7-A9EB-AF86D36B7F17'
        END;

        -----------------------------------------------
        -- Remove BUSINESS DAY 29.05 - CORTESIA
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '71412C38-8301-4595-96E7-E1C405189CFE')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = '71412C38-8301-4595-96E7-E1C405189CFE'
        END;

        -----------------------------------------------
        -- Remove BUSINESS DAY 30.05 - CORTESIA
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = 'E1B9E452-514A-497D-8978-106A018485D6')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes]
            WHERE Uid = 'E1B9E452-514A-497D-8978-106A018485D6'
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