BEGIN TRY
    BEGIN TRANSACTION

        -- Delete Interests
        DELETE FROM [dbo].[Interests] 
        WHERE [Uid] IN (
            'E3B5C1A2-4D4F-4D72-9D6F-8C8F4A2D9B74', -- Doc / Factual
            '9F7C8E62-3A49-4DA8-80B2-3E6A417E57A2', -- Animação | Animation
            '5D2F9B8A-1E5D-4B49-9B3E-2F1D6C7A8F9E', -- Reality Show
            '4A6D3E7C-9F2B-42A7-B6D8-5E3F1A8B9C7D'  -- Ficção | Fiction
        );

        -- Delete InterestGroup
        DELETE FROM [dbo].[InterestGroups] 
        WHERE [Uid] = 'DFFD8186-1E7E-4730-B005-56D1506C9DB6';

    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    -- Re-raise the error
    DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
    SELECT 
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
