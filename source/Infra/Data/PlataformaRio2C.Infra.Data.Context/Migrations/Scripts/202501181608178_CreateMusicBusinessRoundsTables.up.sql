BEGIN TRY
	BEGIN TRANSACTION

		ALTER TABLE "dbo"."Interests"
		ADD Description  varchar(500)  NULL

		ALTER TABLE "dbo"."InterestGroups" 
		   ALTER COLUMN "Name" varchar(300)

		CREATE TABLE "MusicBusinessRoundProjectsInterests"
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

		CREATE TABLE "MusicBusinessRoundProjectsTargetAudiences"
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
			"SellerAttendeeOrganizationId" int  NULL ,
			"PlayerCategoriesThatHaveOrHadContract" varchar(300)  NULL ,
			"ExpectationsForOneToOneMeetings" varchar(max)  NULL ,
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

		CREATE TABLE "MusicBusinessRoundProjectsPlayerCategories"
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

		ALTER TABLE "MusicBusinessRoundProjectsInterests"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjects" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "MusicBusinessRoundProjectsInterests"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjects" UNIQUE ("Uid"  ASC)

		ALTER TABLE "MusicBusinessRoundProjectsTargetAudiences"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjects" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "MusicBusinessRoundProjectsTargetAudiences"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjects" UNIQUE ("Uid"  ASC)

		ALTER TABLE "MusicBusinessRoundProjects"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjects" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "MusicBusinessRoundProjects"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjects" UNIQUE ("Uid"  ASC)

		ALTER TABLE "PlayerCategories"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjects" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "PlayerCategories"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjects" UNIQUE ("Uid"  ASC)

		ALTER TABLE "MusicBusinessRoundProjectsPlayerCategories"
		ADD CONSTRAINT "PK_MusicBusinessRoundProjects" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "MusicBusinessRoundProjectsPlayerCategories"
		ADD CONSTRAINT "IDX_UQ_MusicBusinessRoundProjects" UNIQUE ("Uid"  ASC)

		ALTER TABLE "MusicBusinessRoundProjectsInterests"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectsInterests_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsInterests"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectsInterests_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsInterests"
			ADD CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectsInterests_MusicBusinessRoundProjectId" FOREIGN KEY ("MusicBusinessRoundProjectId") REFERENCES "MusicBusinessRoundProjects"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsInterests"
			ADD CONSTRAINT "FK_Interests_MusicBusinessRoundProjectsInterests_InterestId" FOREIGN KEY ("InterestId") REFERENCES "dbo"."Interests"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsTargetAudiences"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectsTargetAudiences_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsTargetAudiences"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectsTargetAudiences_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsTargetAudiences"
			ADD CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectsTargetAudiences_MusicBusinessRoundProjectId" FOREIGN KEY ("MusicBusinessRoundProjectId") REFERENCES "MusicBusinessRoundProjects"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsTargetAudiences"
			ADD CONSTRAINT "FK_TargetAudiences_MusicBusinessRoundProjectsTargetAudiences_TargetAudienceId" FOREIGN KEY ("TargetAudienceId") REFERENCES "dbo"."TargetAudiences"("Id")

		ALTER TABLE "MusicBusinessRoundProjects"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjects_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjects"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjects_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjects"
			ADD CONSTRAINT "FK_AttendeeOrganizations_MusicBusinessRoundProjects_SellerAttendeeOrganizationId" FOREIGN KEY ("SellerAttendeeOrganizationId") REFERENCES "dbo"."AttendeeOrganizations"("Id")

		ALTER TABLE "PlayerCategories"
			ADD CONSTRAINT "FK_Users_PlayerCategories_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "PlayerCategories"
			ADD CONSTRAINT "FK_Users_PlayerCategories_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "PlayerCategories"
			ADD CONSTRAINT "FK_ProjectTypes_PlayerCategories_ProjectTypeId" FOREIGN KEY ("ProjectTypeId") REFERENCES "dbo"."ProjectTypes"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsPlayerCategories"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectsPlayerCategories_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsPlayerCategories"
			ADD CONSTRAINT "FK_Users_MusicBusinessRoundProjectsPlayerCategories_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsPlayerCategories"
			ADD CONSTRAINT "FK_MusicBusinessRoundProjects_MusicBusinessRoundProjectsPlayerCategories_MusicBusinessRoundProjectId" FOREIGN KEY ("MusicBusinessRoundProjectId") REFERENCES "MusicBusinessRoundProjects"("Id")

		ALTER TABLE "MusicBusinessRoundProjectsPlayerCategories"
			ADD CONSTRAINT "FK_PlayerCategories_MusicBusinessRoundProjectsPlayerCategories_PlayerCategoryId" FOREIGN KEY ("PlayerCategoryId") REFERENCES "PlayerCategories"("Id")
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