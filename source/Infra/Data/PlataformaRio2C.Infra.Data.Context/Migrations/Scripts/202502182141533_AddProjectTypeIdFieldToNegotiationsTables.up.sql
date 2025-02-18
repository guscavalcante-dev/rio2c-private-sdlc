BEGIN TRY
    BEGIN TRANSACTION

        -- Adding project type id to tables
       	ALTER TABLE dbo.NegotiationConfigs add ProjectTypeId INT NOT NULL DEFAULT 1
        ALTER TABLE dbo.NegotiationRoomConfigs add ProjectTypeId INT NOT NULL DEFAULT 1

        ALTER TABLE "NegotiationConfigs"
        ADD CONSTRAINT "FK_ProjectTypes_NegotiationConfigs_ProjectTypeId" FOREIGN KEY ("ProjectTypeId") REFERENCES "dbo"."ProjectTypes"("Id")

        ALTER TABLE "NegotiationRoomConfigs"
        ADD CONSTRAINT "FK_ProjectTypes_NegotiationRoomConfigs_ProjectTypeId" FOREIGN KEY ("ProjectTypeId") REFERENCES "dbo"."ProjectTypes"("Id")


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
