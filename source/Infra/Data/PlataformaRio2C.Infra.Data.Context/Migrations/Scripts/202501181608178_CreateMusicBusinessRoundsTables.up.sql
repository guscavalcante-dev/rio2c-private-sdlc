BEGIN TRY
	BEGIN TRANSACTION

		ALTER TABLE "dbo"."Interests"
		ADD Description  varchar(500)  NULL

		ALTER TABLE "dbo"."InterestGroups" 
		ALTER COLUMN "Name" varchar(300)

		CREATE TABLE "MusicBusinessRoundProjectInterests"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"MusicBusinessRoundProjectId" int  NOT NULL ,
			"InterestId"         int  NOT NULL ,
			"AdditionalInfo"     varchar(200)  NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)

		CREATE TABLE "MusicBusinessRoundProjectTargetAudiences"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"MusicBusinessRoundProjectId" int  NOT NULL ,
			"TargetAudienceId"   int  NOT NULL ,
			"AdditionalInfo"     nvarchar(200)  NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)

		CREATE TABLE "MusicBusinessRoundProjects"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"SellerAttendeeCollaboratorId" int  NOT NULL ,
			"PlayerCategoriesThatHaveOrHadContract" varchar(300)  NULL ,
			"AttachmentUrl"      varchar(300)  NULL ,
			"FinishDate"         datetimeoffset  NULL ,
			"ProjectBuyerEvaluationsCount" int  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)

		CREATE TABLE "PlayerCategories"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"ProjectTypeId"      int  NOT NULL ,
			"Name"               varchar(500)  NULL ,
			"DisplayOrder"       int  NOT NULL ,
			"HasAdditionalInfo"  bit  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)

		CREATE TABLE "MusicBusinessRoundProjectPlayerCategories"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"MusicBusinessRoundProjectId" int  NOT NULL ,
			"PlayerCategoryId"   int  NOT NULL ,
			"AdditionalInfo"     nvarchar(200)  NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)

		CREATE TABLE "MusicBusinessRoundProjectExpectationsForMeetings"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"MusicBusinessRoundProjectId" int  NOT NULL ,
			"LanguageId"         int  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)

		CREATE TABLE "MusicBusinessRoundProjectBuyerEvaluations"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"MusicBusinessRoundProjectId" int  NOT NULL ,
			"BuyerAttendeeOrganizationId" int  NOT NULL ,
			"ProjectEvaluationStatusId" int  NULL ,
			"ProjectEvaluationRefuseReasonId" int  NULL ,
			"Reason"             varchar(1500)  NULL ,
			"SellerUserId"       int  NOT NULL ,
			"BuyerEvaluationUserId" int  NULL ,
			"EvaluationDate"     datetimeoffset  NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL ,
			"BuyerEmailSendDate" datetimeoffset  NULL 
		)

		ALTER TABLE "MusicBusinessRoundProjectInterests"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjectInterests" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "MusicBusinessRoundProjectInterests"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjectInterests" UNIQUE ("Uid"  ASC)

		ALTER TABLE "MusicBusinessRoundProjectTargetAudiences"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjectTargetAudiences" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "MusicBusinessRoundProjectTargetAudiences"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjectTargetAudiences" UNIQUE ("Uid"  ASC)

		ALTER TABLE "MusicBusinessRoundProjects"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjects" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "MusicBusinessRoundProjects"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjects" UNIQUE ("Uid"  ASC)

		ALTER TABLE "PlayerCategories"
		ADD CONSTRAINT "PK_PlayerCategories" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "PlayerCategories"
		ADD CONSTRAINT "IDX_UQ_PlayerCategories" UNIQUE ("Uid"  ASC)

		ALTER TABLE "MusicBusinessRoundProjectPlayerCategories"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjectPlayerCategories" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "MusicBusinessRoundProjectPlayerCategories"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjectPlayerCategories" UNIQUE ("Uid"  ASC)

		ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjectExpectationsForMeetings" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjectExpectationsForMeetings" UNIQUE ("Uid"  ASC)

		ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjectBuyerEvaluations" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjectBuyerEvaluations" UNIQUE ("Uid"  ASC)

		ALTER TABLE "MusicBusinessRoundProjectInterests"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectInterests_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectInterests"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectInterests_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectInterests"
			ADD CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectInterests_MusicBusinessRoundProjectId" FOREIGN KEY ("MusicBusinessRoundProjectId") REFERENCES "MusicBusinessRoundProjects"("Id")

		ALTER TABLE "MusicBusinessRoundProjectInterests"
			ADD CONSTRAINT "FK_Interests_MusicBusinessRoundProjectInterests_InterestId" FOREIGN KEY ("InterestId") REFERENCES "dbo"."Interests"("Id")

		ALTER TABLE "MusicBusinessRoundProjectTargetAudiences"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectTargetAudiences_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectTargetAudiences"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectTargetAudiences_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectTargetAudiences"
			ADD CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectTargetAudiences_MusicBusinessRoundProjectId" FOREIGN KEY ("MusicBusinessRoundProjectId") REFERENCES "MusicBusinessRoundProjects"("Id")

		ALTER TABLE "MusicBusinessRoundProjectTargetAudiences"
			ADD CONSTRAINT "FK_TargetAudiences_MusicBusinessRoundProjectTargetAudiences_TargetAudienceId" FOREIGN KEY ("TargetAudienceId") REFERENCES "dbo"."TargetAudiences"("Id")

		ALTER TABLE "MusicBusinessRoundProjects"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjects_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjects"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjects_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjects"
			ADD CONSTRAINT "FK_AttendeeCollaborators_MusicBusinessRoundProjects_SellerAttendeeCollaboratorId" FOREIGN KEY ("SellerAttendeeCollaboratorId") REFERENCES "dbo"."AttendeeCollaborators"("Id")

		ALTER TABLE "PlayerCategories"
			ADD CONSTRAINT "FK_Users_PlayerCategories_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "PlayerCategories"
			ADD CONSTRAINT "FK_Users_PlayerCategories_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "PlayerCategories"
			ADD CONSTRAINT "FK_ProjectTypes_PlayerCategories_ProjectTypeId" FOREIGN KEY ("ProjectTypeId") REFERENCES "dbo"."ProjectTypes"("Id")

		ALTER TABLE "MusicBusinessRoundProjectPlayerCategories"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectPlayerCategories_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectPlayerCategories"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectPlayerCategories_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectPlayerCategories"
			ADD CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectPlayerCategories_MusicBusinessRoundProjectId" FOREIGN KEY ("MusicBusinessRoundProjectId") REFERENCES "MusicBusinessRoundProjects"("Id")

		ALTER TABLE "MusicBusinessRoundProjectPlayerCategories"
			ADD CONSTRAINT "FK_PlayerCategories_MusicBusinessRoundProjectPlayerCategories_PlayerCategoryId" FOREIGN KEY ("PlayerCategoryId") REFERENCES "PlayerCategories"("Id")

		ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectExpectationsForMeetings_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectExpectationsForMeetings_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings"
			ADD CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectExpectationsForMeetings_MusicBusinessRoundProjectId" FOREIGN KEY ("MusicBusinessRoundProjectId") REFERENCES "MusicBusinessRoundProjects"("Id")

		ALTER TABLE "MusicBusinessRoundProjectExpectationsForMeetings"
			ADD CONSTRAINT "FK_Languages_MusicBusinessRoundProjectExpectationsForMeetings_LanguageId" FOREIGN KEY ("LanguageId") REFERENCES "dbo"."Languages"("Id")

		ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectBuyerEvaluations_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectBuyerEvaluations_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectBuyerEvaluations_SellerUserId" FOREIGN KEY ("SellerUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectBuyerEvaluations_BuyerEvaluationUserId" FOREIGN KEY ("BuyerEvaluationUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations"
			ADD CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectBuyerEvaluations_MusicBusinessRoundProjectId" FOREIGN KEY ("MusicBusinessRoundProjectId") REFERENCES "MusicBusinessRoundProjects"("Id")

		ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations"
			ADD CONSTRAINT "FK_AttendeeOrganizations_MusicBusinessRoundProjectBuyerEvaluations_BuyerAttendeeOrganizationId" FOREIGN KEY ("BuyerAttendeeOrganizationId") REFERENCES "dbo"."AttendeeOrganizations"("Id")

		ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations"
			ADD CONSTRAINT "FK_ProjectEvaluationStatuses_MusicBusinessRoundProjectBuyerEvaluations_ProjectEvaluationStatusId" FOREIGN KEY ("ProjectEvaluationStatusId") REFERENCES "dbo"."ProjectEvaluationStatuses"("Id")

		ALTER TABLE "MusicBusinessRoundProjectBuyerEvaluations"
			ADD CONSTRAINT "FK_ProjectEvaluationRefuseReasons_MusicBusinessRoundProjectBuyerEvaluations_ProjectEvaluationRefuseReasonId" FOREIGN KEY ("ProjectEvaluationRefuseReasonId") REFERENCES "dbo"."ProjectEvaluationRefuseReasons"("Id")

	COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN --RollBack in case of Error

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