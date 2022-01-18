ALTER TABLE AttendeeInnovationOrganizations
ADD WouldYouLikeParticipateBusinessRound BIT NULL DEFAULT 0,
AccumulatedRevenueForLastTwelveMonths DECIMAL(12,2) NULL,
BusinessFoundationYear INT NULL;