SET IDENTITY_INSERT [dbo].[InnovationOptionGroups] ON 
GO
INSERT [dbo].[InnovationOptionGroups] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'752598d8-d445-48f7-be41-6a1b110fa749', N'Quais dessas experiências a empresa já participou?', 1, 0, CAST(N'2020-02-23T23:10:26.0870000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:10:26.0870000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptionGroups] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'50a702a9-d189-4f65-bef9-88365d73ba66', N'Enquadre seu produto ou serviço num track abaixo:', 2, 0, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptionGroups] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'ab308844-5b2f-41e1-9a11-c00088ad2ddf', N'Tecnologia usadas:', 3, 0, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[InnovationOptionGroups] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'2acbcb51-8520-4767-a5c8-927b8cc007e5', N'Qual o seu principal objetivo em participar das Pitching de Startups?', 4, 0, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:10:26.0900000+00:00' AS DateTimeOffset), 1)
GO
SET IDENTITY_INSERT [dbo].[InnovationOptionGroups] OFF
GO

SET IDENTITY_INSERT [dbo].[InnovationOrganizationExperienceOptions] ON 
;
INSERT [dbo].[InnovationOrganizationExperienceOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'82167c1d-7ca6-447f-80c7-ae9188add436', N'Recebeu apoio de incubadora/aceleradora', 1, 0, 0, CAST(N'2020-02-23T23:13:38.2500000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:13:38.2500000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationExperienceOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'29b2cc2f-374d-4f2f-ac00-3513d02ec9c3', N'Captou recursos para pesquisa e desenvolvimento de produtos/serviços tecnológicos', 2, 0, 0, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationExperienceOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'2fd9f6ba-8852-4dd5-a402-dcd2c14923cb', N'Se relacionou formalmente com grandes ou médias empresas', 3, 0, 0, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationExperienceOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'60079b3b-a5d9-4e59-a964-725339afbe7f', N'Recebeu investimento de terceiros que envolveram parte do capital da empresa', 4, 0, 0, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationExperienceOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'4f440536-bab7-4e43-a3a4-f977abafbda8', N'Nenhuma das opções acima', 5, 0, 0, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:13:38.2530000+00:00' AS DateTimeOffset), 1)
;
SET IDENTITY_INSERT [dbo].[InnovationOrganizationExperienceOptions] OFF
;

SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptions] ON 
;
INSERT [dbo].[InnovationOrganizationTrackOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'702a9b2e-dbcb-4bb0-8405-c5bd72eaf627', N'MÍDIA E ENTRETENIMENTO: educação; gamification, plataforma de vídeo; publicidade; mídia social, VR/AR', 1, 0, 0, CAST(N'2020-02-23T23:15:59.3970000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.3970000+00:00' AS DateTimeOffset), 1)
;			  
INSERT [dbo].[InnovationOrganizationTrackOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'0b1bd552-ad36-4a4d-a55d-d7b07eb6e4e0', N'OTIMIZAÇÃO DE NEGÓCIOS: Internet das coisas (IoT); SaaS/PaaS; Inteligência Artificial (AI); economia colaborativa; e-commerce; cibersegurança; business inteligence – dados (geração de valor em dados)', 2, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
;			  
INSERT [dbo].[InnovationOrganizationTrackOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'fb997a1a-cbf0-446b-82ff-ff7b5eaa21bf', N'FINTECH: blockchain/crypto; insurance tech; investimentos; pagamentos; banking; robôs de investimento', 3, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
;			  
INSERT [dbo].[InnovationOrganizationTrackOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'1646a4d0-f43a-4633-9730-3f8893a627ce', N'A NOVA SOCIEDADE: Construtech, smatcities, mercado imobiliário,tecnologia limpa; sustentabilidade social e ambiental; amplo acesso àtecnologia; políticas públicas; questões sociais; sistema de inovação; atendimento a família; comunidade', 4, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
;			  
INSERT [dbo].[InnovationOrganizationTrackOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'a7eedef9-88b9-4ef4-a6ea-32ea5db28598', N'SAÚDE E BEM ESTAR: diagnósticos; computer visioning para diagnósticos; devices de health tech, inteligência artificial para saúde,wellness e mindfullness; biotech', 5, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
;			  
INSERT [dbo].[InnovationOrganizationTrackOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'b05fdb02-f880-49d0-818d-65ba716ec0b8', N'ALIMENTAÇÃO: Agritech, foodtech (dietas personalizadas, dietas genéticas, comida funcional); farmtech; inovação em restaurantes e supermercados', 6, 0, 0, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:15:59.4000000+00:00' AS DateTimeOffset), 1)
;			  
SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptions] OFF
;

SET IDENTITY_INSERT [dbo].[InnovationOrganizationTechnologyOptions] ON 
;
INSERT [dbo].[InnovationOrganizationTechnologyOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'0ee805cd-7e63-47de-8034-c405dc5e1da3', N'SAAS',1, 0, 0, CAST(N'2020-02-23T23:16:55.9570000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9570000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationTechnologyOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'6932ac40-e16d-4858-b550-1b4cd2f9461d', N'AI', 2, 0, 0, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationTechnologyOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'516e3187-ce60-4541-9f46-ac41f29ea0eb', N'IOT',3, 0, 0, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationTechnologyOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'9b3efffc-b4f9-4e65-b679-69eee581dcc2', N'Blockchain',4, 0, 0, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationTechnologyOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'3663d8a3-df1d-4a41-a3ef-13cf2602fa9d', N'Robótica', 5, 0, 0, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationTechnologyOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'f5b4623d-f4f9-4440-b575-140c273c41d2', N'Outros', 6, 1, 0, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:16:55.9600000+00:00' AS DateTimeOffset), 1)
;
SET IDENTITY_INSERT [dbo].[InnovationOrganizationTechnologyOptions] OFF
;

SET IDENTITY_INSERT [dbo].[InnovationOrganizationObjectivesOptions] ON 
;
INSERT [dbo].[InnovationOrganizationObjectivesOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'9ecefe6d-ea9a-4dce-88a0-5b720e02eae0', N'Desenvolvimento tecnológico de uma nova solução',1, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationObjectivesOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'0a0ea283-d9cb-4bce-87b9-5582c57b6e42', N'Aprimoramento tecnológico de uma solução existente',2, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationObjectivesOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'0d2684e1-4a8a-4088-8547-6ac274eb1ee4', N'Venda de produto/serviço/solução', 3, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationObjectivesOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'064371d0-864f-4b79-b709-221f73b0d35d', N'Oportunidade de conexão com médias/grandes empresas', 4, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationObjectivesOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'38a07682-8bd4-4ede-8be2-1593bad8e0b7', N'Validação de ideias/protótipos',5, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationObjectivesOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'5f872b5b-da41-43e6-abe7-cef7e6bc0ce6', N'Oportunidade de investimento', 6, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationObjectivesOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (7, N'1a144db0-dbf2-4ecc-a4c0-267cea374faa', N'Acesso a serviços de incubação/aceleração', 7, 0, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
;
INSERT [dbo].[InnovationOrganizationObjectivesOptions] ([Id], [Uid], [Name], [DisplayOrder], [HasAdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (8, N'5f62c762-01c0-4f55-a89d-d1e5690817f6', N'Outros', 8, 1, 0, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:18:21.0770000+00:00' AS DateTimeOffset), 1)
;
SET IDENTITY_INSERT [dbo].[InnovationOrganizationObjectivesOptions] OFF 
;
		
SET IDENTITY_INSERT [dbo].[WorkDedications] ON 
GO
INSERT [dbo].[WorkDedications] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'dcc9878d-ebc7-438c-8a0a-5952b75a8b54', N'Parcial', 1, 0, CAST(N'2020-02-23T23:21:38.8500000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:21:38.8500000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[WorkDedications] ([Id], [Uid], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'ada0c122-45ef-41e4-9002-edb9e9fbdb51', N'Integral', 2, 0, CAST(N'2020-02-23T23:21:38.8530000+00:00' AS DateTimeOffset), 1, CAST(N'2020-02-23T23:21:38.8530000+00:00' AS DateTimeOffset), 1)
GO
SET IDENTITY_INSERT [dbo].[WorkDedications] OFF
GO
