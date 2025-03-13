BEGIN TRY
    BEGIN TRANSACTION

        -----------------------
		-- Drop AttendeeMusicBusinessRoundNegotiationCollaborators table
		-----------------------
        ALTER TABLE [dbo].[AttendeeMusicBusinessRoundNegotiationCollaborators] DROP CONSTRAINT [FK_Users_AttendeeMusicBusinessRoundNegotiationCollaborators_UpdateUserId]
        ALTER TABLE [dbo].[AttendeeMusicBusinessRoundNegotiationCollaborators] DROP CONSTRAINT [FK_Users_AttendeeMusicBusinessRoundNegotiationCollaborators_CreateUserId]
        ALTER TABLE [dbo].[AttendeeMusicBusinessRoundNegotiationCollaborators] DROP CONSTRAINT [FK_MusicBusinessRoundNegotiations_AttendeeMusicBusinessRoundNegotiationCollaborators_MusicBusinessRoundNegotiationId]
        ALTER TABLE [dbo].[AttendeeMusicBusinessRoundNegotiationCollaborators] DROP CONSTRAINT [FK_AttendeeCollaborators_AttendeeMusicBusinessRoundNegotiationCollaborators_AttendeeCollaboratorId]

        DROP TABLE IF EXISTS [dbo].[AttendeeMusicBusinessRoundNegotiationCollaborators]

        -----------------------
		-- Drop MusicBusinessRoundNegotiations table
		-----------------------
        ALTER TABLE [dbo].[MusicBusinessRoundNegotiations] DROP CONSTRAINT [FK_Users_MusicBusinessRoundNegotiations_UpdateUserId]
        ALTER TABLE [dbo].[MusicBusinessRoundNegotiations] DROP CONSTRAINT [FK_Users_MusicBusinessRoundNegotiations_CreateUserId]
        ALTER TABLE [dbo].[MusicBusinessRoundNegotiations] DROP CONSTRAINT [FK_Rooms_MusicBusinessRoundNegotiations_RoomId]
        ALTER TABLE [dbo].[MusicBusinessRoundNegotiations] DROP CONSTRAINT [FK_MusicBusinessRoundProjectBuyerEvaluations_MusicBusinessRoundNegotiations_MusicBusinessRoundProjectBuyerEvaluationId]
        ALTER TABLE [dbo].[MusicBusinessRoundNegotiations] DROP CONSTRAINT [FK_Editions_MusicBusinessRoundNegotiations_EditionId]

        DROP TABLE IF EXISTS [dbo].[MusicBusinessRoundNegotiations]


        -----------------------
		-- Drop Editions fields
		-----------------------
        ALTER TABLE Editions DROP COLUMN MusicBusinessRoundNegotiationsCreateStartDate;
        ALTER TABLE Editions DROP COLUMN MusicBusinessRoundNegotiationsCreateEndDate;

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
