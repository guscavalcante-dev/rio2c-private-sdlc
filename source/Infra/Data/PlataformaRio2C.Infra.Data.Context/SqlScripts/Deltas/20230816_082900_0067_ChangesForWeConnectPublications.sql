--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		CREATE TABLE "WeConnectPublications"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"SocialMediaPlatformId" int  NULL ,
			"SocialMediaPlatformPublicationId" varchar(20)  NULL ,
			"PublicationText"    varchar(3000)  NULL ,
			"ImageUploadDate"    datetimeoffset  NULL ,
			"IsVideo"            bit  NOT NULL ,
			"IsFixedOnTop"       bit  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NULL 
		)

		CREATE TABLE "SocialMediaPlatforms"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"Name"               varchar(50)  NOT NULL ,
			"ApiKey"             varchar(200)  NULL ,
			"EndpointUrl"        varchar(1000)  NULL ,
			"PublicationsRootUrl" varchar(500)  NULL ,
			"IsSyncActive"       bit  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NULL 
		)

		ALTER TABLE "WeConnectPublications"
		ADD CONSTRAINT "PK_WeConnectPublications" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "SocialMediaPlatforms"
		ADD CONSTRAINT "PK_SocialMediaPlatforms" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "WeConnectPublications"
		ADD CONSTRAINT "FK_Users_WeConnectPublications_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "WeConnectPublications"
		ADD CONSTRAINT "FK_Users_WeConnectPublications_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "WeConnectPublications"
		ADD CONSTRAINT "FK_SocialMediaPlatforms_WeConnectPublications_SocialMediaPlatformId" FOREIGN KEY ("SocialMediaPlatformId") REFERENCES "SocialMediaPlatforms"("Id")

		ALTER TABLE "SocialMediaPlatforms"
		ADD CONSTRAINT "FK_Users_SocialMediaPlatforms_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "SocialMediaPlatforms"
		ADD CONSTRAINT "FK_Users_SocialMediaPlatforms_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		
		EXEC('SET IDENTITY_INSERT [dbo].[SocialMediaPlatforms] ON;
			  INSERT INTO [dbo].[SocialMediaPlatforms]
					   ([Id]
					   ,[Uid]
					   ,[Name]
					   ,[ApiKey]
					   ,[EndpointUrl]
					   ,[IsSyncActive]
					   ,[IsDeleted]
					   ,[CreateDate]
					   ,[CreateUserId]
					   ,[UpdateDate]
					   ,[UpdateUserId])
				 VALUES
					   (1, ''00000001-5A48-4A0B-A720-11AF5051F0CC'', ''Instagram'', NULL, ''https://www.instagram.com/api/v1/users/web_profile_info/?username=rio2c'', ''https://www.instagram.com/p/'' 1, 0, GETDATE(), 1, GETDATE(), 1)
			  SET IDENTITY_INSERT [dbo].[SocialMediaPlatforms] OFF;')

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