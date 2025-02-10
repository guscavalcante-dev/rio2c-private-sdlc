BEGIN TRY
    BEGIN TRANSACTION

        -- Remove the inserted ProjectType “Audiovisual - Pitching”
        DELETE FROM [dbo].[ProjectTypes]
        WHERE [Uid] = 'E5B7CBA4-10B5-4FCF-A778-09B40AEB6C01';

        -- Restore the original ProjectType Id=1 name to “Áudio Visual”
        UPDATE [dbo].[ProjectTypes]
        SET [Name] = 'Áudio Visual',
            [UpdateDate] = SYSDATETIMEOFFSET(),
            [UpdateUserId] = 1
        WHERE [Uid] = '3CE14508-8F6F-4D9D-B5F2-C7B53BA031E0';


    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
    SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
