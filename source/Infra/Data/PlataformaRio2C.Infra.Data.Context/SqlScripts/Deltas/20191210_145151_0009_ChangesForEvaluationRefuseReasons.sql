--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."ProjectBuyerEvaluations"
  DROP COLUMN "IsSent"
go

ALTER TABLE "dbo"."ProjectBuyerEvaluations"
ADD ProjectEvaluationRefuseReasonId  int  NULL
go

CREATE TABLE "ProjectEvaluationRefuseReasons"
( 
	"Id"                 int IDENTITY ( 1,1 ) ,
	"Uid"                uniqueidentifier  NOT NULL ,
	"Name"               varchar(500)  NOT NULL ,
	"HasAdditionalInfo"  bit  NOT NULL ,
	"DisplayOrder"       int  NOT NULL ,
	"IsDeleted"          bit  NOT NULL ,
	"CreateDate"         datetime  NOT NULL ,
	"CreateUserId"       int  NOT NULL ,
	"UpdateDate"         datetime  NOT NULL ,
	"UpdateUserId"       int  NOT NULL 
)
go

ALTER TABLE "ProjectEvaluationRefuseReasons"
ADD CONSTRAINT "PK_ProjectEvaluationRefuseReasons" PRIMARY KEY  CLUSTERED ("Id" ASC)
go

ALTER TABLE "ProjectEvaluationRefuseReasons"
ADD CONSTRAINT "IDX_UQ_ProjectEvaluationRefuseReasons_Uid" UNIQUE ("Uid"  ASC)
go

ALTER TABLE "ProjectEvaluationRefuseReasons"
	ADD CONSTRAINT "FK_Users_ProjectEvaluationRefuseReasons_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "ProjectEvaluationRefuseReasons"
	ADD CONSTRAINT "FK_Users_ProjectEvaluationRefuseReasons_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE "dbo"."ProjectBuyerEvaluations"
	ADD CONSTRAINT "FK_ProjectEvaluationRefuseReasons_ProjectBuyerEvaluations_ProjectEvaluationRefuseReasonId" FOREIGN KEY ("ProjectEvaluationRefuseReasonId") REFERENCES "ProjectEvaluationRefuseReasons"("Id")
go

SET IDENTITY_INSERT [dbo].[ProjectEvaluationRefuseReasons] ON 
GO
INSERT [dbo].[ProjectEvaluationRefuseReasons] ([Id], [Uid], [Name], [HasAdditionalInfo], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'c5ef0b7a-a255-482f-a25f-b16c4b65eea4', N'O projeto tem potencial, mas no momento não cabe nos parâmetros de nossa programação. | The project has potential, but doesn’t fit into our current program.', 0, 1, 0, CAST(N'2019-12-10 15:07:13.117' AS DateTime), 1, CAST(N'2019-12-10 15:07:13.117' AS DateTime), 1)
GO
INSERT [dbo].[ProjectEvaluationRefuseReasons] ([Id], [Uid], [Name], [HasAdditionalInfo], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'eac57d3d-d974-4510-8ebb-ad3932dc7c28', N'O projeto tem potencial, mas precisa de ser mais desenvolvido. | The project has potential, but needs to be better developed.', 0, 2, 0, CAST(N'2019-12-10 15:07:13.133' AS DateTime), 1, CAST(N'2019-12-10 15:07:13.133' AS DateTime), 1)
GO
INSERT [dbo].[ProjectEvaluationRefuseReasons] ([Id], [Uid], [Name], [HasAdditionalInfo], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'6702aa4a-835a-4cce-a238-89577e18d3a9', N'O projeto não está de acordo com nossa proposta de curadoria. | The project doesn’t meet our curatorship needs.', 0, 3, 0, CAST(N'2019-12-10 15:07:13.133' AS DateTime), 1, CAST(N'2019-12-10 15:07:13.133' AS DateTime), 1)
GO
INSERT [dbo].[ProjectEvaluationRefuseReasons] ([Id], [Uid], [Name], [HasAdditionalInfo], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'4e9f6a5e-4513-45d3-9b5f-69a59e843719', N'O projeto foge aos interesses solicitados por nós. | The project doesn’t meet the interests requested by us.', 0, 4, 0, CAST(N'2019-12-10 15:07:13.137' AS DateTime), 1, CAST(N'2019-12-10 15:07:13.137' AS DateTime), 1)
GO
INSERT [dbo].[ProjectEvaluationRefuseReasons] ([Id], [Uid], [Name], [HasAdditionalInfo], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'4bd0252c-0dcd-4c64-a686-8b1ea4d7b931', N'O projeto ainda está em estágio muito inicial, difícil de avaliar. | The project is still in a very early stage, difficult to evaluate.', 0, 5, 0, CAST(N'2019-12-10 15:07:13.140' AS DateTime), 1, CAST(N'2019-12-10 15:07:13.140' AS DateTime), 1)
GO
INSERT [dbo].[ProjectEvaluationRefuseReasons] ([Id], [Uid], [Name], [HasAdditionalInfo], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'65f777f8-c5e3-414d-9002-fc66781ac954', N'Outros. | Other.', 1, 6, 0, CAST(N'2019-12-10 15:07:13.143' AS DateTime), 1, CAST(N'2019-12-10 15:07:13.143' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[ProjectEvaluationRefuseReasons] OFF
GO