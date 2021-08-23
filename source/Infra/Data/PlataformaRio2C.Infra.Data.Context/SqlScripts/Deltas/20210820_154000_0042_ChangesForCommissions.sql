--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		execute sp_rename '"dbo".Editions."MusicProjectEvaluationStartDate"', 'MusicCommissionEvaluationStartDate', 'COLUMN'
		;
		execute sp_rename '"dbo".Editions."MusicProjectEvaluationEndDate"', 'MusicCommissionEvaluationEndDate', 'COLUMN'
		;
		execute sp_rename '"dbo".Editions."InnovationProjectEvaluationStartDate"', 'InnovationCommissionEvaluationStartDate', 'COLUMN'
		;
		execute sp_rename '"dbo".Editions."InnovationProjectEvaluationEndDate"', 'InnovationCommissionEvaluationEndDate', 'COLUMN'
		;
		execute sp_rename '"dbo".Editions."MusicProjectMinimumEvaluationsCount"', 'MusicCommissionMinimumEvaluationsCount', 'COLUMN'
		;
		execute sp_rename '"dbo".Editions."MusicProjectMaximumApprovedBandsCount"', 'MusicCommissionMaximumApprovedBandsCount', 'COLUMN'
		;
		execute sp_rename '"dbo".Editions."InnovationProjectMinimumEvaluationsCount"', 'InnovationCommissionMinimumEvaluationsCount', 'COLUMN'
		;
		execute sp_rename '"dbo".Editions."InnovationProjectMaximumApprovedCompaniesCount"', 'InnovationCommissionMaximumApprovedCompaniesCount', 'COLUMN'
		;

		-------------------------------------------------------------------------------------------------------------------------
		-- AudiovisualCommissionEvaluationStartDate
		-------------------------------------------------------------------------------------------------------------------------
		ALTER TABLE "dbo"."Editions"
		ADD AudiovisualCommissionEvaluationStartDate  datetimeoffset  NULL
		;

		EXEC('UPDATE [dbo].[Editions] SET 
				[AudiovisualCommissionEvaluationStartDate] = CAST(N''2018-01-01T03:00:00.0000000+00:00'' AS DateTimeOffset)
				WHERE Uid = ''96485F4A-56B3-4D03-8847-26D864544FAB '';
			
				UPDATE [dbo].[Editions] SET 
				[AudiovisualCommissionEvaluationStartDate] = CAST(N''2019-01-01T03:00:00.0000000+00:00'' AS DateTimeOffset)
				WHERE Uid = ''80376662-12F6-44A1-8E71-ACBE001D3DF0'';

				UPDATE [dbo].[Editions] SET 
				[AudiovisualCommissionEvaluationStartDate] = CAST(N''2020-01-06T03:00:00.0000000+00:00'' AS DateTimeOffset)
				WHERE Uid = ''88FB28B3-A952-4794-8783-88F6A08F78BE'';

				UPDATE [dbo].[Editions] SET 
				[AudiovisualCommissionEvaluationStartDate] = CAST(N''2021-08-06T03:00:00.0000000+00:00'' AS DateTimeOffset)
				WHERE Uid = ''8B410F34-9529-4CFB-8418-9C0C0D2A1BAE'';
				')

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN AudiovisualCommissionEvaluationStartDate  datetimeoffset  NOT NULL
		;

		-------------------------------------------------------------------------------------------------------------------------
		-- AudiovisualCommissionEvaluationEndDate
		-------------------------------------------------------------------------------------------------------------------------
		ALTER TABLE "dbo"."Editions"
		ADD AudiovisualCommissionEvaluationEndDate  datetimeoffset  NULL
		;

		EXEC('UPDATE [dbo].[Editions] SET 
				[AudiovisualCommissionEvaluationEndDate] = CAST(N''2018-02-16T02:59:59.0000000+00:00'' AS DateTimeOffset)
				WHERE Uid = ''96485F4A-56B3-4D03-8847-26D864544FAB ''
			
				UPDATE [dbo].[Editions] SET 
				[AudiovisualCommissionEvaluationEndDate] = CAST(N''2019-02-16T02:59:59.0000000+00:00'' AS DateTimeOffset)
				WHERE Uid = ''80376662-12F6-44A1-8E71-ACBE001D3DF0''

				UPDATE [dbo].[Editions] SET 
				[AudiovisualCommissionEvaluationEndDate] = CAST(N''2020-04-01T02:59:59.0000000+00:00'' AS DateTimeOffset)
				WHERE Uid = ''88FB28B3-A952-4794-8783-88F6A08F78BE''

				UPDATE [dbo].[Editions] SET 
				[AudiovisualCommissionEvaluationEndDate] = CAST(N''2021-09-01T02:59:59.0000000+00:00'' AS DateTimeOffset)
				WHERE Uid = ''8B410F34-9529-4CFB-8418-9C0C0D2A1BAE''
		')

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN AudiovisualCommissionEvaluationEndDate  datetimeoffset  NOT NULL
		;

		-------------------------------------------------------------------------------------------------------------------------
		-- AudiovisualCommissionMinimumEvaluationsCount
		-------------------------------------------------------------------------------------------------------------------------
		ALTER TABLE "dbo"."Editions"
		ADD AudiovisualCommissionMinimumEvaluationsCount  int  NULL
		;

		EXEC('UPDATE dbo.Editions set AudiovisualCommissionMinimumEvaluationsCount = 3')

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN AudiovisualCommissionMinimumEvaluationsCount  int  NOT NULL
		;

		-------------------------------------------------------------------------------------------------------------------------
		-- AudiovisualCommissionMaximumApprovedProjectsCount
		-------------------------------------------------------------------------------------------------------------------------
		ALTER TABLE "dbo"."Editions"
		ADD AudiovisualCommissionMaximumApprovedProjectsCount  int  NULL
		;

		EXEC('UPDATE dbo.Editions set AudiovisualCommissionMaximumApprovedProjectsCount = 3')

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN AudiovisualCommissionMaximumApprovedProjectsCount  int  NOT NULL
		;

	-- Commands End
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
