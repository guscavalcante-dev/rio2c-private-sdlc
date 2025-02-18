BEGIN TRY
    BEGIN TRANSACTION

        -- Drop foreign key constraints
        ALTER TABLE "NegotiationConfigs" DROP CONSTRAINT "FK_ProjectTypes_NegotiationConfigs_ProjectTypeId";
        ALTER TABLE "NegotiationRoomConfigs" DROP CONSTRAINT "FK_ProjectTypes_NegotiationRoomConfigs_ProjectTypeId";

        -- Drop columns
        ALTER TABLE dbo.NegotiationConfigs DROP COLUMN ProjectTypeId;
        ALTER TABLE dbo.NegotiationRoomConfigs DROP COLUMN ProjectTypeId;

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
