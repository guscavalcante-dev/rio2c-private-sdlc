BEGIN TRY
    BEGIN TRANSACTION

		-----------------------
		-- Create MusicBusinessRoundNegotiations table
		-----------------------
		CREATE TABLE [dbo].[MusicBusinessRoundNegotiations](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[MusicBusinessRoundProjectBuyerEvaluationId] [int] NOT NULL,
			[RoomId] [int] NOT NULL,
			[StartDate] [datetimeoffset](7) NULL,
			[EndDate] [datetimeoffset](7)  NULL,
			[TableNumber] [int] NOT NULL,
			[RoundNumber] [int] NOT NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
			[IsAutomatic] [bit] NOT NULL,
			[EditionId] [int] NOT NULL,
			CONSTRAINT [PK_MusicBusinessRoundNegotiations] PRIMARY KEY CLUSTERED ([Id] ASC),
			CONSTRAINT [IDX_UQ_MusicBusinessRoundNegotiations_Uid] UNIQUE NONCLUSTERED ([Uid] ASC)
		) ON [PRIMARY]

		ALTER TABLE [dbo].[MusicBusinessRoundNegotiations] ADD CONSTRAINT [FK_Editions_MusicBusinessRoundNegotiations_EditionId] FOREIGN KEY([EditionId])
		REFERENCES [dbo].[Editions] ([Id])

		ALTER TABLE [dbo].[MusicBusinessRoundNegotiations] ADD CONSTRAINT [FK_MusicBusinessRoundProjectBuyerEvaluations_MusicBusinessRoundNegotiations_MusicBusinessRoundProjectBuyerEvaluationId] FOREIGN KEY([MusicBusinessRoundProjectBuyerEvaluationId])
		REFERENCES [dbo].[MusicBusinessRoundProjectBuyerEvaluations] ([Id])

		ALTER TABLE [dbo].[MusicBusinessRoundNegotiations] ADD CONSTRAINT [FK_Rooms_MusicBusinessRoundNegotiations_RoomId] FOREIGN KEY([RoomId])
		REFERENCES [dbo].[Rooms] ([Id])

		ALTER TABLE [dbo].[MusicBusinessRoundNegotiations] ADD CONSTRAINT [FK_Users_MusicBusinessRoundNegotiations_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[MusicBusinessRoundNegotiations] ADD CONSTRAINT [FK_Users_MusicBusinessRoundNegotiations_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])


		-----------------------
		-- Create AttendeeMusicBusinessRoundNegotiationCollaborators table
		-----------------------
		CREATE TABLE [dbo].[AttendeeMusicBusinessRoundNegotiationCollaborators](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[MusicBusinessRoundNegotiationId] [int] NOT NULL,
			[AttendeeCollaboratorId] [int] NOT NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NOT NULL,
			[UpdateDate] [datetimeoffset](7) NOT NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_AttendeeMusicBusinessRoundNegotiationCollaborators] PRIMARY KEY CLUSTERED ([Id] ASC),
		 CONSTRAINT [IDX_UQ_AttendeeMusicBusinessRoundNegotiationCollaborators_Uid] UNIQUE NONCLUSTERED ([Uid] ASC),
		 CONSTRAINT [IDX_UQ_AttendeeMusicBusinessRoundNegotiationCollaborators_AttendeeCollaboratorId_NegotiationId] UNIQUE NONCLUSTERED ([AttendeeCollaboratorId], [MusicBusinessRoundNegotiationId])
		) ON [PRIMARY]

		ALTER TABLE [dbo].[AttendeeMusicBusinessRoundNegotiationCollaborators] ADD CONSTRAINT [FK_AttendeeCollaborators_AttendeeMusicBusinessRoundNegotiationCollaborators_AttendeeCollaboratorId] FOREIGN KEY([AttendeeCollaboratorId])
		REFERENCES [dbo].[AttendeeCollaborators] ([Id])

		ALTER TABLE [dbo].[AttendeeMusicBusinessRoundNegotiationCollaborators] ADD CONSTRAINT [FK_MusicBusinessRoundNegotiations_AttendeeMusicBusinessRoundNegotiationCollaborators_MusicBusinessRoundNegotiationId] FOREIGN KEY([MusicBusinessRoundNegotiationId])
		REFERENCES [dbo].[MusicBusinessRoundNegotiations] ([Id])

		ALTER TABLE [dbo].[AttendeeMusicBusinessRoundNegotiationCollaborators] ADD CONSTRAINT [FK_Users_AttendeeMusicBusinessRoundNegotiationCollaborators_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[AttendeeMusicBusinessRoundNegotiationCollaborators] ADD CONSTRAINT [FK_Users_AttendeeMusicBusinessRoundNegotiationCollaborators_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])


		-----------------------
		-- Alter Editions table
		-----------------------
		ALTER TABLE Editions ADD MusicBusinessRoundNegotiationsCreateStartDate datetimeoffset(7)
		ALTER TABLE Editions ADD MusicBusinessRoundNegotiationsCreateEndDate datetimeoffset(7)

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
