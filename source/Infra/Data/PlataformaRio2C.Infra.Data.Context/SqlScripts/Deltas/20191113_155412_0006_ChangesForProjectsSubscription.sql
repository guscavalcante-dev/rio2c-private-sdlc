--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Negotiations"
DROP CONSTRAINT FK_ProjectBuyerEvaluations_Negotiations_ProjectBuyerEvaluationId
go

DROP TABLE "dbo"."ProjectTitles"
go

DROP TABLE "dbo"."ProjectSummaries"
go

DROP TABLE "dbo"."ProjectLogLines"
go

DROP TABLE "dbo"."ProjectProductionPlans"
go

DROP TABLE "dbo"."ProjectAdditionalInformations"
go

DROP TABLE "dbo"."ProjectImageLinks"
go

DROP TABLE "dbo"."ProjectTeaserLinks"
go

DROP TABLE "dbo"."ProjectBuyerEvaluations"
go

DROP TABLE "dbo"."ProjectInterests"
go

DROP TABLE "dbo"."ProjectTargetAudiences"
go

DROP TABLE "dbo"."Projects"
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectAdditionalInformations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectAdditionalInformations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectAdditionalInformations_ProjectId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectAdditionalInformations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectBuyerEvaluations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[BuyerAttendeeOrganizationId] [int] NOT NULL,
	[ProjectEvaluationStatusId] [int] NULL,
	[Reason] [varchar](1500) NULL,
	[IsSent] [bit] NOT NULL,
	[SellerUserId] [int] NOT NULL,
	[BuyerEvaluationUserId] [int] NULL,
	[EvaluationDate] [datetime] NULL,
	[BuyerEmailSendDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectBuyerEvaluations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectBuyerEvaluations_ProjectId_BuyerAttendeeOrganizationId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[BuyerAttendeeOrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectBuyerEvaluations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectImageLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Value] [varchar](3000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectImageLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectInterests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[InterestId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectInterests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectInterests_ProjectId_InterestId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[InterestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectInterests_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectLogLines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](8000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectLogLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectLogLines_ProjectId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectLogLines_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectProductionPlans](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [nvarchar](3000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectProductionPlans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectProductionPlans_ProjectId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectProductionPlans_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[SellerAttendeeOrganizationId] [int] NOT NULL,
	[ProjectTypeId] [int] NOT NULL,
	[NumberOfEpisodes] [int] NULL,
	[EachEpisodePlayingTime] [varchar](50) NULL,
	[ValuePerEpisode] [int] NULL,
	[TotalValueOfProject] [int] NULL,
	[ValueAlreadyRaised] [int] NULL,
	[ValueStillNeeded] [int] NULL,
	[IsPitching] [bit] NOT NULL,
	[FinishDate] [datetime] NULL,
	[ProjectBuyerEvaluationGroupsCount] [int] NOT NULL,
	[ProjectBuyerEvaluationsCount] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Projects_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectSummaries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectSummaries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectSummaries_ProjectId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectSummaries_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTargetAudiences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[TargetAudienceId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectTargetAudiences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectTargetAudiences_ProjectId_TargetAudienceId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[TargetAudienceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectTargetAudiences_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectTeaserLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Value] [varchar](3000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectTeaserLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectTeaserLinks_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectTitles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectTitles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectTitles_ProjectId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectTitles_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerAttendeeOrganizations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeOrganizationId] [int] NOT NULL,
	[AttendeeCollaboratorTicketId] [int] NOT NULL,
	[ProjectsCount] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_SellerAttendeeOrganizations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_SellerAttendeeOrganizations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectAdditionalInformations]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ProjectAdditionalInformations_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations] CHECK CONSTRAINT [FK_Languages_ProjectAdditionalInformations_LanguageId]
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectAdditionalInformations_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations] CHECK CONSTRAINT [FK_Projects_ProjectAdditionalInformations_ProjectId]
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectAdditionalInformations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations] CHECK CONSTRAINT [FK_Users_ProjectAdditionalInformations_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectAdditionalInformations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations] CHECK CONSTRAINT [FK_Users_ProjectAdditionalInformations_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeOrganizations_ProjectBuyerEvaluations_BuyerAttendeeOrganizationId] FOREIGN KEY([BuyerAttendeeOrganizationId])
REFERENCES [dbo].[AttendeeOrganizations] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_AttendeeOrganizations_ProjectBuyerEvaluations_BuyerAttendeeOrganizationId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_ProjectEvaluationStatuses_ProjectBuyerEvaluations_ProjectEvaluationStatusId] FOREIGN KEY([ProjectEvaluationStatusId])
REFERENCES [dbo].[ProjectEvaluationStatuses] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_ProjectEvaluationStatuses_ProjectBuyerEvaluations_ProjectEvaluationStatusId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectBuyerEvaluations_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_Projects_ProjectBuyerEvaluations_ProjectId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectBuyerEvaluations_BuyerEvaluationUserId] FOREIGN KEY([BuyerEvaluationUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_Users_ProjectBuyerEvaluations_BuyerEvaluationUserId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectBuyerEvaluations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_Users_ProjectBuyerEvaluations_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectBuyerEvaluations_SellerUserId] FOREIGN KEY([SellerUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_Users_ProjectBuyerEvaluations_SellerUserId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectBuyerEvaluations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_Users_ProjectBuyerEvaluations_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectImageLinks]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectImageLinks_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectImageLinks] CHECK CONSTRAINT [FK_Projects_ProjectImageLinks_ProjectId]
GO
ALTER TABLE [dbo].[ProjectImageLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectImageLinks_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectImageLinks] CHECK CONSTRAINT [FK_Users_ProjectImageLinks_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectImageLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectImageLinks_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectImageLinks] CHECK CONSTRAINT [FK_Users_ProjectImageLinks_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectInterests]  WITH CHECK ADD  CONSTRAINT [FK_Interests_ProjectInterests_InterestId] FOREIGN KEY([InterestId])
REFERENCES [dbo].[Interests] ([Id])
GO
ALTER TABLE [dbo].[ProjectInterests] CHECK CONSTRAINT [FK_Interests_ProjectInterests_InterestId]
GO
ALTER TABLE [dbo].[ProjectInterests]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectInterests_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectInterests] CHECK CONSTRAINT [FK_Projects_ProjectInterests_ProjectId]
GO
ALTER TABLE [dbo].[ProjectInterests]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectInterests_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectInterests] CHECK CONSTRAINT [FK_Users_ProjectInterests_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectInterests]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectInterests_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectInterests] CHECK CONSTRAINT [FK_Users_ProjectInterests_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ProjectLogLines_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProjectLogLines] CHECK CONSTRAINT [FK_Languages_ProjectLogLines_LanguageId]
GO
ALTER TABLE [dbo].[ProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectLogLines_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectLogLines] CHECK CONSTRAINT [FK_Projects_ProjectLogLines_ProjectId]
GO
ALTER TABLE [dbo].[ProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectLogLines_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectLogLines] CHECK CONSTRAINT [FK_Users_ProjectLogLines_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectLogLines_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectLogLines] CHECK CONSTRAINT [FK_Users_ProjectLogLines_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ProjectProductionPlans_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProjectProductionPlans] CHECK CONSTRAINT [FK_Languages_ProjectProductionPlans_LanguageId]
GO
ALTER TABLE [dbo].[ProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectProductionPlans_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectProductionPlans] CHECK CONSTRAINT [FK_Projects_ProjectProductionPlans_ProjectId]
GO
ALTER TABLE [dbo].[ProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectProductionPlans_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectProductionPlans] CHECK CONSTRAINT [FK_Users_ProjectProductionPlans_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectProductionPlans_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectProductionPlans] CHECK CONSTRAINT [FK_Users_ProjectProductionPlans_UpdateUserId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypes_Projects_ProjectTypeId] FOREIGN KEY([ProjectTypeId])
REFERENCES [dbo].[ProjectTypes] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_ProjectTypes_Projects_ProjectTypeId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_SellerAttendeeOrganizations_Projects_SellerAttendeeOrganizationId] FOREIGN KEY([SellerAttendeeOrganizationId])
REFERENCES [dbo].[SellerAttendeeOrganizations] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_SellerAttendeeOrganizations_Projects_SellerAttendeeOrganizationId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Users_Projects_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Users_Projects_CreateUserId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Users_Projects_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Users_Projects_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ProjectSummaries_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProjectSummaries] CHECK CONSTRAINT [FK_Languages_ProjectSummaries_LanguageId]
GO
ALTER TABLE [dbo].[ProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectSummaries_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectSummaries] CHECK CONSTRAINT [FK_Projects_ProjectSummaries_ProjectId]
GO
ALTER TABLE [dbo].[ProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectSummaries_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectSummaries] CHECK CONSTRAINT [FK_Users_ProjectSummaries_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectSummaries_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectSummaries] CHECK CONSTRAINT [FK_Users_ProjectSummaries_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectTargetAudiences_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectTargetAudiences] CHECK CONSTRAINT [FK_Projects_ProjectTargetAudiences_ProjectId]
GO
ALTER TABLE [dbo].[ProjectTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_TargetAudiences_ProjectTargetAudiences_TargetAudienceId] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudiences] ([Id])
GO
ALTER TABLE [dbo].[ProjectTargetAudiences] CHECK CONSTRAINT [FK_TargetAudiences_ProjectTargetAudiences_TargetAudienceId]
GO
ALTER TABLE [dbo].[ProjectTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTargetAudiences_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTargetAudiences] CHECK CONSTRAINT [FK_Users_ProjectTargetAudiences_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTargetAudiences_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTargetAudiences] CHECK CONSTRAINT [FK_Users_ProjectTargetAudiences_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectTeaserLinks]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectTeaserLinks_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectTeaserLinks] CHECK CONSTRAINT [FK_Projects_ProjectTeaserLinks_ProjectId]
GO
ALTER TABLE [dbo].[ProjectTeaserLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTeaserLinks_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTeaserLinks] CHECK CONSTRAINT [FK_Users_ProjectTeaserLinks_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectTeaserLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTeaserLinks_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTeaserLinks] CHECK CONSTRAINT [FK_Users_ProjectTeaserLinks_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ProjectTitles_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProjectTitles] CHECK CONSTRAINT [FK_Languages_ProjectTitles_LanguageId]
GO
ALTER TABLE [dbo].[ProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectTitles_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectTitles] CHECK CONSTRAINT [FK_Projects_ProjectTitles_ProjectId]
GO
ALTER TABLE [dbo].[ProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTitles_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTitles] CHECK CONSTRAINT [FK_Users_ProjectTitles_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTitles_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTitles] CHECK CONSTRAINT [FK_Users_ProjectTitles_UpdateUserId]
GO
ALTER TABLE [dbo].[SellerAttendeeOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeCollaboratorTickets_SellerAttendeeOrganizations_AttendeeCollaboratorTicketId] FOREIGN KEY([AttendeeCollaboratorTicketId])
REFERENCES [dbo].[AttendeeCollaboratorTickets] ([Id])
GO
ALTER TABLE [dbo].[SellerAttendeeOrganizations] CHECK CONSTRAINT [FK_AttendeeCollaboratorTickets_SellerAttendeeOrganizations_AttendeeCollaboratorTicketId]
GO
ALTER TABLE [dbo].[SellerAttendeeOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeOrganizations_SellerAttendeeOrganizations_AttendeeOrganizationId] FOREIGN KEY([AttendeeOrganizationId])
REFERENCES [dbo].[AttendeeOrganizations] ([Id])
GO
ALTER TABLE [dbo].[SellerAttendeeOrganizations] CHECK CONSTRAINT [FK_AttendeeOrganizations_SellerAttendeeOrganizations_AttendeeOrganizationId]
GO
ALTER TABLE [dbo].[SellerAttendeeOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Users_SellerAttendeeOrganizations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SellerAttendeeOrganizations] CHECK CONSTRAINT [FK_Users_SellerAttendeeOrganizations_CreateUserId]
GO
ALTER TABLE [dbo].[SellerAttendeeOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Users_SellerAttendeeOrganizations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SellerAttendeeOrganizations] CHECK CONSTRAINT [FK_Users_SellerAttendeeOrganizations_UpdateUserId]
GO

