--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE [dbo].[NegotiationConfigs]
ALTER COLUMN [TimeIntervalBetweenTurn] [time](7) NOT NULL
go
