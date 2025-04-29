BEGIN TRY
    BEGIN TRANSACTION
        -----------------------------------------------
        -- FESTIVALIA - 31.05
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '7B9B51AC-7D6E-4639-B18F-760A8111D54C')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatformTicketTypes] 
            WHERE Uid = '7B9B51AC-7D6E-4639-B18F-760A8111D54C'
        END;

        -----------------------------------------------
        -- FESTIVALIA 31.05 - INGRESSO SOCIAL
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatforms] WHERE Uid = 'B6601073-646B-4FF5-BDBB-E98E3F664363')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatforms] 
            WHERE Uid = 'B6601073-646B-4FF5-BDBB-E98E3F664363'
        END;

        -----------------------------------------------
        -- FESTIVALIA 01.06 - INGRESSO SOCIAL
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatforms] WHERE Uid = 'E451E258-D18E-4816-A52C-FA26EDAF6D32')
        BEGIN
            DELETE FROM [dbo].[AttendeeSalesPlatforms] 
            WHERE Uid = 'E451E258-D18E-4816-A52C-FA26EDAF6D32'
        END;

    COMMIT TRAN -- Reversão concluída com sucesso
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN -- Rollback em caso de erro

    -- Informações do erro
    DECLARE @ErrorLine INT;
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT
        @ErrorLine = ERROR_LINE(),
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();
         
    RAISERROR ('Erro encontrado na linha %i: %s', @ErrorSeverity, @ErrorState, @ErrorLine, @ErrorMessage) WITH SETERROR
END CATCH