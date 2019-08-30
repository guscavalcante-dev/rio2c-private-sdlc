BEGIN
    DECLARE @db varchar(50);
 
    SELECT @db = Upper(DB_NAME())
	
	

    IF charindex('_DEV', @db) > 1
		BEGIN
			--é DEV
			UPDATE AttendeeSalesPlatforms SET SalesPlatformEventId = '71059816825'

			UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '127470921' WHERE Id = 1
			UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '127470917' WHERE Id = 2
			UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '127470923' WHERE Id = 5
			UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '127470919' WHERE Id = 6

			UPDATE SalesPlatforms SET ApiKey = 'MXYRD6WLR6C537BWR3'

			UPDATE SalesPlatforms set WebhookSecurityKey = 'C111D841-B5B2-4B74-8CB1-3E21D79C802A'
		END
	ELSE
		BEGIN
			IF charindex('_TEST', @db) > 1
				BEGIN
					--é TEST
					UPDATE AttendeeSalesPlatforms SET SalesPlatformEventId = '71060007395'

					UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '127471171' WHERE Id = 1
					UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '127471167' WHERE Id = 2
					UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '127471173' WHERE Id = 5
					UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '127471169' WHERE Id = 6

					UPDATE SalesPlatforms SET ApiKey = 'WHNIXWOU7SRIBWS4FI'

					UPDATE SalesPlatforms set WebhookSecurityKey = '9E447D9E-8B35-406E-AE1D-EAD26ED6D8AF'
				END
			ELSE
				BEGIN
					IF charindex('_PROD', @db) > 1
					BEGIN
						--é PROD
						UPDATE AttendeeSalesPlatforms SET SalesPlatformEventId = '63245927271'

						UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '115642573' WHERE Id = 1
						UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '115507795' WHERE Id = 2
						UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '115642751' WHERE Id = 5
						UPDATE AttendeeSalesPlatformTicketTypes SET TicketClassId = '115508710' WHERE Id = 6

						UPDATE SalesPlatforms SET ApiKey = '2DSXISHPWOYPWT2XAS'

						UPDATE SalesPlatforms set WebhookSecurityKey = '3718D5BD-D3D3-4E8F-A3E0-5270C5830AF2'
					END
				END
		END
END