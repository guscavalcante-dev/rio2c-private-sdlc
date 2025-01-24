BEGIN TRY
    BEGIN TRANSACTION

        -- Drop foreign key constraints
        ALTER TABLE "MusicBusinessRoundProjectActivities" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectActivities_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjectActivities" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectActivities_CreateUserId";
        ALTER TABLE "MusicBusinessRoundProjectActivities" DROP CONSTRAINT "FK_Activities_MusicBusinessRoundProjectActivities_ActivityId";
        ALTER TABLE "MusicBusinessRoundProjectActivities" DROP CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectActivities_MusicBusinessRoundProjectId";

        -- Drop unique and primary key constraints
        ALTER TABLE "MusicBusinessRoundProjectActivities" DROP CONSTRAINT "IDX_UQ_MusicBusinessRoundProjectActivities";
        ALTER TABLE "MusicBusinessRoundProjectActivities" DROP CONSTRAINT "PK_MusicBusinessRoundProjectActivities";

        -- Drop table
        DROP TABLE "MusicBusinessRoundProjectActivities";

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
