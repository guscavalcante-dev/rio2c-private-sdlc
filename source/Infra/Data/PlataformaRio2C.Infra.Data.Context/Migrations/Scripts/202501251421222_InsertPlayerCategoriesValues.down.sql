BEGIN TRY
    BEGIN TRANSACTION
        -----------------------------------------------
        -- PlayerCategories Deletions
        -----------------------------------------------

        -- Revert Selo
        DELETE FROM [dbo].[PlayerCategories]
        WHERE [Uid] = 'F1C7D5F2-9B32-46C9-8946-E89B2F5B4A10';

        -- Revert Gravadora
        DELETE FROM [dbo].[PlayerCategories]
        WHERE [Uid] = 'EA59F761-5D3C-482F-8F37-2B1E8B3A5199';

        -- Revert Editora e Associações
        DELETE FROM [dbo].[PlayerCategories]
        WHERE [Uid] = 'A6C1F3E5-B7A9-4F2E-A1C5-47D73A41E231';

        -- Revert Agregadora
        DELETE FROM [dbo].[PlayerCategories]
        WHERE [Uid] = 'B1D1A7D7-1F62-4A99-B6D8-BC2E51B35A01';

        -- Commands End
        COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN -- RollBack in case of Error

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
END CATCH;
