--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE [dbo].[CollaboratorTypes] DROP CONSTRAINT [IDX_UQ_CollaboratorTypes_Uid]
GO

SET IDENTITY_INSERT [dbo].[CollaboratorTypes] ON 
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (110, N'4ac5a971-ba73-493b-9749-0f51bb6925b5', N'Curatorship | Audiovisual', 2, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (111, N'495b5126-0a9e-4658-ac42-8bba55818b6f', N'Curatorship | Music', 2, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (112, N'cb1a63d2-5862-4b40-8b8a-7f512e5d046d', N'Curatorship | Innovation', 2, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[CollaboratorTypes] OFF
GO

UPDATE dbo.AttendeeCollaboratorTypes SET CollaboratorTypeId = 110 WHERE CollaboratorTypeId = 101
GO

UPDATE dbo.AttendeeCollaboratorTypes SET CollaboratorTypeId = 111 WHERE CollaboratorTypeId = 102
GO

DELETE FROM dbo.CollaboratorTypes WHERE Id = 101
GO

DELETE FROM dbo.CollaboratorTypes WHERE Id = 102
GO

SET IDENTITY_INSERT [dbo].[CollaboratorTypes] ON 
GO
INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (101, N'2141f9f7-4037-423c-81bf-7ed27520489a', N'Admin | Logistic', 2, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[CollaboratorTypes] OFF
GO

ALTER TABLE [dbo].[CollaboratorTypes] ADD  CONSTRAINT [IDX_UQ_CollaboratorTypes_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)
GO