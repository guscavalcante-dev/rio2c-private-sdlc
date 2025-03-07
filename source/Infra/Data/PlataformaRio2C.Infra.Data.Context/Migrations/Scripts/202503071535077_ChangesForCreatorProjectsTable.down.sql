BEGIN TRY
    BEGIN TRANSACTION
        ALTER TABLE CreatorProjects ALTER COLUMN OnlinePlatformsWhereProjectIsAvailable varchar(300) NULL
        ALTER TABLE CreatorProjects ALTER COLUMN AssociatedInstitutions varchar(300) NULL
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
