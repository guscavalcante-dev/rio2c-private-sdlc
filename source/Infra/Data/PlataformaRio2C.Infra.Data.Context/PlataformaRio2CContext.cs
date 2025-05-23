﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-19-2025
// ***********************************************************************
// <copyright file="PlataformaRio2CContext.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Config;
using PlataformaRio2C.Infra.Data.Context.Mapping;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PlataformaRio2C.Infra.Data.Context
{
    /// <summary>PlataformaRio2CContext</summary>
    public class PlataformaRio2CContext : BaseContext
    {
        /// <summary>Initializes the <see cref="PlataformaRio2CContext"/> class.</summary>
        static PlataformaRio2CContext()
        {
            Database.SetInitializer<PlataformaRio2CContext>(null);

            string environment = ConfigurationManager.AppSettings["Environment"]?.ToLower();
            if (environment == EnumEnvironments.Test.ToDescription().ToLower() ||
                environment == EnumEnvironments.Prod.ToDescription().ToLower())
            {
                // Disables the Migrations
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<PlataformaRio2CContext, Migrations.Configuration>());
                //using (var context = new PlataformaRio2CContext())
                //{
                //    context.Database.Initialize(true);
                //}

                // Enables Seeder
                Seeder.Seed(ConfigurationManager.ConnectionStrings["PlataformaRio2CConnection"].ConnectionString, 
                    autoRun: true, 
                    stopOnException: true);
            }
            else
            {
                Database.SetInitializer<PlataformaRio2CContext>(null);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlataformaRio2CContext" /> class.
        /// </summary>
        public PlataformaRio2CContext() : base("PlataformaRio2CConnection")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlataformaRio2CContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public PlataformaRio2CContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        /// <summary>Called when [model creating].</summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new ConnectionMap());

            // Edition
            modelBuilder.Configurations.Add(new EditionMap());
            modelBuilder.Configurations.Add(new EditionEventMap());

            // Holding
            modelBuilder.Configurations.Add(new HoldingMap());
            modelBuilder.Configurations.Add(new HoldingDescriptionMap());

            // Organization
            modelBuilder.Configurations.Add(new OrganizationMap());
            modelBuilder.Configurations.Add(new OrganizationDescriptionMap());
            modelBuilder.Configurations.Add(new OrganizationRestrictionSpecificMap());
            modelBuilder.Configurations.Add(new AttendeeOrganizationMap());
            modelBuilder.Configurations.Add(new AttendeeOrganizationTypeMap());
            modelBuilder.Configurations.Add(new AttendeeOrganizationCollaboratorMap());
            modelBuilder.Configurations.Add(new OrganizationTypeMap());
            modelBuilder.Configurations.Add(new OrganizationActivityMap());
            modelBuilder.Configurations.Add(new OrganizationTargetAudienceMap());
            modelBuilder.Configurations.Add(new OrganizationInterestMap());

            // Collaborator
            modelBuilder.Configurations.Add(new CollaboratorMap());
            modelBuilder.Configurations.Add(new CollaboratorJobTitleMap());
            modelBuilder.Configurations.Add(new CollaboratorMiniBioMap());
            modelBuilder.Configurations.Add(new CollaboratorTypeMap());
            modelBuilder.Configurations.Add(new AttendeeCollaboratorMap());
            modelBuilder.Configurations.Add(new AttendeeCollaboratorTypeMap());
            modelBuilder.Configurations.Add(new AttendeeCollaboratorTicketMap());
            modelBuilder.Configurations.Add(new CollaboratorGenderMap());
            modelBuilder.Configurations.Add(new CollaboratorRoleMap());
            modelBuilder.Configurations.Add(new CollaboratorIndustryMap());
            modelBuilder.Configurations.Add(new CollaboratorEditionParticipationMap());
            modelBuilder.Configurations.Add(new AttendeeCollaboratorInterestMap());
            modelBuilder.Configurations.Add(new AttendeeCollaboratorActivityMap());
            modelBuilder.Configurations.Add(new AttendeeCollaboratorTargetAudienceMap());

            // Audiovisual
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new ProjectTypeMap());
            modelBuilder.Configurations.Add(new ProjectAdditionalInformationMap());
            modelBuilder.Configurations.Add(new ProjectInterestMap());
            modelBuilder.Configurations.Add(new ProjectTargetAudienceMap());
            modelBuilder.Configurations.Add(new ProjectImageLinkMap());
            modelBuilder.Configurations.Add(new ProjectTeaserLinkMap());
            modelBuilder.Configurations.Add(new ProjectLogLineMap());
            modelBuilder.Configurations.Add(new ProjectProductionPlanMap());
            modelBuilder.Configurations.Add(new ProjectSummaryMap());
            modelBuilder.Configurations.Add(new ProjectTitleMap());
            modelBuilder.Configurations.Add(new ProjectBuyerEvaluationMap());
            modelBuilder.Configurations.Add(new ProjectEvaluationStatusMap());
            modelBuilder.Configurations.Add(new ProjectEvaluationRefuseReasonMap());
            modelBuilder.Configurations.Add(new CommissionEvaluationMap());
            modelBuilder.Configurations.Add(new ProjectModalityMap());

            // Addresses
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new StateMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new AddressMap());

            // Sales Platforms
            modelBuilder.Configurations.Add(new SalesPlatformMap());
            modelBuilder.Configurations.Add(new SalesPlatformWebhookRequestMap());
            modelBuilder.Configurations.Add(new AttendeeSalesPlatformMap());
            modelBuilder.Configurations.Add(new AttendeeSalesPlatformTicketTypeMap());

            // Sent emails
            modelBuilder.Configurations.Add(new SentEmailMap());

            // Subscribe lists
            modelBuilder.Configurations.Add(new SubscribeListMap());
            modelBuilder.Configurations.Add(new UserUnsubscribedListMap());

            // Networks
            modelBuilder.Configurations.Add(new MessageMap());

            // Conferences
            modelBuilder.Configurations.Add(new ConferenceMap());
            modelBuilder.Configurations.Add(new ConferenceTitleMap());
            modelBuilder.Configurations.Add(new ConferenceSynopsisMap());
            modelBuilder.Configurations.Add(new ConferenceParticipantMap());
            modelBuilder.Configurations.Add(new ConferenceParticipantRoleMap());
            modelBuilder.Configurations.Add(new ConferenceParticipantRoleTitleMap());
            modelBuilder.Configurations.Add(new ConferenceTrackMap());
            modelBuilder.Configurations.Add(new ConferencePillarMap());
            modelBuilder.Configurations.Add(new ConferencePresentationFormatMap());
            modelBuilder.Configurations.Add(new ConferenceDynamicMap());

            // Negotiations
            modelBuilder.Configurations.Add(new NegotiationMap());
            modelBuilder.Configurations.Add(new NegotiationConfigMap());
            modelBuilder.Configurations.Add(new NegotiationRoomConfigMap());
            modelBuilder.Configurations.Add(new AttendeeNegotiationCollaboratorMap());

            // Music
            modelBuilder.Configurations.Add(new MusicBandMap());
            modelBuilder.Configurations.Add(new MusicBandTypeMap());
            modelBuilder.Configurations.Add(new AttendeeMusicBandMap());
            modelBuilder.Configurations.Add(new AttendeeMusicBandCollaboratorMap());
            modelBuilder.Configurations.Add(new AttendeeMusicBandEvaluationMap());
            modelBuilder.Configurations.Add(new MusicProjectMap());
            modelBuilder.Configurations.Add(new MusicGenreMap());
            modelBuilder.Configurations.Add(new MusicBandGenreMap());
            modelBuilder.Configurations.Add(new MusicBandTargetAudienceMap());
            modelBuilder.Configurations.Add(new MusicBandMemberMap());
            modelBuilder.Configurations.Add(new MusicBandTeamMemberMap());
            modelBuilder.Configurations.Add(new ReleasedMusicProjectMap());

            // Music BusinessRound Projects
            modelBuilder.Configurations.Add(new MusicBusinessRoundProjectMap());
            modelBuilder.Configurations.Add(new MusicBusinessRoundProjectActivityMap());
            modelBuilder.Configurations.Add(new MusicBusinessRoundProjectInterestMap());
            modelBuilder.Configurations.Add(new MusicBusinessRoundProjectPlayerCategoryMap());
            modelBuilder.Configurations.Add(new MusicBusinessRoundProjectTargetAudienceMap());
            modelBuilder.Configurations.Add(new MusicBusinessRoundProjectBuyerEvaluationMap());
            modelBuilder.Configurations.Add(new MusicBusinessRoundProjectExpectationsForMeetingMap());
            modelBuilder.Configurations.Add(new MusicBusinessRoundNegotiationMap());
            modelBuilder.Configurations.Add(new AttendeeMusicBusinessRoundNegotiationCollaboratorMap());


            // Common
            modelBuilder.Configurations.Add(new ActivityMap());
            modelBuilder.Configurations.Add(new TargetAudienceMap());
            modelBuilder.Configurations.Add(new InterestGroupMap());
            modelBuilder.Configurations.Add(new InterestMap());
            modelBuilder.Configurations.Add(new LanguageMap());
            modelBuilder.Configurations.Add(new RoomMap());
            modelBuilder.Configurations.Add(new RoomNameMap());
            modelBuilder.Configurations.Add(new TrackMap());
            modelBuilder.Configurations.Add(new PresentationFormatMap());
            modelBuilder.Configurations.Add(new PillarMap());
            modelBuilder.Configurations.Add(new PlayerCategoryMap());

            // Logistics Configuration
            modelBuilder.Configurations.Add(new LogisticMap());
            modelBuilder.Configurations.Add(new LogisticAirfareMap());
            modelBuilder.Configurations.Add(new LogisticAccommodationMap());
            modelBuilder.Configurations.Add(new LogisticTransferMap());
            modelBuilder.Configurations.Add(new PlaceMap());
            modelBuilder.Configurations.Add(new AttendeePlacesMap());
            modelBuilder.Configurations.Add(new LogisticsSponsorMap());
            modelBuilder.Configurations.Add(new AttendeeLogisticSponsorMap());

            // Innovation
            modelBuilder.Configurations.Add(new InnovationOrganizationMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationCollaboratorMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationFounderMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationCompetitorMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationExperienceMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationObjectiveMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationTechnologyMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationTrackMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationSustainableDevelopmentObjectiveMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationEvaluationMap());
            modelBuilder.Configurations.Add(new AttendeeCollaboratorInnovationOrganizationTrackMap());
            modelBuilder.Configurations.Add(new InnovationOrganizationExperienceOptionMap());
            modelBuilder.Configurations.Add(new InnovationOrganizationObjectivesOptionMap());
            modelBuilder.Configurations.Add(new InnovationOrganizationTechnologyOptionMap());
            modelBuilder.Configurations.Add(new InnovationOrganizationTrackOptionMap());
            modelBuilder.Configurations.Add(new InnovationOrganizationTrackOptionGroupMap());
            modelBuilder.Configurations.Add(new InnovationOrganizationSustainableDevelopmentObjectivesOptionMap());
            modelBuilder.Configurations.Add(new WorkDedicationMap());

            // Quiz
            modelBuilder.Configurations.Add(new QuizMap());
            modelBuilder.Configurations.Add(new QuizQuestionMap());
            modelBuilder.Configurations.Add(new QuizOptionMap());
            modelBuilder.Configurations.Add(new QuizAnswerMap());

            // Cartoon
            modelBuilder.Configurations.Add(new CartoonProjectMap());
            modelBuilder.Configurations.Add(new CartoonProjectFormatMap());
            modelBuilder.Configurations.Add(new CartoonProjectCreatorMap());
            modelBuilder.Configurations.Add(new CartoonProjectOrganizationMap());
            modelBuilder.Configurations.Add(new AttendeeCartoonProjectMap());
            modelBuilder.Configurations.Add(new AttendeeCartoonProjectCollaboratorMap());
            modelBuilder.Configurations.Add(new AttendeeCartoonProjectEvaluationMap());

            // Creator
            modelBuilder.Configurations.Add(new CreatorProjectMap());
            modelBuilder.Configurations.Add(new CreatorProjectInterestMap());
            modelBuilder.Configurations.Add(new AttendeeCreatorProjectMap());
            modelBuilder.Configurations.Add(new AttendeeCreatorProjectEvaluationMap());

            // WeConnect
            modelBuilder.Configurations.Add(new WeConnectPublicationMap());
            modelBuilder.Configurations.Add(new SocialMediaPlatformMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserRole> UsersRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AttendeeCollaborator> AttendeeCollaborators { get; set; }
        public DbSet<AttendeeCollaboratorType> AttendeeCollaboratorTypes { get; set; }
        public DbSet<CollaboratorType> CollaboratorTypes { get; set; }

        // Cartoon

        public DbSet<CartoonProject> CartoonProjecs { get; set; }
        public DbSet<CartoonProjectFormat> CartoonProjectFormats { get; set; }
        public DbSet<CartoonProjectCreator> CartoonProjectCreators { get; set; }
        public DbSet<CartoonProjectOrganization> CartoonProjectOrganizations { get; set; }
        public DbSet<AttendeeCartoonProject> AttendeeCartoonProjects { get; set; }
        public DbSet<AttendeeCartoonProjectCollaborator> AttendeeCartoonProjectCollaborators { get; set; }
        public DbSet<AttendeeCartoonProjectEvaluation> AttendeeCartoonProjectEvaluations { get; set; }

        // Edition
        public DbSet<Edition> Editions { get; set; }

        // Holding
        public DbSet<Holding> Holdings { get; set; }
        public DbSet<HoldingDescription> HoldingDescriptions { get; set; } //TODO: Try to remove this (Holding Descriptions)

        // Organization
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<AttendeeOrganization> AttendeeOrganizations { get; set; } //TODO: Try to remove this (AttendeeOrganizations)

        // Collaborators
        public DbSet<Collaborator> Collaborators { get; set; }

        // Common
        public DbSet<Activity> Activities { get; set; }
        public DbSet<TargetAudience> TargetAudiences { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Language> Languages { get; set; }

        // Users
        public DbSet<User> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }

        // Projects
        public DbSet<InterestGroup> InterestGroups { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<AttendeeCollaboratorInterest> AttendeeCollaboratorInterests { get; set; }
        public DbSet<CommissionEvaluation> CommissionEvaluations { get; set; }

        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }

        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<QuizQuestion> QuizQuestion { get; set; }
        public DbSet<QuizOption> QuizOption { get; set; }
        public DbSet<QuizAnswer> QuizAnswer { get; set; }
        public DbSet<SalesPlatform> SalesPlatforms { get; set; }
        public DbSet<SalesPlatformWebhookRequest> SalesPlatformWebhookRequests { get; set; }
    }
}