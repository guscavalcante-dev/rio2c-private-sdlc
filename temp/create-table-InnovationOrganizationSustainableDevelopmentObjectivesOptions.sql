USE [MyRio2C_Dev]
GO

/****** Object:  Table [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions]    Script Date: 12/01/2022 13:10:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[HasAdditionalInfo] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
	[Description] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_InnovationOrganizationSustainableDevelopmentObjectivesOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_InnovationOrganizationSustainableDevelopmentObjectivesOptions_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_InnovationOrganizationSustainableDevelopmentObjectivesOptions_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] CHECK CONSTRAINT [FK_Users_InnovationOrganizationSustainableDevelopmentObjectivesOptions_CreateUserId]
GO

ALTER TABLE [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_InnovationOrganizationSustainableDevelopmentObjectivesOptions_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] CHECK CONSTRAINT [FK_Users_InnovationOrganizationSustainableDevelopmentObjectivesOptions_UpdateUserId]
GO


