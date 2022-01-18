USE [MyRio2C_Dev]
GO

/****** Object:  Table [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives]    Script Date: 12/01/2022 13:16:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeInnovationOrganizationId] [int] NOT NULL,
	[InnovationOrganizationSustainableDevelopmentObjectiveOptionId] [int] NOT NULL,
	[AdditionalInfo] [varchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeInnovationOrganizationSustainableDevelopmentObjectives] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeInnovationOrganizations_AttendeeInnovationOrgSustDevObj_AttendeeInnovationOrganizationId] FOREIGN KEY([AttendeeInnovationOrganizationId])
REFERENCES [dbo].[AttendeeInnovationOrganizations] ([Id])
GO

ALTER TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives] CHECK CONSTRAINT [FK_AttendeeInnovationOrganizations_AttendeeInnovationOrgSustDevObj_AttendeeInnovationOrganizationId]
GO

ALTER TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeInnovationOrganizations_AttendeeInnovationOrgSustDevObj_InnovationOrgSustDevObjOptId] FOREIGN KEY([InnovationOrganizationSustainableDevelopmentObjectiveOptionId])
REFERENCES [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Id])
GO

ALTER TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives] CHECK CONSTRAINT [FK_AttendeeInnovationOrganizations_AttendeeInnovationOrgSustDevObj_InnovationOrgSustDevObjOptId]
GO

ALTER TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives] CHECK CONSTRAINT [FK_Users_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_CreateUserId]
GO

ALTER TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives] CHECK CONSTRAINT [FK_Users_AttendeeInnovationOrganizationSustainableDevelopmentObjectives_UpdateUserId]
GO


