--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "Tracks"
ADD Color  varchar(10)  NULL
go

UPDATE [dbo].[Tracks] SET Color = '#5578eb' WHERE [Uid] = '761529f9-f3f7-476e-bd2c-a1ee908a59e5'
GO
UPDATE [dbo].[Tracks] SET Color = '#5578eb' WHERE [Uid] = '24417536-5c46-4d8c-90df-71a96d1797bc'
GO
UPDATE [dbo].[Tracks] SET Color = '#5578eb' WHERE [Uid] = '21b2cc48-60b9-415b-8ae6-2c9b9d81fd35'
GO
UPDATE [dbo].[Tracks] SET Color = '#5578eb' WHERE [Uid] = '41f51907-0a02-448b-98aa-42065a5b475a'
GO
UPDATE [dbo].[Tracks] SET Color = '#5578eb' WHERE [Uid] = 'c63da8e5-3a1c-460e-abb4-fd5649fad4ba'
GO
UPDATE [dbo].[Tracks] SET Color = '#5578eb' WHERE [Uid] = 'd09aaaaf-d3e8-4170-86a9-103e139ca4c3'
GO
UPDATE [dbo].[Tracks] SET Color = '#F9B233' WHERE [Uid] = 'b6322dd2-997c-437c-8485-a9600d0a5979'
GO
UPDATE [dbo].[Tracks] SET Color = '#5578eb' WHERE [Uid] = 'b6e2ed47-553a-4f92-84cf-b505148ea084'
GO
UPDATE [dbo].[Tracks] SET Color = '#AD0F0A' WHERE [Uid] = 'fe555e57-0396-4e64-bccd-e85b6ee28a05'
GO
UPDATE [dbo].[Tracks] SET Color = '#FF4500' WHERE [Uid] = 'b098b966-2f41-499b-b867-f0783a4bb901'
GO
UPDATE [dbo].[Tracks] SET Color = '#941B81' WHERE [Uid] = '244b01ec-bb26-4b42-a3d9-7faf54965c92'
GO
UPDATE [dbo].[Tracks] SET Color = '#191970' WHERE [Uid] = '1517e35c-d6f1-41e7-89c8-cb8bcf151007'
GO
UPDATE [dbo].[Tracks] SET Color = '#E5007D' WHERE [Uid] = 'aeea9445-c123-425f-b16a-0d6f7bcfcd91'
GO
UPDATE [dbo].[Tracks] SET Color = '#008542' WHERE [Uid] = '32a90fdb-8362-41ba-a24f-a797e4c1e6ad'
GO
UPDATE [dbo].[Tracks] SET Color = '#5578eb' WHERE Color IS NULL
go

ALTER TABLE "Tracks"
ALTER COLUMN Color  varchar(10)  NULL
go

ALTER TABLE "dbo"."Conferences"
DROP CONSTRAINT "FK_Rooms_Conferences_RoomId"
go

DROP INDEX "IDX_Conferences_RoomId" 
ON "dbo"."Conferences"
go

ALTER TABLE "dbo"."Conferences" 
   ALTER COLUMN "RoomId" int NOT NULL
go

CREATE NONCLUSTERED INDEX "IDX_Conferences_RoomId" ON "dbo"."Conferences"
( 
	"RoomId"              ASC
)
go

ALTER TABLE "dbo"."Conferences"
	ADD CONSTRAINT "FK_Rooms_Conferences_RoomId" FOREIGN KEY ("RoomId") REFERENCES "dbo"."Rooms"("Id")
go
