--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		SET IDENTITY_INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] OFF 

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'16ae0720-76c0-4398-a48b-8be9f09b5bc3')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'16ae0720-76c0-4398-a48b-8be9f09b5bc3', N'Erradica��o da Pobreza', 1, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Acabar com a pobreza em todas suas formas e todos os lugares.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'a459e272-7ce8-4a23-9b4d-b1efa0755d82')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'a459e272-7ce8-4a23-9b4d-b1efa0755d82', N'Fome Zero e Agricultura Sustent�vel', 2, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Acabar com a fome, alcan�ar a seguran�a alimentar e melhoria da nutri��o e promover a agricultura sustent�vel')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'0149d7b1-c132-41fb-872b-5b6d906752ee')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'0149d7b1-c132-41fb-872b-5b6d906752ee', N'Sa�de e Bem-Estar', 3, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Assegurar uma vida saud�vel e promover o bem-estar para todas e todos, em todas as idades.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'0e55777e-96dc-4a7d-b0c8-85cc2611c35e')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'0e55777e-96dc-4a7d-b0c8-85cc2611c35e', N'Educa��o de Qualidade', 4, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Assegurar a educa��o inclusiva e equitativa e de qualidade, e promover oportunidades de aprendizagem ao longo da vida para todos e todas.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'df4a9c73-ea42-4053-8070-4866c0d274be')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'df4a9c73-ea42-4053-8070-4866c0d274be', N'Igualdade de G�nero', 5, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Alcan�ar a igualdade de g�nero e empoderar todas as mulheres e meninas.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'd61f8349-343e-4054-9ad1-9436d21fd9b1')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'd61f8349-343e-4054-9ad1-9436d21fd9b1', N'�gua pot�vel e saneamento', 6, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Assegurar a disponibilidade e gest�o sustent�vel da �gua e saneamento para todos e todas.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'3e814cff-6cf7-4cd8-89b3-3196a13e3ef8')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'3e814cff-6cf7-4cd8-89b3-3196a13e3ef8', N'Energia Limpa e Acess�vel', 7, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Assegurar o acesso confi�vel, sustent�vel, moderno e a pre�o acess�vel � energia para todos e todas.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'78974f4a-9858-44ab-94bb-143ee8c9d69c')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'78974f4a-9858-44ab-94bb-143ee8c9d69c', N'Trabalho descente e crescimento econ�mico', 8, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Promover o crescimento econ�mico sustentado, inclusivo e sustent�vel, emprego pleno e produtivo e trabalho decente para todos e todas.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'4cc4c21d-e5e6-468d-9420-2af0b839d2ed')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'4cc4c21d-e5e6-468d-9420-2af0b839d2ed', N'Ind�stria, Inova��o e Infraestrutura', 9, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Construir infraestruturas resilientes, promover a industrializa��o inclusiva e sustent�vel e fomentar a inova��o.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'426262ed-cd36-4189-94bc-9cca674a4ea6')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'426262ed-cd36-4189-94bc-9cca674a4ea6', N'Redu��o das desigualdades', 10, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Reduzir as desigualdades dentro dos pa�ses e entre eles.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'50612a8d-849c-42f3-8c9b-ccf371d3ac33')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'50612a8d-849c-42f3-8c9b-ccf371d3ac33', N'Cidades e Comunidades Sustent�veis', 11, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Tornar as cidades e assentamentos humanos inclusivos, seguros, resilientes e sustent�veis.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'14ab7a46-3138-4f02-a97d-8d8f4f5888c3')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'14ab7a46-3138-4f02-a97d-8d8f4f5888c3', N'Consumo e Produ��o Respons�veis', 12, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Assegurar padr�es de produ��o e de consumo sustent�veis.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'a7d10242-eec3-4381-9ea5-9cd79bc0adf4')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'a7d10242-eec3-4381-9ea5-9cd79bc0adf4', N'A��o contra a mudan�a global do clima', 13, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Tomar medidas urgentes par combater a mudan�a do clima e seus impactos.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'414dfa3d-aa2b-4fa7-8b3f-cff94ef969b5')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'414dfa3d-aa2b-4fa7-8b3f-cff94ef969b5', N'Vida na �gua', 14, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Conserva��o e uso sustent�vel dos oceanos, dos mares, e dos recursos marinhos para o desenvolvimento sustent�vel.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'618898df-e029-4294-b707-bb373b480002')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'618898df-e029-4294-b707-bb373b480002', N'Vida Terrestre', 15, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Proteger, recuperar e promover o uso sustent�vel dos ecossistemas terrestres, gerir de forma sustent�vel as florestas, combater a desertifica��o, deter e reverter a degrada��o da terra e deter a perda da biodiversidade.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'f6452139-6845-48b7-8925-efadff8f425e')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'f6452139-6845-48b7-8925-efadff8f425e', N'Paz, Justi�a e Institui��es eficazes', 16, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Promover sociedades pac�ficas e inclusivas para o desenvolvimento sustent�vel, proporcionar o acesso � justi�a para todas e todos e construir institui��es eficazes, respons�veis, inclusivas em todos os n�veis.')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] WHERE Uid = N'6e11b3a0-db0b-407b-aef3-ff601c42d566')
		BEGIN
		INSERT [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'6e11b3a0-db0b-407b-aef3-ff601c42d566', N'Parcerias e Meios de Implementa��o', 17, 0, 0, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-12T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'Fortalecer os meios de implementa��o e revitalizar a parceria global para o desenvolvimento sustent�vel.')
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