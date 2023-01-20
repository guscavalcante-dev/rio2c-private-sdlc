// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 06-29-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-04-2023
// ***********************************************************************
// <copyright file="IAttendeeInnovationOrganizationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Domain.Interfaces
{
    /// <summary>IAttendeeInnovationOrganizationRepository</summary>
    public interface IAttendeeInnovationOrganizationRepository : IRepository<AttendeeInnovationOrganization>
    {
        Task<IPagedList<AttendeeInnovationOrganizationDto>> FindAllDtosPagedAsync(int editionId, string searchKeywords, List<Guid?> innovationOrganizationTrackOptionUids, Guid? evaluationStatusUid, int page, int pageSize);
        Task<IPagedList<AttendeeInnovationOrganizationJsonDto>> FindAllJsonDtosPagedAsync(int editionId, string searchKeywords, List<Guid?> innovationOrganizationTrackOptionGroupUids, Guid? evaluationStatusUid, int page, int pageSize, List<Tuple<string, string>> sortColumns);
        Task<AttendeeInnovationOrganizationDto> FindDtoToEvaluateAsync(Guid attendeeInnovationOrganizationUid);
        Task<AttendeeInnovationOrganizationDto> FindDtoToEvaluateAsync(int attendeeInnovationOrganizationId);
        Task<AttendeeInnovationOrganizationDto> FindMainInformationWidgetDtoAsync(Guid attendeeInnovationOrganizationUid);
        Task<AttendeeInnovationOrganizationDto> FindBusinessInformationWidgetDtoAsync(Guid attendeeInnovationOrganizationUid);
        Task<AttendeeInnovationOrganizationDto> FindTracksWidgetDtoAsync(Guid attendeeInnovationOrganizationUid);
        Task<AttendeeInnovationOrganizationDto> FindObjectivesWidgetDtoAsync(Guid attendeeInnovationOrganizationUid);
        Task<AttendeeInnovationOrganizationDto> FindExperiencesWidgetDtoAsync(Guid attendeeInnovationOrganizationUid);
        Task<AttendeeInnovationOrganizationDto> FindTechnologiesWidgetDtoAsync(Guid attendeeInnovationOrganizationUid);
        Task<AttendeeInnovationOrganizationDto> FindEvaluationGradeWidgetDtoAsync(Guid attendeeInnovationOrganizationUid, int userId);
        Task<AttendeeInnovationOrganizationDto> FindEvaluatorsWidgetDtoAsync(Guid attendeeInnovationOrganizationUid);
        Task<AttendeeInnovationOrganizationDto> FindFoundersWidgetDtoAsync(Guid attendeeInnovationOrganizationUid);
        Task<AttendeeInnovationOrganizationDto> FindPresentationWidgetDtoAsync(Guid attendeeInnovationOrganizationUid);
        Task<AttendeeInnovationOrganization> FindByIdAsync(int attendeeInnovationOrganizationId);
        Task<AttendeeInnovationOrganization> FindByUidAsync(Guid attendeeInnovationOrganizationUid);
        Task<List<AttendeeInnovationOrganization>> FindAllByIdsAsync(List<int?> attendeeInnovationOrganizationIds);
        Task<List<AttendeeInnovationOrganization>> FindAllByUidsAsync(List<Guid?> attendeeInnovationOrganizationUids);
        Task<AttendeeInnovationOrganization> FindByDocumentAndEditionIdAsync(string document, int editionId);
        Task<List<AttendeeInnovationOrganization>> FindAllByEditionIdAsync(int editionId);
        Task<int[]> FindAllApprovedAttendeeInnovationOrganizationsIdsAsync(int editionId);
        Task<int[]> FindAllInnovationOrganizationsIdsPagedAsync(int editionId, string searchKeywords, List<Guid?> innovationOrganizationTrackOptionGroupUids, Guid? evaluationStatusUid, int page, int pageSize);
        Task<int> CountAsync(int editionId, bool showAllEditions = false);
        Task<int> CountPagedAsync(int editionId, string searchKeywords, List<Guid?> innovationOrganizationTrackOptionGroupUids, Guid? evaluationStatusUid, int page, int pageSize);
        Task<List<InnovationOrganizationGroupedByTrackDto>> FindEditionCountPieWidgetDto(int editionId);

        Task<AttendeeInnovationOrganizationDto> FindSustainableDevelopmentWidgetDtoAsync(Guid attendeeInnovationOrganizationUid);
    }
}