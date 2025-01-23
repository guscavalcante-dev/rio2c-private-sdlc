BEGIN TRY
    BEGIN TRANSACTION

        CREATE TABLE "MusicBusinessRoundProjectActivities"
        ( 
	        "Id"                 int IDENTITY ( 1,1 ) ,
	        "Uid"                uniqueidentifier  NOT NULL ,
	        "MusicBusinessRoundProjectId" int  NOT NULL ,
	        "ActivityId"         int  NOT NULL ,
	        "AdditionalInfo"     nvarchar(200)  NULL ,
	        "IsDeleted"          bit  NOT NULL ,
	        "CreateDate"         datetimeoffset  NOT NULL ,
	        "CreateUserId"       int  NOT NULL ,
	        "UpdateDate"         datetimeoffset  NOT NULL ,
	        "UpdateUserId"       int  NOT NULL 
        )

        ALTER TABLE "MusicBusinessRoundProjectActivities"
        ADD CONSTRAINT "PK_MusicBusinessRoundProjectActivities" PRIMARY KEY  CLUSTERED ("Id" ASC)

        ALTER TABLE "MusicBusinessRoundProjectActivities"
        ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjectActivities" UNIQUE ("Uid"  ASC)

        ALTER TABLE "MusicBusinessRoundProjectActivities"
        ADD CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectActivities_MusicBusinessRoundProjectId" FOREIGN KEY ("MusicBusinessRoundProjectId") REFERENCES "MusicBusinessRoundProjects"("Id")

        ALTER TABLE "MusicBusinessRoundProjectActivities"
        ADD CONSTRAINT "FK_Activities_MusicBusinessRoundProjectActivities_ActivityId" FOREIGN KEY ("ActivityId") REFERENCES "dbo"."Activities"("Id")

        ALTER TABLE "MusicBusinessRoundProjectActivities"
        ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectActivities_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

        ALTER TABLE "MusicBusinessRoundProjectActivities"
        ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectActivities_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

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
