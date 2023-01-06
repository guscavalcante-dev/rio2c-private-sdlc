--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done
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

		
		EXEC('SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptionGroups] ON;
			  INSERT INTO [dbo].[InnovationOrganizationTrackOptionGroups]
					   ([Id]
					   ,[Uid]
					   ,[Name]
					   ,[DisplayOrder]
					   ,[IsActive]
					   ,[IsDeleted]
					   ,[CreateDate]
					   ,[CreateUserId]
					   ,[UpdateDate]
					   ,[UpdateUserId])
				 VALUES
					   (1, ''3B475635-B2CF-4EF8-9294-AD8739763825'', ''Consumo'',					1, 1, 0, GETDATE(), 1, GETDATE(), 1),
					   (2, ''F2B33ABA-EE09-4C2B-85FD-9BEB2A2C2DC5'', ''Entretenimento e Lazer'',	2, 1, 0, GETDATE(), 1, GETDATE(), 1),
					   (3, ''50EC23C7-B19D-40E3-B046-572EA56DD709'', ''Educação e Trabalho'',		3, 1, 0, GETDATE(), 1, GETDATE(), 1),
					   (4, ''C4286A42-678F-4A17-9888-99CE0A271DCA'', ''Impacto Positivo'',			4, 1, 0, GETDATE(), 1, GETDATE(), 1);
			  SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptionGroups] OFF;')
		

		
		EXEC('SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptions] ON;
			  INSERT INTO [dbo].[InnovationOrganizationTrackOptions]
			  	   ([Id]
			  	   ,[Uid]
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
			  	   (18, ''FF881E76-110A-4940-AD61-A31C2794FB68'', ''Mídia'',			1, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
			  	   (19, ''1E7A74CD-DB94-4D55-8FB5-8613F5FC964B'', ''Publicidade'',		2, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
			  	   (20, ''E0A67623-D4B1-4E2B-A604-0D2778095A9D'', ''Arquitetura'',		3, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
			  	   (21, ''0EC073F0-18B1-41AA-8E31-DC33B3F1D842'', ''Design'',			4, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
			  	   (22, ''7E7274B9-9598-4722-B9F9-9AD6BE361E31'', ''Moda'',				5, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
			  	   (23, ''C1E6EE24-FAEB-4743-BCF2-43E11B4E42D6'', ''Esporte'',			6, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 1, 1),
		 
			  	   (24, ''C96DBF09-CB00-4B72-84A0-E55067E1CD59'', ''Audiovisual'',		1, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 2, 1),
			  	   (25, ''6DEED5C5-4AEF-4467-9BCA-A203F6F7580D'', ''Editorial'',		2, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 2, 1),
			  	   (26, ''2237135F-F1F6-4F71-AA9C-6632F6CB64AC'', ''Música'',			3, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 2, 1),
			  	   (27, ''54B251C3-4578-4A2D-A629-F450EEECD561'', ''Games'',			4, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 2, 1),
			  	   (28, ''C159F683-77DE-46A3-AA88-0EF56B0F6525'', ''Turismo'',			5, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 2, 1),

			  	   (29, ''15F011A7-D709-4CE7-9C14-8264E76AEA3B'', ''Educação - Edtechs'', 1, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 3, 1),
			  	   (30, ''559FF6C1-F635-4977-A5F3-B00CA8ED968E'', ''Trabalho – Hrtechs'', 2, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 3, 1),

			  	   (31, ''F4E50F8C-BB24-4C1F-8F3E-EB65F25F372D'', ''ESG'',									1, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 4, 1),
			  	   (32, ''30AF3491-5FF7-4188-8472-7A219EB8880E'', ''Negócios desenvolvidos em Favelas'',	2, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '''', 4, 1);
			  SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptions] OFF;
					   ')

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