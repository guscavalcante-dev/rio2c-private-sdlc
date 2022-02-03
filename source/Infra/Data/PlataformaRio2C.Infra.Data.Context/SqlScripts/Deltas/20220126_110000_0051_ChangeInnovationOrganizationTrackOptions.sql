--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		UPDATE InnovationOrganizationTrackOptions SET isDeleted = 1 WHERE Id > 0 AND Id <= 11;

		SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptions] OFF

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid = N'eaad698c-4357-4546-9096-6680fc9739c6')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'eaad698c-4357-4546-9096-6680fc9739c6', N'Game e Sports', 1, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'(Tecnologias que auxiliam na saúde, condicionamento, performance e bem-estar/estilo de vida dos atletas; Empreendedorismo e negócios do eSport e games; VR/AR Marcas, Patrocínio, propaganda no esporte; Monitoramento e tecnologias de dados, gadgets de performance, Telemedicina; Inteligência Artificial (IA), Internet das Coisas (IoT), Direitos de Transmissão; Streaming e canais de comunicação; Tendencias e perspectivas para a indústria de game e eSports; Neurociência – performance e inteligência emocional; Fan Experience)')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid = N'7e157c72-bad2-45cf-85dd-8d3f6da2c2a3')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'7e157c72-bad2-45cf-85dd-8d3f6da2c2a3', N'Trabalho e Educação', 2, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'EDTECH  (Objeto Digital de Aprendizagem (ODA), Jogo educativo, Curso online, Ferramenta de apoio à gestão administrativo-financeira, Ferramenta de apoio à gestão pedagógica, Ferramenta de avaliação do estudante, Ferramenta gerenciadora de currículo, Ferramenta de autoria, Ferramenta de apoio à aula, Ferramenta de colaboração, Ferramenta de tutoria, Sistema de gestão educacional (SIG | SIS), Sistema gerenciador de sala de aula, Ambiente virtual de aprendizagem (AVA), Plataforma educacional, Plataforma educacional adaptativa, Plataforma de oferta de conteúdo online, Repositório digital, Ferramenta maker, Hardware educacional, Outros produtos ou serviços) HRTECH (Soluções para o desenvolvimento do capital humano: Banco de Talentos, Gestão de times e processos, Desenvolvimento de Skills, Engajamento e performance, Processo Seletivo, Software de HR, Benefícios, Gestão de Ponto, Qualidade de Vida, Contratação Flexível, Diversidade e Inclusão, On/Offboarding) ')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid = N'29ecd329-b74b-48d3-9dc8-5b91ee00fe03')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'29ecd329-b74b-48d3-9dc8-5b91ee00fe03', N'Marketing, Mídias e outras histórias', 3, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'ADTECH e MARTECH (Advertising e Promotion: Search & display, OOH (Out Of Home) , Geomarketing, Mídia Programática, Análise e desempenho de Marketing, Design. Conteúdo & Experiência: Ferramenta de E-Mail & SMS Marketing , Automação de Marketing e Gestão de Campanha, Conteúdo de Marketing, Marketing Sensorial , SEO, storyteeling, brandend game, brandend content; Comercio e vendas: E-commerce Marketing & Tools, Market Research & Survey , Loyalty Programs, Big Data & Business Intelligence. Social & Relacionamento:  Events, Meetings & Webinars, Customer Relationship , Influencers & Buzz , Social Media Management & Monitoring)')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid = N'8bfce3bb-5f6e-4776-86fe-48e074a548cf')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'8bfce3bb-5f6e-4776-86fe-48e074a548cf', N'Ciência e Tecnologia', 4, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'DEEPTECH E CLEANTECH (Inteligência Artificial, Internet das Coisas (IoT), Robótica e drones, Biotecnologia, Materiais avançados (Desenvolvimento e modificação de materiais de base biológica ou sintética de modo a obter um desempenho superior ao dos materiais convencionais), AR/VR, Blockchain, Fotônica e eletrônica, Computação quântica, Energia renovável, Tecnologia limpa, sustentabilidade social e ambiental;')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid =N'f79d91fc-1d48-419c-82c3-6ddcdf32d79d')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'f79d91fc-1d48-419c-82c3-6ddcdf32d79d', N'Moda Gastronomia e Lifestyle', 5, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'FOODTECH (Internet das Coisas (IoT), o big data e a Inteligência Artificial (IA), entre outras, para transformar a indústria agroalimentar num setor mais moderno, sustentável e eficiente em todas as suas etapas, que vão desde a elaboração dos alimentos até a distribuição e o consumo.) RETAILTECH (Ambientes virtuais; E-commerce; Engajamento do Consumidor; Inteligência artificial (AI); Internet das coisas (IoT); Logística; Operações; Pagamentos.) HEALTHCARE (Educação da Saúde; Relacionamento com Pacientes; MedTech; Farmacêutica; Gestão; Wearable; AI e Big Data; Reconhecimento de imagens, facial e voz; Monitoramento de Sensores; Realidade Virtual; Nanotecnologia; Robótica.)')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[InnovationOrganizationTrackOptions] WHERE Uid = N'7d5da1ae-e347-4cdc-98c8-06e84665193d')
		BEGIN
		INSERT [dbo].[InnovationOrganizationTrackOptions] ([Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES (N'7d5da1ae-e347-4cdc-98c8-06e84665193d', N'Arquitetura, Design e Artes', 6, 0, 0, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, CAST(N'2022-01-13T00:00:00.0000000-03:00' AS DateTimeOffset), 1, N'SMARTCITIES e FAVELA 2.0 (Infraestrutura urbana, Gestão de resíduos, Mobilidade, Segurança, Soluções, ecológicas, Qualidade de vida urbana, Operações Municipais, Planejamento e gestão urbana)')
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