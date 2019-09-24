--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

SET IDENTITY_INSERT [dbo].[Roles] ON 
go
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (3, N'Admin | Audiovisual')
go
SET IDENTITY_INSERT [dbo].[Roles] OFF
go
