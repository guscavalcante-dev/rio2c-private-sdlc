SET IDENTITY_INSERT [dbo].[SalesPlatformWebhookRequests] ON 
GO
INSERT [dbo].[SalesPlatformWebhookRequests] ([Id], [Uid], [SalesPlatformId], [CreateDate], [Endpoint], [Header], [Payload], [IpAddress], [IsProcessed], [IsProcessing], [ProcessingCount], [LastProcessingDate], [NextProcessingDate], [ProcessingErrorCode], [ProcessingErrorMessage], [ManualProcessingUserId], [SecurityStamp]) VALUES (1, N'e99e2b55-1019-465f-a0b5-b3770e3e2ff4', 1, CAST(N'2019-08-30 15:29:50.913' AS DateTime), N'http://localhost:43931/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A', N'Accept: text/plain
Accept-Encoding: gzip, deflate
Host: localhost:43931
User-Agent: Eventbrite Webhooks (Dilithium)
X-Eventbrite-Delivery: 1806556
X-Eventbrite-Event: order.updated
X-Forwarded-For: 52.202.1.10
X-Original-Host: 85251281.ngrok.io
ApplicationInsights-RequestTrackingTelemetryModule-RootRequest-Id: d764a8b5-311e-4786-96b9-2889594b0c74
', N'{"config": {"action": "order.updated", "user_id": "311783287832", "endpoint_url": "http://85251281.ngrok.io/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A", "webhook_id": "1806556"}, "api_url": "https://www.eventbriteapi.com/v3/orders/1034608029/"}', N'52.202.1.10', 1, 0, 1, CAST(N'2019-08-30 20:24:45.767' AS DateTime), NULL, NULL, NULL, NULL, N'5f14e2c5-0443-411d-8c7d-da7fe01d4641')
GO
INSERT [dbo].[SalesPlatformWebhookRequests] ([Id], [Uid], [SalesPlatformId], [CreateDate], [Endpoint], [Header], [Payload], [IpAddress], [IsProcessed], [IsProcessing], [ProcessingCount], [LastProcessingDate], [NextProcessingDate], [ProcessingErrorCode], [ProcessingErrorMessage], [ManualProcessingUserId], [SecurityStamp]) VALUES (2, N'61eddc3e-9f53-46e5-8818-805f53aeb960', 1, CAST(N'2019-08-30 15:29:50.947' AS DateTime), N'http://localhost:43931/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A', N'Accept: text/plain
Accept-Encoding: gzip, deflate
Host: localhost:43931
User-Agent: Eventbrite Webhooks (Dilithium)
X-Eventbrite-Delivery: 1806556
X-Eventbrite-Event: order.placed
X-Forwarded-For: 52.201.128.93
X-Original-Host: 85251281.ngrok.io
ApplicationInsights-RequestTrackingTelemetryModule-RootRequest-Id: eb7e11a2-1544-4c77-8881-767d836b6f77
', N'{"config": {"action": "order.placed", "user_id": "311783287832", "endpoint_url": "http://85251281.ngrok.io/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A", "webhook_id": "1806556"}, "api_url": "https://www.eventbriteapi.com/v3/orders/1034608029/"}', N'52.201.128.93', 1, 0, 1, CAST(N'2019-08-30 20:24:54.947' AS DateTime), NULL, NULL, NULL, NULL, N'729f2bef-c3a1-4ba7-9e22-38085433470c')
GO
INSERT [dbo].[SalesPlatformWebhookRequests] ([Id], [Uid], [SalesPlatformId], [CreateDate], [Endpoint], [Header], [Payload], [IpAddress], [IsProcessed], [IsProcessing], [ProcessingCount], [LastProcessingDate], [NextProcessingDate], [ProcessingErrorCode], [ProcessingErrorMessage], [ManualProcessingUserId], [SecurityStamp]) VALUES (3, N'32704d0b-4889-4ba0-88f1-de4e639c7a0a', 1, CAST(N'2019-08-30 15:29:51.297' AS DateTime), N'http://localhost:43931/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A', N'Accept: text/plain
Accept-Encoding: gzip, deflate
Host: localhost:43931
User-Agent: Eventbrite Webhooks (Dilithium)
X-Eventbrite-Delivery: 1806556
X-Eventbrite-Event: attendee.updated
X-Forwarded-For: 52.202.1.10
X-Original-Host: 85251281.ngrok.io
ApplicationInsights-RequestTrackingTelemetryModule-RootRequest-Id: 70b81f4d-02f8-4089-8e99-5195c0ec72ca
', N'{"config": {"action": "attendee.updated", "user_id": "311783287832", "endpoint_url": "http://85251281.ngrok.io/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A", "webhook_id": "1806556"}, "api_url": "https://www.eventbriteapi.com/v3/events/71059816825/attendees/1346364453/"}', N'52.202.1.10', 1, 0, 1, CAST(N'2019-08-30 20:24:55.450' AS DateTime), NULL, NULL, NULL, NULL, N'3e2e2c83-7396-4c92-b201-fe84dd52ec9c')
GO
INSERT [dbo].[SalesPlatformWebhookRequests] ([Id], [Uid], [SalesPlatformId], [CreateDate], [Endpoint], [Header], [Payload], [IpAddress], [IsProcessed], [IsProcessing], [ProcessingCount], [LastProcessingDate], [NextProcessingDate], [ProcessingErrorCode], [ProcessingErrorMessage], [ManualProcessingUserId], [SecurityStamp]) VALUES (4, N'a1433c22-966b-4bcb-a122-dad78274e029', 1, CAST(N'2019-08-30 15:29:51.310' AS DateTime), N'http://localhost:43931/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A', N'Accept: text/plain
Accept-Encoding: gzip, deflate
Host: localhost:43931
User-Agent: Eventbrite Webhooks (Dilithium)
X-Eventbrite-Delivery: 1806556
X-Eventbrite-Event: attendee.updated
X-Forwarded-For: 52.201.4.8
X-Original-Host: 85251281.ngrok.io
ApplicationInsights-RequestTrackingTelemetryModule-RootRequest-Id: 73d826a4-e2ce-40c9-82c8-a051e077f027
', N'{"config": {"action": "attendee.updated", "user_id": "311783287832", "endpoint_url": "http://85251281.ngrok.io/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A", "webhook_id": "1806556"}, "api_url": "https://www.eventbriteapi.com/v3/events/71059816825/attendees/1346364455/"}', N'52.201.4.8', 1, 0, 1, CAST(N'2019-08-30 20:24:55.940' AS DateTime), NULL, NULL, NULL, NULL, N'8dcca19b-a267-4129-9c9a-efe6174dcbd1')
GO
INSERT [dbo].[SalesPlatformWebhookRequests] ([Id], [Uid], [SalesPlatformId], [CreateDate], [Endpoint], [Header], [Payload], [IpAddress], [IsProcessed], [IsProcessing], [ProcessingCount], [LastProcessingDate], [NextProcessingDate], [ProcessingErrorCode], [ProcessingErrorMessage], [ManualProcessingUserId], [SecurityStamp]) VALUES (5, N'10a13d9e-2a88-40a6-ab16-d4336c36d130', 1, CAST(N'2019-08-30 15:29:52.073' AS DateTime), N'http://localhost:43931/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A', N'Accept: text/plain
Accept-Encoding: gzip, deflate
Host: localhost:43931
User-Agent: Eventbrite Webhooks (Dilithium)
X-Eventbrite-Delivery: 1806556
X-Eventbrite-Event: order.updated
X-Forwarded-For: 52.202.1.10
X-Original-Host: 85251281.ngrok.io
ApplicationInsights-RequestTrackingTelemetryModule-RootRequest-Id: 250c3a8c-3bd1-4e4d-8854-23a64b6653d6
', N'{"config": {"action": "order.updated", "user_id": "311783287832", "endpoint_url": "http://85251281.ngrok.io/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A", "webhook_id": "1806556"}, "api_url": "https://www.eventbriteapi.com/v3/orders/1034608029/"}', N'52.202.1.10', 1, 0, 1, CAST(N'2019-08-30 20:24:56.443' AS DateTime), NULL, NULL, NULL, NULL, N'9a237ecf-642c-48f7-8b0e-92366a06291f')
GO
INSERT [dbo].[SalesPlatformWebhookRequests] ([Id], [Uid], [SalesPlatformId], [CreateDate], [Endpoint], [Header], [Payload], [IpAddress], [IsProcessed], [IsProcessing], [ProcessingCount], [LastProcessingDate], [NextProcessingDate], [ProcessingErrorCode], [ProcessingErrorMessage], [ManualProcessingUserId], [SecurityStamp]) VALUES (6, N'e90feea2-fb37-481f-bc60-c25cf4d85b89', 1, CAST(N'2019-08-30 15:32:08.190' AS DateTime), N'http://localhost:43931/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A', N'Accept: text/plain
Accept-Encoding: gzip, deflate
Host: localhost:43931
User-Agent: Eventbrite Webhooks (Dilithium)
X-Eventbrite-Delivery: 1806556
X-Eventbrite-Event: order.updated
X-Forwarded-For: 52.200.70.39
X-Original-Host: 85251281.ngrok.io
ApplicationInsights-RequestTrackingTelemetryModule-RootRequest-Id: 75857953-2569-431d-b9c5-aca3b00194d7
', N'{"config": {"action": "order.updated", "user_id": "311783287832", "endpoint_url": "http://85251281.ngrok.io/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A", "webhook_id": "1806556"}, "api_url": "https://www.eventbriteapi.com/v3/orders/1034611193/"}', N'52.200.70.39', 1, 0, 1, CAST(N'2019-08-30 20:24:56.910' AS DateTime), NULL, NULL, NULL, NULL, N'7a21a16a-7975-48a6-9338-911ee52b0128')
GO
INSERT [dbo].[SalesPlatformWebhookRequests] ([Id], [Uid], [SalesPlatformId], [CreateDate], [Endpoint], [Header], [Payload], [IpAddress], [IsProcessed], [IsProcessing], [ProcessingCount], [LastProcessingDate], [NextProcessingDate], [ProcessingErrorCode], [ProcessingErrorMessage], [ManualProcessingUserId], [SecurityStamp]) VALUES (7, N'f4cd0187-57f3-4787-9d4b-5bb175d2d087', 1, CAST(N'2019-08-30 15:32:08.217' AS DateTime), N'http://localhost:43931/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A', N'Accept: text/plain
Accept-Encoding: gzip, deflate
Host: localhost:43931
User-Agent: Eventbrite Webhooks (Dilithium)
X-Eventbrite-Delivery: 1806556
X-Eventbrite-Event: order.placed
X-Forwarded-For: 52.201.4.8
X-Original-Host: 85251281.ngrok.io
ApplicationInsights-RequestTrackingTelemetryModule-RootRequest-Id: 299ef8b6-7942-4bfa-a830-a62a1a709928
', N'{"config": {"action": "order.placed", "user_id": "311783287832", "endpoint_url": "http://85251281.ngrok.io/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A", "webhook_id": "1806556"}, "api_url": "https://www.eventbriteapi.com/v3/orders/1034611193/"}', N'52.201.4.8', 1, 0, 1, CAST(N'2019-08-30 20:24:57.440' AS DateTime), NULL, NULL, NULL, NULL, N'ffdf5331-8389-4680-813d-fbc192740cac')
GO
INSERT [dbo].[SalesPlatformWebhookRequests] ([Id], [Uid], [SalesPlatformId], [CreateDate], [Endpoint], [Header], [Payload], [IpAddress], [IsProcessed], [IsProcessing], [ProcessingCount], [LastProcessingDate], [NextProcessingDate], [ProcessingErrorCode], [ProcessingErrorMessage], [ManualProcessingUserId], [SecurityStamp]) VALUES (8, N'0db8181a-e101-43c2-b539-d0bc756703cd', 1, CAST(N'2019-08-30 15:32:08.390' AS DateTime), N'http://localhost:43931/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A', N'Accept: text/plain
Accept-Encoding: gzip, deflate
Host: localhost:43931
User-Agent: Eventbrite Webhooks (Dilithium)
X-Eventbrite-Delivery: 1806556
X-Eventbrite-Event: attendee.updated
X-Forwarded-For: 52.201.128.93
X-Original-Host: 85251281.ngrok.io
ApplicationInsights-RequestTrackingTelemetryModule-RootRequest-Id: 9241c044-32e1-4c48-a96d-cef7c639a8fe
', N'{"config": {"action": "attendee.updated", "user_id": "311783287832", "endpoint_url": "http://85251281.ngrok.io/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A", "webhook_id": "1806556"}, "api_url": "https://www.eventbriteapi.com/v3/events/71059816825/attendees/1346369367/"}', N'52.201.128.93', 1, 0, 1, CAST(N'2019-08-30 20:24:57.960' AS DateTime), NULL, NULL, NULL, NULL, N'f5d0a647-3e08-49a6-bd7a-6c9dd0fca793')
GO
INSERT [dbo].[SalesPlatformWebhookRequests] ([Id], [Uid], [SalesPlatformId], [CreateDate], [Endpoint], [Header], [Payload], [IpAddress], [IsProcessed], [IsProcessing], [ProcessingCount], [LastProcessingDate], [NextProcessingDate], [ProcessingErrorCode], [ProcessingErrorMessage], [ManualProcessingUserId], [SecurityStamp]) VALUES (9, N'b7a81c15-33ff-42ba-ad3a-64f0d82991ea', 1, CAST(N'2019-08-30 15:32:08.437' AS DateTime), N'http://localhost:43931/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A', N'Accept: text/plain
Accept-Encoding: gzip, deflate
Host: localhost:43931
User-Agent: Eventbrite Webhooks (Dilithium)
X-Eventbrite-Delivery: 1806556
X-Eventbrite-Event: attendee.updated
X-Forwarded-For: 52.202.1.10
X-Original-Host: 85251281.ngrok.io
ApplicationInsights-RequestTrackingTelemetryModule-RootRequest-Id: 1ebf4307-452a-4572-9940-0d2c4be97526
', N'{"config": {"action": "attendee.updated", "user_id": "311783287832", "endpoint_url": "http://85251281.ngrok.io/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A", "webhook_id": "1806556"}, "api_url": "https://www.eventbriteapi.com/v3/events/71059816825/attendees/1346369365/"}', N'52.202.1.10', 1, 0, 1, CAST(N'2019-08-30 20:24:58.373' AS DateTime), NULL, NULL, NULL, NULL, N'b1b23528-f76e-4be0-99c4-6068cccc598a')
GO
INSERT [dbo].[SalesPlatformWebhookRequests] ([Id], [Uid], [SalesPlatformId], [CreateDate], [Endpoint], [Header], [Payload], [IpAddress], [IsProcessed], [IsProcessing], [ProcessingCount], [LastProcessingDate], [NextProcessingDate], [ProcessingErrorCode], [ProcessingErrorMessage], [ManualProcessingUserId], [SecurityStamp]) VALUES (10, N'69c19a07-a9bb-4e60-a302-51a3481a64df', 1, CAST(N'2019-08-30 15:32:09.193' AS DateTime), N'http://localhost:43931/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A', N'Accept: text/plain
Accept-Encoding: gzip, deflate
Host: localhost:43931
User-Agent: Eventbrite Webhooks (Dilithium)
X-Eventbrite-Delivery: 1806556
X-Eventbrite-Event: order.updated
X-Forwarded-For: 52.201.128.93
X-Original-Host: 85251281.ngrok.io
ApplicationInsights-RequestTrackingTelemetryModule-RootRequest-Id: 07b877f5-ba3a-434c-aeff-b1a683ea0308
', N'{"config": {"action": "order.updated", "user_id": "311783287832", "endpoint_url": "http://85251281.ngrok.io/api/v1.0/salesplatforms/eventbrite/C111D841-B5B2-4B74-8CB1-3E21D79C802A", "webhook_id": "1806556"}, "api_url": "https://www.eventbriteapi.com/v3/orders/1034611193/"}', N'52.201.128.93', 1, 0, 1, CAST(N'2019-08-30 20:24:59.100' AS DateTime), NULL, NULL, NULL, NULL, N'c62dfc19-a118-4a83-aa7a-4b8dd8009894')
GO
SET IDENTITY_INSERT [dbo].[SalesPlatformWebhookRequests] OFF
GO