// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2021
// ***********************************************************************
// <copyright file="PlataformaRio2CContext.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.Data.Context.Config;
using PlataformaRio2C.Infra.Data.Context.Mapping;
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
        }

        /// <summary>Initializes a new instance of the <see cref="PlataformaRio2CContext"/> class.</summary>
        public PlataformaRio2CContext()
            : base("PlataformaRio2CConnection")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
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

            // Projects
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

            // Negotiations
            modelBuilder.Configurations.Add(new NegotiationMap());
            modelBuilder.Configurations.Add(new NegotiationConfigMap());
            modelBuilder.Configurations.Add(new NegotiationRoomConfigMap());

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
            modelBuilder.Configurations.Add(new InnovationOrganizationOptionMap());
            modelBuilder.Configurations.Add(new InnovationOptionMap());
            modelBuilder.Configurations.Add(new InnovationOptionGroupMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationMap());
            modelBuilder.Configurations.Add(new AttendeeInnovationOrganizationCollaboratorMap());
            modelBuilder.Configurations.Add(new WorkDedicationMap());

            // TODO: Old mapping that must be reviewed

            //modelBuilder.Configurations.Add(new ImageFileMap());            
            //modelBuilder.Configurations.Add(new PlayerMap());
            //modelBuilder.Configurations.Add(new PlayerDescriptionMap());
            //modelBuilder.Configurations.Add(new UserUseTermMap());
            //modelBuilder.Configurations.Add(new PlayerInterestMap());            
            //modelBuilder.Configurations.Add(new PlayerTargetAudienceMap());
            //modelBuilder.Configurations.Add(new PlayerRestrictionsSpecificsMap());
            //modelBuilder.Configurations.Add(new ProducerMap());
            //modelBuilder.Configurations.Add(new ProducerDescriptionMap());
            //modelBuilder.Configurations.Add(new ProducerEventMap());
            //modelBuilder.Configurations.Add(new CollaboratorProducerMap());
            //modelBuilder.Configurations.Add(new ProjectPlayerMap());
            //modelBuilder.Configurations.Add(new ProjectStatusMap());
            //modelBuilder.Configurations.Add(new ProjectPlayerEvaluationMap());

            //modelBuilder.Configurations.Add(new MailMap());

            //modelBuilder.Configurations.Add(new ConferenceLecturerMap());
            //modelBuilder.Configurations.Add(new LecturerMap());

            //modelBuilder.Configurations.Add(new RoleLecturerMap());
            //modelBuilder.Configurations.Add(new RoleLecturerTitleMap());

            modelBuilder.Configurations.Add(new QuizMap());
            modelBuilder.Configurations.Add(new QuizQuestionMap());
            modelBuilder.Configurations.Add(new QuizOptionMap());
            modelBuilder.Configurations.Add(new QuizAnswerMap());

            //modelBuilder.Configurations.Add(new SpeakerMap());

            //modelBuilder.Configurations.Add(new MusicalCommissionMap());

            //modelBuilder.Configurations.Add(new UserRoleMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserRole> UsersRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AttendeeCollaborator> AttendeeCollaborators { get; set; }
        public DbSet<AttendeeCollaboratorType> AttendeeCollaboratorTypes { get; set; }
        public DbSet<CollaboratorType> CollaboratorTypes { get; set; }



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

        // Users
        public DbSet<User> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }

        // TODO: Old dbsets that must be reviewed
        public DbSet<Language> Languages { get; set; }
        //public DbSet<ImageFile> ImageFiles { get; set; }
        //public DbSet<Player> Players { get; set; }
        //public DbSet<UserUseTerm> UserUseTerms { get; set; }
        public DbSet<InterestGroup> InterestGroups { get; set; }
        public DbSet<Interest> Interests { get; set; }
        //public DbSet<PlayerInterest> PlayerInterests { get; set; }        
        //public DbSet<Producer> Producers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }

        public DbSet<Room> Rooms { get; set; }

        //public DbSet<RoleLecturer> RoleLecturers { get; set; }

        //public DbSet<Mail> Mail { get; set; }
        //public DbSet<MailCollaborator> MailCollaborators { get; set; }

        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }

        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<QuizQuestion> QuizQuestion { get; set; }
        public DbSet<QuizOption> QuizOption { get; set; }
        public DbSet<QuizAnswer> QuizAnswer { get; set; }

        //public DbSet<Speaker> Speaker { get; set; }

        //public DbSet<MusicalCommission> MusicalCommission { get; set; }

        public DbSet<SalesPlatform> SalesPlatforms { get; set; }
        public DbSet<SalesPlatformWebhookRequest> SalesPlatformWebhookRequests { get; set; }
    }
}