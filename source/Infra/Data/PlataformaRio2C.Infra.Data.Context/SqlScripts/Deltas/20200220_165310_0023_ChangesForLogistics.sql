--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Logistics"
  DROP COLUMN "AirfareOtherLogisticSponsor"
go

ALTER TABLE "dbo"."Logistics"
  DROP COLUMN "AccommodationOtherLogisticSponsor"
go

ALTER TABLE "dbo"."Logistics"
  DROP COLUMN "AirportTransferOtherLogisticSponsor"
go

ALTER TABLE "dbo"."Logistics"
ALTER COLUMN AdditionalInfo  varchar(1000)  NULL
go

ALTER TABLE "AttendeeLogisticSponsors"
ADD IsOther  bit  NULL
go

ALTER TABLE "Places"
ADD IsAirport  bit  NULL
go

-- Places Addresses
INSERT INTO [dbo].[Addresses] ([Uid], [CountryId], [StateId], [CityId], [Address1], [ZipCode], [IsManual], [Latitude], [Longitude], [IsGeoLocationUpdated], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (NEWID(), (SELECT Id FROM dbo.Countries where Code = 'BR'), (SELECT Id FROM dbo.States where Code = 'RJ'), (SELECT Id FROM dbo.Cities where Name = 'Rio de Janeiro'), 'Av. das Américas, 8585 - Barra da Tijuca', '22793-081', 0, null, null, 0, 0, GETDATE(), 1, GETDATE(), 1)
GO
INSERT INTO [dbo].[Addresses] ([Uid], [CountryId], [StateId], [CityId], [Address1], [ZipCode], [IsManual], [Latitude], [Longitude], [IsGeoLocationUpdated], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (NEWID(), (SELECT Id FROM dbo.Countries where Code = 'BR'), (SELECT Id FROM dbo.States where Code = 'RJ'), (SELECT Id FROM dbo.Cities where Name = 'Rio de Janeiro'), 'Av. Lúcio Costa, 5400 - Barra da Tijuca', '22630-012', 0, null, null, 0, 0, GETDATE(), 1, GETDATE(), 1)
GO
INSERT INTO [dbo].[Addresses] ([Uid], [CountryId], [StateId], [CityId], [Address1], [ZipCode], [IsManual], [Latitude], [Longitude], [IsGeoLocationUpdated], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (NEWID(), (SELECT Id FROM dbo.Countries where Code = 'BR'), (SELECT Id FROM dbo.States where Code = 'RJ'), (SELECT Id FROM dbo.Cities where Name = 'Rio de Janeiro'), 'Av. das Américas, 5300 - Barra da Tijuca', '22793-080', 0, null, null, 0, 0, GETDATE(), 1, GETDATE(), 1)
GO

-- Places
SET IDENTITY_INSERT [dbo].[Places] ON 
GO
INSERT [dbo].[Places] ([Id], [Uid], [Name], [IsHotel], [IsAirport], [AddressId], [Website], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'daa2cf74-bf8a-49df-aaa1-af0ad3640dff', N'Vogue Square Fashion Hotel', 1, 0, (SELECT Id FROM dbo.Addresses WHERE Address1 = 'Av. das Américas, 8585 - Barra da Tijuca' AND ZipCode = '22793-081'), N'http://www.voguefashionhotel.com.br', N'Café da Manhã Incluído | Breakfast included', 0, CAST(N'2020-01-23 14:50:28.463' AS datetimeoffset), 1, CAST(N'2020-01-23 14:50:28.463' AS datetimeoffset), 1)
GO
INSERT [dbo].[Places] ([Id], [Uid], [Name], [IsHotel], [IsAirport], [AddressId], [Website], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'f334193e-0b47-4cbc-81b3-657c88a91981', N'Windsor Marapendi', 1, 0, (SELECT Id FROM dbo.Addresses WHERE Address1 = 'Av. Lúcio Costa, 5400 - Barra da Tijuca' AND ZipCode = '22630-012'), N'https://windsorhoteis.com/hotel/windsor-marapendi', N'Café da Manhã Incluído | Breakfast included', 0, CAST(N'2020-01-23 14:50:28.473' AS datetimeoffset), 1, CAST(N'2020-01-23 14:50:28.473' AS datetimeoffset), 1)
GO
INSERT [dbo].[Places] ([Id], [Uid], [Name], [IsHotel], [IsAirport], [AddressId], [Website], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'39b526d0-e560-4eed-8ef4-27251e8a8cde', N'Cidade das Artes', 0, 0, (SELECT Id FROM dbo.Addresses WHERE Address1 = 'Av. das Américas, 5300 - Barra da Tijuca' AND ZipCode = '22793-080'), NULL, NULL, 0, CAST(N'2020-01-23 14:50:28.487' AS datetimeoffset), 1, CAST(N'2020-01-23 14:50:28.487' AS datetimeoffset), 1)
GO
INSERT [dbo].[Places] ([Id], [Uid], [Name], [IsHotel], [IsAirport], [AddressId], [Website], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'49da6803-871d-452e-b51e-1eef0f993cb6', N'Rodoviária Novo Rio', 0, 0, NULL, NULL, NULL, 0, CAST(N'2020-03-13 14:50:28.463' AS datetimeoffset), 1, CAST(N'2020-03-13 14:50:28.463' AS datetimeoffset), 1)
GO
INSERT [dbo].[Places] ([Id], [Uid], [Name], [IsHotel], [IsAirport], [AddressId], [Website], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'e78234a7-1777-46eb-a73e-f79a314633aa', N'Rio de Janeiro - GIG Airport', 0, 1, NULL, NULL, NULL, 0, CAST(N'2020-03-13 14:50:28.463' AS datetimeoffset), 1, CAST(N'2020-03-13 14:50:28.463' AS datetimeoffset), 1)
GO
INSERT [dbo].[Places] ([Id], [Uid], [Name], [IsHotel], [IsAirport], [AddressId], [Website], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'90dcc4ed-7383-4b28-84c5-48dc5dcf0a49', N'Rio de Janeiro - SDU Airport', 0, 1, NULL, NULL, NULL, 0, CAST(N'2020-03-13 14:50:28.463' AS datetimeoffset), 1, CAST(N'2020-03-13 14:50:28.463' AS datetimeoffset), 1)
GO
SET IDENTITY_INSERT [dbo].[Places] OFF
GO

SET IDENTITY_INSERT [dbo].[AttendeePlaces] ON 
GO
INSERT [dbo].[AttendeePlaces] ([Id], [Uid], [EditionId], [PlaceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'a9a88577-df52-4777-a816-1eca2d82269f', 2, 1, 0, CAST(N'2020-01-23 14:54:28.037' AS datetimeoffset), 1, CAST(N'2020-01-23 14:54:28.037' AS datetimeoffset), 1)
GO
INSERT [dbo].[AttendeePlaces] ([Id], [Uid], [EditionId], [PlaceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'1dc3637c-48fd-4b59-b103-119dae5f8849', 2, 2, 0, CAST(N'2020-01-23 14:54:28.040' AS datetimeoffset), 1, CAST(N'2020-01-23 14:54:28.040' AS datetimeoffset), 1)
GO
INSERT [dbo].[AttendeePlaces] ([Id], [Uid], [EditionId], [PlaceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'b1302f94-586a-47d6-ba7f-a101321f1664', 2, 3, 0, CAST(N'2020-01-23 14:54:28.050' AS datetimeoffset), 1, CAST(N'2020-01-23 14:54:28.050' AS datetimeoffset), 1)
GO
INSERT [dbo].[AttendeePlaces] ([Id], [Uid], [EditionId], [PlaceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'37c6afb7-d78f-43a9-bf7d-c1d68c7e37ac', 2, 4, 0, CAST(N'2020-03-13 14:54:28.037' AS datetimeoffset), 1, CAST(N'2020-03-13 14:54:28.037' AS datetimeoffset), 1)
GO
INSERT [dbo].[AttendeePlaces] ([Id], [Uid], [EditionId], [PlaceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (5, N'2e4de1b4-ac9a-4989-beae-ba0ea07e26ba', 2, 5, 0, CAST(N'2020-03-13 14:54:28.037' AS datetimeoffset), 1, CAST(N'2020-03-13 14:54:28.037' AS datetimeoffset), 1)
GO
INSERT [dbo].[AttendeePlaces] ([Id], [Uid], [EditionId], [PlaceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (6, N'3a0f76c2-81c0-4ccf-8b02-55b36c2f9abc', 2, 6, 0, CAST(N'2020-03-13 14:54:28.037' AS datetimeoffset), 1, CAST(N'2020-03-13 14:54:28.037' AS datetimeoffset), 1)
GO
SET IDENTITY_INSERT [dbo].[AttendeePlaces] OFF
GO

-- Logistic Sponsors
SET IDENTITY_INSERT [dbo].[LogisticSponsors] ON 
GO
INSERT [dbo].[LogisticSponsors] ([Id], [Uid], [Name], [IsAirfareTicketRequired], [IsOtherRequired], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'56d74d6b-31d5-4652-8685-213216fb22a8', N'Rio2C', 1, 0, 0, CAST(N'2020-01-23 15:02:16.733' AS datetimeoffset), 1, CAST(N'2020-01-23 15:02:16.733' AS datetimeoffset), 1)
GO
INSERT [dbo].[LogisticSponsors] ([Id], [Uid], [Name], [IsAirfareTicketRequired], [IsOtherRequired], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'b75920d0-05e0-4b72-bed1-1ab3cfe8ed2b', N'APEX', 1, 0, 0, CAST(N'2020-01-23 15:02:16.740' AS datetimeoffset), 1, CAST(N'2020-01-23 15:02:16.740' AS datetimeoffset), 1)
GO
INSERT [dbo].[LogisticSponsors] ([Id], [Uid], [Name], [IsAirfareTicketRequired], [IsOtherRequired], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'421c6c70-ef6e-4071-b00f-7c4c222aa410', N'Próprio | Own', 0, 0, 0, CAST(N'2020-01-23 15:02:16.750' AS datetimeoffset), 1, CAST(N'2020-01-23 15:02:16.750' AS datetimeoffset), 1)
GO
INSERT [dbo].[LogisticSponsors] ([Id], [Uid], [Name], [IsAirfareTicketRequired], [IsOtherRequired], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'37030831-5d54-4227-a050-820a32a0ad95', N'Patrocinado por outras instituições | Sponsored by other institutions', 0, 1, 0, CAST(N'2020-01-23 15:02:16.753' AS datetimeoffset), 1, CAST(N'2020-01-23 15:02:16.753' AS datetimeoffset), 1)
GO
SET IDENTITY_INSERT [dbo].[LogisticSponsors] OFF
GO

SET IDENTITY_INSERT [dbo].[AttendeeLogisticSponsors] ON 
GO
INSERT [dbo].[AttendeeLogisticSponsors] ([Id], [Uid], [EditionId], [LogisticSponsorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'616a9d09-9aa7-47a8-959c-7d14ac897a3f', 2, 1, 0, CAST(N'2020-01-23 15:02:16.757' AS datetimeoffset), 1, CAST(N'2020-01-23 15:02:16.757' AS datetimeoffset), 1)
GO
INSERT [dbo].[AttendeeLogisticSponsors] ([Id], [Uid], [EditionId], [LogisticSponsorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'4d1583a0-9fb0-47ac-b159-b77530930f0b', 2, 2, 0, CAST(N'2020-01-23 15:02:16.760' AS datetimeoffset), 1, CAST(N'2020-01-23 15:02:16.760' AS datetimeoffset), 1)
GO
INSERT [dbo].[AttendeeLogisticSponsors] ([Id], [Uid], [EditionId], [LogisticSponsorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'dfe407a2-8e7a-45d5-9bcd-1a6be011b0ae', 2, 3, 0, CAST(N'2020-01-23 15:02:16.763' AS datetimeoffset), 1, CAST(N'2020-01-23 15:02:16.763' AS datetimeoffset), 1)
GO
INSERT [dbo].[AttendeeLogisticSponsors] ([Id], [Uid], [EditionId], [LogisticSponsorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'ab120dd7-2868-4ca5-a0eb-432810d3ddee', 2, 4, 0, CAST(N'2020-01-23 15:02:16.763' AS datetimeoffset), 1, CAST(N'2020-01-23 15:02:16.763' AS datetimeoffset), 1)
GO
SET IDENTITY_INSERT [dbo].[AttendeeLogisticSponsors] OFF
GO

-- Logistic Transfer Statuses 
SET IDENTITY_INSERT [dbo].[LogisticTransferStatuses] ON 
GO
INSERT [dbo].[LogisticTransferStatuses] ([Id], [Uid], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (1, N'2a6b8ecc-7e85-4f35-9e50-84570c9f5431', N'Confirmado | Confirmed', 0, CAST(N'2020-01-27 14:22:18.680' AS datetimeoffset), 1, CAST(N'2020-01-27 14:22:18.680' AS datetimeoffset), 1)
GO
INSERT [dbo].[LogisticTransferStatuses] ([Id], [Uid], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (2, N'bd4d79df-d193-4b40-a144-71d95b2fc169', N'Dispensado | Waived', 0, CAST(N'2020-01-27 14:22:18.683' AS datetimeoffset), 1, CAST(N'2020-01-27 14:22:18.683' AS datetimeoffset), 1)
GO
INSERT [dbo].[LogisticTransferStatuses] ([Id], [Uid], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (3, N'926db045-af08-4132-8781-bbffc0e9bea9', N'Código Uber | Uber Code', 0, CAST(N'2020-01-27 14:22:18.703' AS datetimeoffset), 1, CAST(N'2020-01-27 14:22:18.703' AS datetimeoffset), 1)
GO
INSERT [dbo].[LogisticTransferStatuses] ([Id], [Uid], [Name], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (4, N'0ba5ad37-0863-4eb0-8fc2-85b3c025c269', N'Fora do período de serviço | Outside the service period', 0, CAST(N'2020-01-27 14:22:18.727' AS datetimeoffset), 1, CAST(N'2020-01-27 14:22:18.727' AS datetimeoffset), 1)
GO
SET IDENTITY_INSERT [dbo].[LogisticTransferStatuses] OFF
GO

UPDATE "AttendeeLogisticSponsors"
    SET IsOther = 0
go

ALTER TABLE "AttendeeLogisticSponsors"
ALTER COLUMN IsOther  bit  NOT NULL
go

UPDATE "Places"
    SET IsAirport = 0
go

ALTER TABLE "Places"
ALTER COLUMN IsAirport  bit  NOT NULL
go
