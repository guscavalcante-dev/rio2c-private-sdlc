BEGIN TRY
    BEGIN TRANSACTION

        -- 1. Create a new ProjectType “Audiovisual - Pitching”
        INSERT INTO [dbo].[ProjectTypes]
            ([Uid], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
        VALUES
            ('E5B7CBA4-10B5-4FCF-A778-09B40AEB6C01', 'Audiovisual - Pitching', 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1);

        -- 2. Rename the ProjectType with Uid '3CE14508-8F6F-4D9D-B5F2-C7B53BA031E0' from “Áudio Visual“ to “Audiovisual - Rodadas de Negócio“
        UPDATE [dbo].[ProjectTypes]
        SET [Name] = 'Audiovisual - Rodadas de Negócio'
        WHERE [Uid] = '3CE14508-8F6F-4D9D-B5F2-C7B53BA031E0';

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
    SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;

GO
