--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		ALTER TABLE "InnovationOrganizationTrackOptions"
			ADD InnovationOrganizationTrackOptionGroupId int NULL

		ALTER TABLE "InnovationOrganizationTrackOptions"
			ADD IsActive bit NULL

		EXEC('Update InnovationOrganizationTrackOptions set IsActive = 0')

		ALTER TABLE "InnovationOrganizationTrackOptions"
			ALTER COLUMN IsActive bit NOT NULL

		CREATE TABLE "InnovationOrganizationTrackOptionGroups"
		( 
			"Id"                 int IDENTITY ( 1,1 ),
			"Uid"                uniqueidentifier  NOT NULL,
			"Name"               varchar(100)  NOT NULL,
			"DisplayOrder"       int  NOT NULL,
			"IsActive"           bit  NOT NULL,
			"IsDeleted"          bit  NOT NULL,
			"CreateDate"         datetimeoffset  NOT NULL,
			"CreateUserId"       int  NOT NULL,
			"UpdateDate"         datetimeoffset  NOT NULL,
			"UpdateUserId"       int  NOT NULL
		)

		ALTER TABLE "InnovationOrganizationTrackOptionGroups"
			ADD CONSTRAINT "PK_InnovationOrganizationOptionGroup" PRIMARY KEY  CLUSTERED ("Id" ASC)

		ALTER TABLE "InnovationOrganizationTrackOptionGroups"
			ADD CONSTRAINT "IDX_UQ_InnovationOrganizationOptionGroup" UNIQUE ("Uid"  ASC)

		ALTER TABLE "InnovationOrganizationTrackOptionGroups"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationTrackOptionGroups_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "InnovationOrganizationTrackOptionGroups"
			ADD CONSTRAINT "FK_Users_InnovationOrganizationTrackOptionGroups_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")

		ALTER TABLE "InnovationOrganizationTrackOptions"
			ADD CONSTRAINT "FK_InnovationOrganizationTrackOptionGroups_InnovationOrganizationTrackOptions_InnovationOrganizationTrackOptionGroupId" FOREIGN KEY ("InnovationOrganizationTrackOptionGroupId") REFERENCES "InnovationOrganizationTrackOptionGroups"("Id")

		EXEC('INSERT INTO [dbo].[InnovationOrganizationTrackOptionGroups]
					   ([Uid]
					   ,[Name]
					   ,[DisplayOrder]
					   ,[IsActive]
					   ,[IsDeleted]
					   ,[CreateDate]
					   ,[CreateUserId]
					   ,[UpdateDate]
					   ,[UpdateUserId])
				 VALUES
					   (NEWID(), ''Consumo'',					1, 1, 0, GETDATE(), 1, GETDATE(), 1),
					   (NEWID(), ''Entretenimento e Lazer'',	2, 1, 0, GETDATE(), 1, GETDATE(), 1),
					   (NEWID(), ''Educação e Trabalho'',		3, 1, 0, GETDATE(), 1, GETDATE(), 1),
					   (NEWID(), ''Impacto Positivo'',			4, 1, 0, GETDATE(), 1, GETDATE(), 1)')

		EXEC('INSERT INTO [dbo].[InnovationOrganizationTrackOptions]
					   ([Uid]
					   ,[Name]
					   ,[DisplayOrder]
					   ,[HasAdditionalInfo]
					   ,[IsDeleted]
					   ,[CreateDate]
					   ,[CreateUserId]
					   ,[UpdateDate]
					   ,[UpdateUserId]
					   ,[Description]
					   ,[InnovationOrganizationTrackOptionGroupId]
					   ,[IsActive])
				 VALUES
					   (NEWID(), ''Mídia'',			1, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
					   (NEWID(), ''Publicidade'',	2, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
					   (NEWID(), ''Arquitetura'',	3, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
					   (NEWID(), ''Design'',		4, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
					   (NEWID(), ''Moda'',			5, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
					   (NEWID(), ''Esporte'',		6, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
		   
					   (NEWID(), ''Audiovisual'',	1, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 2, 1),
					   (NEWID(), ''Editorial'',		2, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 2, 1),
					   (NEWID(), ''Música'',		3, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 2, 1),
					   (NEWID(), ''Games'',			4, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 2, 1),
					   (NEWID(), ''Turismo'',		5, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 2, 1),

					   (NEWID(), ''Educação - Edtechs'', 1, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 3, 1),
					   (NEWID(), ''Trabalho – Hrtechs'', 2, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 3, 1),

					   (NEWID(), ''ESG'',								1, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 4, 1),
					   (NEWID(), ''Negócios desenvolvidos em Favelas'',	2, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 4, 1)')

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