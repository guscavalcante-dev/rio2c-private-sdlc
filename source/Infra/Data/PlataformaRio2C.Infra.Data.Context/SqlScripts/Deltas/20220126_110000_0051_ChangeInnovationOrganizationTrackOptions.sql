--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		UPDATE InnovationOrganizationTrackOptions SET isDeleted = 1 WHERE Id > 0 AND Id <= 11;

		SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptions] OFF

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid = N'eaad698c-4357-4546-9096-6680fc9739c6')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'eaad698c-4357-4546-9096-6680fc9739c6', N'Game e Sports', 1, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'(Tecnologias que auxiliam na sa�de, condicionamento, performance e bem-estar/estilo de vida dos atletas; Empreendedorismo e neg�cios do eSport e games; VR/AR Marcas, Patroc�nio, propaganda no esporte; Monitoramento e tecnologias de dados, gadgets de performance, Telemedicina; Intelig�ncia Artificial (IA), Internet das Coisas (IoT), Direitos de Transmiss�o; Streaming e canais de comunica��o; Tendencias e perspectivas para a ind�stria de game e eSports; Neuroci�ncia � performance e intelig�ncia emocional; Fan Experience)')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid = N'7e157c72-bad2-45cf-85dd-8d3f6da2c2a3')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'7e157c72-bad2-45cf-85dd-8d3f6da2c2a3', N'Trabalho e Educa��o', 2, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'EDTECH  (Objeto Digital de Aprendizagem (ODA), Jogo educativo, Curso online, Ferramenta de apoio � gest�o administrativo-financeira, Ferramenta de apoio � gest�o pedag�gica, Ferramenta de avalia��o do estudante, Ferramenta gerenciadora de curr�culo, Ferramenta de autoria, Ferramenta de apoio � aula, Ferramenta de colabora��o, Ferramenta de tutoria, Sistema de gest�o educacional (SIG | SIS), Sistema gerenciador de sala de aula, Ambiente virtual de aprendizagem (AVA), Plataforma educacional, Plataforma educacional adaptativa, Plataforma de oferta de conte�do online, Reposit�rio digital, Ferramenta maker, Hardware educacional, Outros produtos ou servi�os) HRTECH (Solu��es para o desenvolvimento do capital humano: Banco de Talentos, Gest�o de times e processos, Desenvolvimento de Skills, Engajamento e performance, Processo Seletivo, Software de HR, Benef�cios, Gest�o de Ponto, Qualidade de Vida, Contrata��o Flex�vel, Diversidade e Inclus�o, On/Offboarding) ')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid = N'29ecd329-b74b-48d3-9dc8-5b91ee00fe03')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'29ecd329-b74b-48d3-9dc8-5b91ee00fe03', N'Marketing, M�dias e outras hist�rias', 3, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'ADTECH e MARTECH (Advertising e Promotion: Search & display, OOH (Out Of Home) , Geomarketing, M�dia Program�tica, An�lise e desempenho de Marketing, Design. Conte�do & Experi�ncia: Ferramenta de E-Mail & SMS Marketing , Automa��o de Marketing e Gest�o de Campanha, Conte�do de Marketing, Marketing Sensorial , SEO, storyteeling, brandend game, brandend content; Comercio e vendas: E-commerce Marketing & Tools, Market Research & Survey , Loyalty Programs, Big Data & Business Intelligence. Social & Relacionamento:  Events, Meetings & Webinars, Customer Relationship , Influencers & Buzz , Social Media Management & Monitoring)')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid = N'8bfce3bb-5f6e-4776-86fe-48e074a548cf')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'8bfce3bb-5f6e-4776-86fe-48e074a548cf', N'Ci�ncia e Tecnologia', 4, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'DEEPTECH E CLEANTECH (Intelig�ncia Artificial, Internet das Coisas (IoT), Rob�tica e drones, Biotecnologia, Materiais avan�ados (Desenvolvimento e modifica��o de materiais de base biol�gica ou sint�tica de modo a obter um desempenho superior ao dos materiais convencionais), AR/VR, Blockchain, Fot�nica e eletr�nica, Computa��o qu�ntica, Energia renov�vel, Tecnologia limpa, sustentabilidade social e ambiental;')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid =N'f79d91fc-1d48-419c-82c3-6ddcdf32d79d')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'f79d91fc-1d48-419c-82c3-6ddcdf32d79d', N'Moda Gastronomia e Lifestyle', 5, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'FOODTECH (Internet das Coisas (IoT), o big data e a Intelig�ncia Artificial (IA), entre outras, para transformar a ind�stria agroalimentar num setor mais moderno, sustent�vel e eficiente em todas as suas etapas, que v�o desde a elabora��o dos alimentos at� a distribui��o e o consumo.) RETAILTECH (Ambientes virtuais; E-commerce; Engajamento do Consumidor; Intelig�ncia artificial (AI); Internet das coisas (IoT); Log�stica; Opera��es; Pagamentos.) HEALTHCARE (Educa��o da Sa�de; Relacionamento com Pacientes; MedTech; Farmac�utica; Gest�o; Wearable; AI e Big Data; Reconhecimento de imagens, facial e voz; Monitoramento de Sensores; Realidade Virtual; Nanotecnologia; Rob�tica.)')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid = N'7d5da1ae-e347-4cdc-98c8-06e84665193d')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'7d5da1ae-e347-4cdc-98c8-06e84665193d', N'Arquitetura, Design e Artes', 6, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'SMARTCITIES e FAVELA 2.0 (Infraestrutura urbana, Gest�o de res�duos, Mobilidade, Seguran�a, Solu��es, ecol�gicas, Qualidade de vida urbana, Opera��es Municipais, Planejamento e gest�o urbana)')
		END

		SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptions] ON
	-- Commands End
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