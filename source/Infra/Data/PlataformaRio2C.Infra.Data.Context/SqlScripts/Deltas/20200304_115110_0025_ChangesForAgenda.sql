--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE [dbo].[NegotiationConfigs]
ALTER COLUMN [TimeIntervalBetweenTurn] [time](7) NOT NULL
go

ALTER TABLE [dbo].[Negotiations]
	ADD [IsAutomatic] [bit] NOT NULL
go

ALTER TABLE "dbo"."NegotiationRoomConfigs"
DROP CONSTRAINT "FK_Users_NegotiationRoomConfigs_CrateUserId"
go

execute sp_rename '"dbo".NegotiationRoomConfigs."CrateUserId"', 'CreateUserId', 'COLUMN'
go

ALTER TABLE "dbo"."NegotiationRoomConfigs"
	ADD CONSTRAINT "FK_Users_NegotiationRoomConfigs_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
go

ALTER TABLE [dbo].[Editions]
	ADD [AudiovisualNegotiationsCreateDate] [datetimeoffset](7) NULL
go


SET IDENTITY_INSERT [dbo].[NegotiationConfigs] ON 
GO
INSERT [dbo].[NegotiationConfigs] ([Id], [Uid], [EditionId], [StartDate], [EndDate], [RoundFirstTurn], [RoundSecondTurn], [TimeIntervalBetweenTurn], [TimeOfEachRound], [TimeIntervalBetweenRound], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'7cbadd45-0cce-4074-a6ab-a5f7f5fc0973', 2, CAST(N'2020-05-05T17:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2020-05-05T21:30:00.0000000+00:00' AS DateTimeOffset), 9, 0, CAST(N'00:00:00' AS Time), CAST(N'00:20:00' AS Time), CAST(N'00:10:00' AS Time), 0, CAST(N'2020-03-04T16:16:33.8830000+00:00' AS DateTimeOffset), 1, CAST(N'2020-03-04T16:16:33.8830000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[NegotiationConfigs] ([Id], [Uid], [EditionId], [StartDate], [EndDate], [RoundFirstTurn], [RoundSecondTurn], [TimeIntervalBetweenTurn], [TimeOfEachRound], [TimeIntervalBetweenRound], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'5a517cab-0353-4e85-95d0-ee570fd25437', 2, CAST(N'2020-05-06T13:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2020-05-06T22:00:00.0000000+00:00' AS DateTimeOffset), 6, 9, CAST(N'01:30:00' AS Time), CAST(N'00:20:00' AS Time), CAST(N'00:10:00' AS Time), 0, CAST(N'2020-03-04T16:16:33.8830000+00:00' AS DateTimeOffset), 1, CAST(N'2020-03-04T16:16:33.8830000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[NegotiationConfigs] ([Id], [Uid], [EditionId], [StartDate], [EndDate], [RoundFirstTurn], [RoundSecondTurn], [TimeIntervalBetweenTurn], [TimeOfEachRound], [TimeIntervalBetweenRound], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'0612ec24-a3f7-4a5b-a9b3-c54a41a31733', 2, CAST(N'2020-05-07T13:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2020-05-07T22:00:00.0000000+00:00' AS DateTimeOffset), 6, 9, CAST(N'01:30:00' AS Time), CAST(N'00:20:00' AS Time), CAST(N'00:10:00' AS Time), 0, CAST(N'2020-03-04T16:16:33.8830000+00:00' AS DateTimeOffset), 1, CAST(N'2020-03-04T16:16:33.8830000+00:00' AS DateTimeOffset), 1)
GO
INSERT [dbo].[NegotiationConfigs] ([Id], [Uid], [EditionId], [StartDate], [EndDate], [RoundFirstTurn], [RoundSecondTurn], [TimeIntervalBetweenTurn], [TimeOfEachRound], [TimeIntervalBetweenRound], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'0d32b17b-a99b-4524-ad52-9f361a4969af', 2, CAST(N'2020-05-08T13:00:00.0000000+00:00' AS DateTimeOffset), CAST(N'2020-05-08T22:00:00.0000000+00:00' AS DateTimeOffset), 6, 7, CAST(N'01:30:00' AS Time), CAST(N'00:20:00' AS Time), CAST(N'00:10:00' AS Time), 0, CAST(N'2020-03-04T16:16:33.8830000+00:00' AS DateTimeOffset), 1, CAST(N'2020-03-04T16:16:33.8830000+00:00' AS DateTimeOffset), 1)
GO
SET IDENTITY_INSERT [dbo].[NegotiationConfigs] OFF
GO