ALTER TABLE [dbo].[Negotiations]  WITH CHECK ADD  CONSTRAINT [FK_ProjectBuyerEvaluations_Negotiations_ProjectBuyerEvaluationId] FOREIGN KEY([ProjectBuyerEvaluationId])
REFERENCES [dbo].[ProjectBuyerEvaluations] ([Id])
GO

ALTER TABLE [dbo].[Negotiations] CHECK CONSTRAINT [FK_ProjectBuyerEvaluations_Negotiations_ProjectBuyerEvaluationId]
GO

ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes]
ADD	[ProjectMaxCount] [int] NOT NULL
go

ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes]
ADD	[ProjectBuyerEvaluationGroupMaxCount] [int] NOT NULL
go

ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes]
ADD	[ProjectBuyerEvaluationMaxCount] [int] NOT NULL
go

UPDATE [dbo].[AttendeeSalesPlatformTicketTypes] SET [ProjectMaxCount] = 0, [ProjectBuyerEvaluationGroupMaxCount] = 0, [ProjectBuyerEvaluationMaxCount] = 0
go

UPDATE [dbo].[AttendeeSalesPlatformTicketTypes] SET [ProjectMaxCount] = 3, [ProjectBuyerEvaluationGroupMaxCount] = 3, [ProjectBuyerEvaluationMaxCount] = 5 WHERE [CollaboratorTypeId] = 500
go

ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes]
ALTER COLUMN [ProjectMaxCount] [int] NOT NULL
go

ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes]
ALTER COLUMN [ProjectBuyerEvaluationGroupMaxCount] [int] NOT NULL
go

ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes]
ALTER COLUMN [ProjectBuyerEvaluationMaxCount] [int] NOT NULL
go
