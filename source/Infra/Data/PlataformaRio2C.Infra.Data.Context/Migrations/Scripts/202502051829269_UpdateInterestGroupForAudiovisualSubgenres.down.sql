BEGIN TRY
    BEGIN TRANSACTION


        -- Remove the Interests linked to the InterestGroup “Subgênero”
        DELETE FROM [dbo].[Interests]
        WHERE [InterestGroupId] = (SELECT TOP 1 [Id] FROM [dbo].[InterestGroups] WHERE [UID] = '55FD72C1-B3D5-4DD5-8A3E-44B1B0BAF5A3');

        -- Remove the InterestGroup “Subgênero”
        DELETE FROM [dbo].[InterestGroups]
        WHERE [Uid] = '55FD72C1-B3D5-4DD5-8A3E-44B1B0BAF5A3';


    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
    SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
