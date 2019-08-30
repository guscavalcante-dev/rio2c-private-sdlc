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
					END
				END
		END
END