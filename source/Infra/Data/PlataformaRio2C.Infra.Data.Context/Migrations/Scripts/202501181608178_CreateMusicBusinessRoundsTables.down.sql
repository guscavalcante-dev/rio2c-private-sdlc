BEGIN TRY
    BEGIN TRANSACTION

        -- Drop Foreign Keys
        ALTER TABLE "MusicBusinessRoundProjectsInterests" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectsInterests_CreateUserId";
        ALTER TABLE "MusicBusinessRoundProjectsInterests" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectsInterests_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjectsInterests" DROP CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectsInterests_MusicBusinessRoundProjectId";
        ALTER TABLE "MusicBusinessRoundProjectsInterests" DROP CONSTRAINT "FK_Interests_MusicBusinessRoundProjectsInterests_InterestId";

        ALTER TABLE "MusicBusinessRoundProjectsTargetAudiences" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectsTargetAudiences_CreateUserId";
        ALTER TABLE "MusicBusinessRoundProjectsTargetAudiences" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectsTargetAudiences_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjectsTargetAudiences" DROP CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectsTargetAudiences_MusicBusinessRoundProjectId";
        ALTER TABLE "MusicBusinessRoundProjectsTargetAudiences" DROP CONSTRAINT "FK_TargetAudiences_MusicBusinessRoundProjectsTargetAudiences_TargetAudienceId";

        ALTER TABLE "MusicBusinessRoundProjects" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjects_CreateUserId";
        ALTER TABLE "MusicBusinessRoundProjects" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjects_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjects" DROP CONSTRAINT "FK_AttendeeOrganizations_MusicBusinessRoundProjects_SellerAttendeeOrganizationId";

        ALTER TABLE "PlayerCategories" DROP CONSTRAINT "FK_Users_PlayerCategories_CreateUserId";
        ALTER TABLE "PlayerCategories" DROP CONSTRAINT "FK_Users_PlayerCategories_UpdateUserId";
        ALTER TABLE "PlayerCategories" DROP CONSTRAINT "FK_ProjectTypes_PlayerCategories_ProjectTypeId";

        ALTER TABLE "MusicBusinessRoundProjectsPlayerCategories" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectsPlayerCategories_CreateUserId";
        ALTER TABLE "MusicBusinessRoundProjectsPlayerCategories" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectsPlayerCategories_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjectsPlayerCategories" DROP CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectsPlayerCategories_MusicBusinessRoundProjectId";
        ALTER TABLE "MusicBusinessRoundProjectsPlayerCategories" DROP CONSTRAINT "FK_PlayerCategories_MusicBusinessRoundProjectsPlayerCategories_PlayerCategoryId";

        -- Drop Tables
        DROP TABLE "MusicBusinessRoundProjectsPlayerCategories";
        DROP TABLE "PlayerCategories";
        DROP TABLE "MusicBusinessRoundProjectsTargetAudiences";
        DROP TABLE "MusicBusinessRoundProjectsInterests";
        DROP TABLE "MusicBusinessRoundProjects";

        -- Drop Constraints
        ALTER TABLE "dbo"."InterestGroups" ALTER COLUMN "Name" varchar(255); -- Revert to previous column size

        -- Drop Added Columns
        ALTER TABLE "dbo"."Interests" DROP COLUMN "Description";

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
END CATCH
