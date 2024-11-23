BEGIN TRY
	BEGIN TRANSACTION
		CREATE TABLE dbo.ProjectModalities (
			Id int IDENTITY(1,1) NOT NULL,
			Uid uniqueidentifier NOT NULL,
			Name varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
			IsDeleted bit NOT NULL,
			CreateDate datetimeoffset NULL,
			CreateUserId int NOT NULL,
			UpdateDate datetimeoffset NULL,
			UpdateUserId int NOT NULL,
			Description varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL,
			CONSTRAINT IDX_UQ_ProjectModalities_Uid UNIQUE (Uid),
			CONSTRAINT PK_ProjectModalities PRIMARY KEY (Id)
		);
		ALTER TABLE dbo.ProjectModalities ADD CONSTRAINT FK_Users_ProjectModalities_CreateUserId FOREIGN KEY (CreateUserId) REFERENCES dbo.Users(Id);
		ALTER TABLE dbo.ProjectModalities ADD CONSTRAINT FK_Users_ProjectModalities_UpdateUserId FOREIGN KEY (UpdateUserId) REFERENCES dbo.Users(Id);
		DECLARE @PitchingId int, @BusinessRoundId int, @BothId int;
		SET @BothId = 1;
		SET @BusinessRoundId = 2;
		SET @PitchingId = 3;
		INSERT INTO dbo.ProjectModalities (Uid, Name, IsDeleted, CreateDate, CreateUserId, UpdateDate, UpdateUserId, Description)
			VALUES (N'18f04fa4-e0cb-42e5-903a-00790f31f1d5', N'Ambos | Both', 0, N'2024-10-23 04:08:23', 1, N'2024-10-23 04:08:23', 1, N'Ambos | Both'),
			(N'64cd8c96-e6ba-4bec-9332-17e6ec5a9c62', N'Somente Rodadas de Negocio | Only Business Rounds', 0, N'2024-10-23 04:08:23', 1, N'2024-10-23 04:08:23', 1, N'Somente Rodadas de Negocio | Only Business Rounds'),
			(N'88382ad8-d192-424d-bacf-1d946e15c471', N'Somente Pitching | Only Pitching', 0, N'2024-10-23 04:08:23', 1, N'2024-10-23 04:08:23', 1, N'Somente Pitching | Only Pitching');

		ALTER TABLE dbo.Projects ADD ProjectModalityId int NULL;
		ALTER TABLE dbo.Projects ADD CONSTRAINT FK_ProjectModalities_Projects_ProjectModalityId FOREIGN KEY (ProjectModalityId) REFERENCES dbo.ProjectModalities(Id);

		EXEC('UPDATE dbo.Projects SET ProjectModalityId =' + @BothId + 'WHERE IsPitching = 1');
		EXEC('UPDATE dbo.Projects SET ProjectModalityId =' + @BusinessRoundId + 'WHERE IsPitching = 0');
		EXEC('ALTER TABLE dbo.Projects ALTER COLUMN ProjectModalityId int NOT NULL');
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