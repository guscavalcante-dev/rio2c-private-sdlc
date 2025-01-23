BEGIN TRY
	BEGIN TRANSACTION
		-----------------------------------------------------
		-- Delete MusicBands from deleted MusicProjects
		-----------------------------------------------------
		update MusicBands set IsDeleted = 1 where id in (
			select mb.id
			from MusicProjects mp
			left join AttendeeMusicBands amb on amb.Id = mp.AttendeeMusicBandId
			left join MusicBands mb on mb.Id = amb.MusicBandId
			where mp.IsDeleted = 1
		)
		-----------------------------------------------------
		-- Delete AttendeeMusicBands from deleted MusicProjects
		-----------------------------------------------------
		update AttendeeMusicBands set IsDeleted = 1 where id in (
			select amb.id
			from MusicProjects mp
			left join AttendeeMusicBands amb on amb.Id = mp.AttendeeMusicBandId
			left join MusicBands mb on mb.Id = amb.MusicBandId
			where mp.IsDeleted = 1
		)
		-----------------------------------------------------
		-- Delete AttendeeMusicBandEvaluations from deleted MusicProjects
		-----------------------------------------------------
		update AttendeeMusicBandEvaluations set IsDeleted = 1 where AttendeeMusicBandId in (
			select amb.id
			from MusicProjects mp
			left join AttendeeMusicBands amb on amb.Id = mp.AttendeeMusicBandId
			left join MusicBands mb on mb.Id = amb.MusicBandId
			where mp.IsDeleted = 1
		)
		-----------------------------------------------------
		-- Delete AttendeeMusicBandCollaborators from deleted MusicProjects
		-----------------------------------------------------
		update AttendeeMusicBandCollaborators set IsDeleted = 1 where AttendeeMusicBandId in (
			select amb.id
			from MusicProjects mp
			left join AttendeeMusicBands amb on amb.Id = mp.AttendeeMusicBandId
			left join MusicBands mb on mb.Id = amb.MusicBandId
			where mp.IsDeleted = 1
		)

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
