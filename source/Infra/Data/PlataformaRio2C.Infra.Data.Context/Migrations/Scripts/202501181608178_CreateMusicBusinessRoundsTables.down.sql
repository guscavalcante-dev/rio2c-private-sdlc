BEGIN TRY
    BEGIN TRANSACTION

        -- Drop foreign key constraints
        ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations" DROP CONSTRAINT "FK_ProjectEvaluationRefuseReasons_MusicBusinessRoundProjectBuyerEvaluations_ProjectEvaluationRefuseReasonId";
        ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations" DROP CONSTRAINT "FK_ProjectEvaluationStatuses_MusicBusinessRoundProjectBuyerEvaluations_ProjectEvaluationStatusId";
        ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations" DROP CONSTRAINT "FK_AttendeeOrganizations_MusicBusinessRoundProjectBuyerEvaluations_BuyerAttendeeOrganizationId";
        ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations" DROP CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectBuyerEvaluations_MusicBusinessRoundProjectId";
        ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectBuyerEvaluations_BuyerEvaluationUserId";
        ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectBuyerEvaluations_SellerUserId";
        ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectBuyerEvaluations_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectBuyerEvaluations_CreateUserId";

        ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings" DROP CONSTRAINT "FK_Languages_MusicBusinessRoundProjectExpectationsForMeetings_LanguageId";
        ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings" DROP CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectExpectationsForMeetings_MusicBusinessRoundProjectId";
        ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectExpectationsForMeetings_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectExpectationsForMeetings_CreateUserId";

        ALTER TABLE "MusicBusinessRoundProjectPlayerCategories" DROP CONSTRAINT "FK_PlayerCategories_MusicBusinessRoundProjectPlayerCategories_PlayerCategoryId";
        ALTER TABLE "MusicBusinessRoundProjectPlayerCategories" DROP CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectPlayerCategories_MusicBusinessRoundProjectId";
        ALTER TABLE "MusicBusinessRoundProjectPlayerCategories" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectPlayerCategories_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjectPlayerCategories" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectPlayerCategories_CreateUserId";

        ALTER TABLE "PlayerCategories" DROP CONSTRAINT "FK_ProjectTypes_PlayerCategories_ProjectTypeId";
        ALTER TABLE "PlayerCategories" DROP CONSTRAINT "FK_Users_PlayerCategories_UpdateUserId";
        ALTER TABLE "PlayerCategories" DROP CONSTRAINT "FK_Users_PlayerCategories_CreateUserId";

        ALTER TABLE "MusicBusinessRoundProjects" DROP CONSTRAINT "FK_AttendeeCollaborators_MusicBusinessRoundProjects_SellerAttendeeCollaboratorId";
        ALTER TABLE "MusicBusinessRoundProjects" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjects_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjects" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjects_CreateUserId";

        ALTER TABLE "MusicBusinessRoundProjectTargetAudiences" DROP CONSTRAINT "FK_TargetAudiences_MusicBusinessRoundProjectTargetAudiences_TargetAudienceId";
        ALTER TABLE "MusicBusinessRoundProjectTargetAudiences" DROP CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectTargetAudiences_MusicBusinessRoundProjectId";
        ALTER TABLE "MusicBusinessRoundProjectTargetAudiences" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectTargetAudiences_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjectTargetAudiences" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectTargetAudiences_CreateUserId";

        ALTER TABLE "MusicBusinessRoundProjectInterests" DROP CONSTRAINT "FK_Interests_MusicBusinessRoundProjectInterests_InterestId";
        ALTER TABLE "MusicBusinessRoundProjectInterests" DROP CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectInterests_MusicBusinessRoundProjectId";
        ALTER TABLE "MusicBusinessRoundProjectInterests" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectInterests_UpdateUserId";
        ALTER TABLE "MusicBusinessRoundProjectInterests" DROP CONSTRAINT "FK_Users_MusicBusinessRoundProjectInterests_CreateUserId";

        -- Drop tables
        DROP TABLE "MusicBusinessRoundProjectBuyerEvaluations";
        DROP TABLE "MusicBusinessRoundProjectExpectationsForMeetings";
        DROP TABLE "MusicBusinessRoundProjectPlayerCategories";
        DROP TABLE "PlayerCategories";
        DROP TABLE "MusicBusinessRoundProjects";
        DROP TABLE "MusicBusinessRoundProjectTargetAudiences";
        DROP TABLE "MusicBusinessRoundProjectInterests";

        -- Revert column changes
        ALTER TABLE "dbo"."InterestGroups" ALTER COLUMN "Name" varchar(200);
        ALTER TABLE "dbo"."Interests" DROP COLUMN "Description";

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
