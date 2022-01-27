--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		SET IDENTITY_INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] OFF 

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'16ae0720-76c0-4398-a48b-8be9f09b5bc3')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'16ae0720-76c0-4398-a48b-8be9f09b5bc3', N'Erradicação da Pobreza', 1, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Acabar com a pobreza em todas suas formas e todos os lugares.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'a459e272-7ce8-4a23-9b4d-b1efa0755d82')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'a459e272-7ce8-4a23-9b4d-b1efa0755d82', N'Fome Zero e Agricultura Sustentável', 2, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Acabar com a fome, alcançar a segurança alimentar e melhoria da nutrição e promover a agricultura sustentável')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'0149d7b1-c132-41fb-872b-5b6d906752ee')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'0149d7b1-c132-41fb-872b-5b6d906752ee', N'Saúde e Bem-Estar', 3, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Assegurar uma vida saudável e promover o bem-estar para todas e todos, em todas as idades.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'0e55777e-96dc-4a7d-b0c8-85cc2611c35e')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'0e55777e-96dc-4a7d-b0c8-85cc2611c35e', N'Educação de Qualidade', 4, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Assegurar a educação inclusiva e equitativa e de qualidade, e promover oportunidades de aprendizagem ao longo da vida para todos e todas.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'df4a9c73-ea42-4053-8070-4866c0d274be')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'df4a9c73-ea42-4053-8070-4866c0d274be', N'Igualdade de Gênero', 5, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Alcançar a igualdade de gênero e empoderar todas as mulheres e meninas.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'd61f8349-343e-4054-9ad1-9436d21fd9b1')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'd61f8349-343e-4054-9ad1-9436d21fd9b1', N'Água potável e saneamento', 6, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Assegurar a disponibilidade e gestão sustentável da água e saneamento para todos e todas.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'3e814cff-6cf7-4cd8-89b3-3196a13e3ef8')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'3e814cff-6cf7-4cd8-89b3-3196a13e3ef8', N'Energia Limpa e Acessível', 7, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Assegurar o acesso confiável, sustentável, moderno e a preço acessível à energia para todos e todas.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'78974f4a-9858-44ab-94bb-143ee8c9d69c')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'78974f4a-9858-44ab-94bb-143ee8c9d69c', N'Trabalho descente e crescimento econômico', 8, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Promover o crescimento econômico sustentado, inclusivo e sustentável, emprego pleno e produtivo e trabalho decente para todos e todas.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'4cc4c21d-e5e6-468d-9420-2af0b839d2ed')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'4cc4c21d-e5e6-468d-9420-2af0b839d2ed', N'Indústria, Inovação e Infraestrutura', 9, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Construir infraestruturas resilientes, promover a industrialização inclusiva e sustentável e fomentar a inovação.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'426262ed-cd36-4189-94bc-9cca674a4ea6')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'426262ed-cd36-4189-94bc-9cca674a4ea6', N'Redução das desigualdades', 10, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Reduzir as desigualdades dentro dos países e entre eles.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'50612a8d-849c-42f3-8c9b-ccf371d3ac33')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'50612a8d-849c-42f3-8c9b-ccf371d3ac33', N'Cidades e Comunidades Sustentáveis', 11, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Tornar as cidades e assentamentos humanos inclusivos, seguros, resilientes e sustentáveis.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'14ab7a46-3138-4f02-a97d-8d8f4f5888c3')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'14ab7a46-3138-4f02-a97d-8d8f4f5888c3', N'Consumo e Produção Responsáveis', 12, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Assegurar padrões de produção e de consumo sustentáveis.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'a7d10242-eec3-4381-9ea5-9cd79bc0adf4')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'a7d10242-eec3-4381-9ea5-9cd79bc0adf4', N'Ação contra a mudança global do clima', 13, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Tomar medidas urgentes par combater a mudança do clima e seus impactos.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'414dfa3d-aa2b-4fa7-8b3f-cff94ef969b5')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'414dfa3d-aa2b-4fa7-8b3f-cff94ef969b5', N'Vida na água', 14, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Conservação e uso sustentável dos oceanos, dos mares, e dos recursos marinhos para o desenvolvimento sustentável.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'618898df-e029-4294-b707-bb373b480002')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'618898df-e029-4294-b707-bb373b480002', N'Vida Terrestre', 15, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Proteger, recuperar e promover o uso sustentável dos ecossistemas terrestres, gerir de forma sustentável as florestas, combater a desertificação, deter e reverter a degradação da terra e deter a perda da biodiversidade.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'f6452139-6845-48b7-8925-efadff8f425e')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'f6452139-6845-48b7-8925-efadff8f425e', N'Paz, Justiça e Instituições eficazes', 16, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Promover sociedades pacíficas e inclusivas para o desenvolvimento sustentável, proporcionar o acesso à justiça para todas e todos e construir instituições eficazes, responsáveis, inclusivas em todos os níveis.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'6e11b3a0-db0b-407b-aef3-ff601c42d566')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'6e11b3a0-db0b-407b-aef3-ff601c42d566', N'Parcerias e Meios de Implementação', 17, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Fortalecer os meios de implementação e revitalizar a parceria global para o desenvolvimento sustentável.')
		END

		SET IDENTITY_INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ON

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