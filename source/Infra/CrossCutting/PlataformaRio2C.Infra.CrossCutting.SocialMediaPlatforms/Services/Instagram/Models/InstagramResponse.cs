// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 08-15-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-15-2023
// ***********************************************************************
// <copyright file="InstagramResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Services.Instagram.Models
{
    public class InstagramResponse
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    #region Child Classes - In Use

    public class Data
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class User
    {
        [JsonProperty("biography")]
        public string Biography { get; set; }

        [JsonProperty("bio_links")]
        public List<BioLink> BioLinks { get; set; }

        [JsonProperty("fb_profile_biolink")]
        public object FbProfileBiolink { get; set; }

        [JsonProperty("biography_with_entities")]
        public BiographyWithEntities BiographyWithEntities { get; set; }

        [JsonProperty("blocked_by_viewer")]
        public bool BlockedByViewer { get; set; }

        [JsonProperty("restricted_by_viewer")]
        public object RestrictedByViewer { get; set; }

        [JsonProperty("country_block")]
        public bool CountryBlock { get; set; }

        [JsonProperty("eimu_id")]
        public string EimuId { get; set; }

        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }

        [JsonProperty("external_url_linkshimmed")]
        public string ExternalUrlLinkshimmed { get; set; }

        [JsonProperty("edge_followed_by")]
        public EdgeFollowedBy EdgeFollowedBy { get; set; }

        [JsonProperty("fbid")]
        public string Fbid { get; set; }

        [JsonProperty("followed_by_viewer")]
        public bool FollowedByViewer { get; set; }

        [JsonProperty("edge_follow")]
        public EdgeFollow EdgeFollow { get; set; }

        [JsonProperty("follows_viewer")]
        public bool FollowsViewer { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("group_metadata")]
        public object GroupMetadata { get; set; }

        [JsonProperty("has_ar_effects")]
        public bool HasArEffects { get; set; }

        [JsonProperty("has_clips")]
        public bool HasClips { get; set; }

        [JsonProperty("has_guides")]
        public bool HasGuides { get; set; }

        [JsonProperty("has_channel")]
        public bool HasChannel { get; set; }

        [JsonProperty("has_blocked_viewer")]
        public bool HasBlockedViewer { get; set; }

        [JsonProperty("highlight_reel_count")]
        public int HighlightReelCount { get; set; }

        [JsonProperty("has_requested_viewer")]
        public bool HasRequestedViewer { get; set; }

        [JsonProperty("hide_like_and_view_counts")]
        public bool HideLikeAndViewCounts { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_business_account")]
        public bool IsBusinessAccount { get; set; }

        [JsonProperty("is_professional_account")]
        public bool IsProfessionalAccount { get; set; }

        [JsonProperty("is_supervision_enabled")]
        public bool IsSupervisionEnabled { get; set; }

        [JsonProperty("is_guardian_of_viewer")]
        public bool IsGuardianOfViewer { get; set; }

        [JsonProperty("is_supervised_by_viewer")]
        public bool IsSupervisedByViewer { get; set; }

        [JsonProperty("is_supervised_user")]
        public bool IsSupervisedUser { get; set; }

        [JsonProperty("is_embeds_disabled")]
        public bool IsEmbedsDisabled { get; set; }

        [JsonProperty("is_joined_recently")]
        public bool IsJoinedRecently { get; set; }

        [JsonProperty("guardian_id")]
        public object GuardianId { get; set; }

        [JsonProperty("business_address_json")]
        public object BusinessAddressJson { get; set; }

        [JsonProperty("business_contact_method")]
        public string BusinessContactMethod { get; set; }

        [JsonProperty("business_email")]
        public object BusinessEmail { get; set; }

        [JsonProperty("business_phone_number")]
        public object BusinessPhoneNumber { get; set; }

        [JsonProperty("business_category_name")]
        public string BusinessCategoryName { get; set; }

        [JsonProperty("overall_category_name")]
        public object OverallCategoryName { get; set; }

        [JsonProperty("category_enum")]
        public string CategoryEnum { get; set; }

        [JsonProperty("category_name")]
        public string CategoryName { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("is_verified_by_mv4b")]
        public bool IsVerifiedByMv4b { get; set; }

        [JsonProperty("is_regulated_c18")]
        public bool IsRegulatedC18 { get; set; }

        [JsonProperty("edge_mutual_followed_by")]
        public EdgeMutualFollowedBy EdgeMutualFollowedBy { get; set; }

        [JsonProperty("pinned_channels_list_count")]
        public int PinnedChannelsListCount { get; set; }

        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; set; }

        [JsonProperty("profile_pic_url_hd")]
        public string ProfilePicUrlHd { get; set; }

        [JsonProperty("requested_by_viewer")]
        public bool RequestedByViewer { get; set; }

        [JsonProperty("should_show_category")]
        public bool ShouldShowCategory { get; set; }

        [JsonProperty("should_show_public_contacts")]
        public bool ShouldShowPublicContacts { get; set; }

        [JsonProperty("show_account_transparency_details")]
        public bool ShowAccountTransparencyDetails { get; set; }

        [JsonProperty("transparency_label")]
        public object TransparencyLabel { get; set; }

        [JsonProperty("transparency_product")]
        public string TransparencyProduct { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("connected_fb_page")]
        public object ConnectedFbPage { get; set; }

        [JsonProperty("pronouns")]
        public List<object> Pronouns { get; set; }

        [JsonProperty("edge_felix_video_timeline")]
        public EdgeFelixVideoTimeline EdgeFelixVideoTimeline { get; set; }

        [JsonProperty("edge_owner_to_timeline_media")]
        public EdgeOwnerToTimelineMedia EdgeOwnerToTimelineMedia { get; set; }

        [JsonProperty("edge_saved_media")]
        public EdgeSavedMedia EdgeSavedMedia { get; set; }

        [JsonProperty("edge_media_collections")]
        public EdgeMediaCollections EdgeMediaCollections { get; set; }

        [JsonProperty("edge_related_profiles")]
        public EdgeRelatedProfiles EdgeRelatedProfiles { get; set; }
    }

    public class Edge
    {
        [JsonProperty("node")]
        public Node Node { get; set; }
    }

    public class Node
    {
        [JsonProperty("__typename")]
        public string Typename { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("shortcode")]
        public string Shortcode { get; set; }

        [JsonProperty("dimensions")]
        public Dimensions Dimensions { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("edge_media_to_tagged_user")]
        public EdgeMediaToTaggedUser EdgeMediaToTaggedUser { get; set; }

        [JsonProperty("fact_check_overall_rating")]
        public object FactCheckOverallRating { get; set; }

        [JsonProperty("fact_check_information")]
        public object FactCheckInformation { get; set; }

        [JsonProperty("gating_info")]
        public object GatingInfo { get; set; }

        [JsonProperty("sharing_friction_info")]
        public SharingFrictionInfo SharingFrictionInfo { get; set; }

        [JsonProperty("media_overlay_info")]
        public object MediaOverlayInfo { get; set; }

        [JsonProperty("media_preview")]
        public string MediaPreview { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("is_video")]
        public bool IsVideo { get; set; }

        [JsonProperty("has_upcoming_event")]
        public bool HasUpcomingEvent { get; set; }

        [JsonProperty("accessibility_caption")]
        public object AccessibilityCaption { get; set; }

        [JsonProperty("dash_info")]
        public DashInfo DashInfo { get; set; }

        [JsonProperty("has_audio")]
        public bool HasAudio { get; set; }

        [JsonProperty("tracking_token")]
        public string TrackingToken { get; set; }

        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }

        [JsonProperty("video_view_count")]
        public int VideoViewCount { get; set; }

        [JsonProperty("edge_media_to_caption")]
        public EdgeMediaToCaption EdgeMediaToCaption { get; set; }

        [JsonProperty("edge_media_to_comment")]
        public EdgeMediaToComment EdgeMediaToComment { get; set; }

        [JsonProperty("comments_disabled")]
        public bool CommentsDisabled { get; set; }

        [JsonProperty("taken_at_timestamp")]
        public int TakenAtTimestamp { get; set; }

        [JsonProperty("edge_liked_by")]
        public EdgeLikedBy EdgeLikedBy { get; set; }

        [JsonProperty("edge_media_preview_like")]
        public EdgeMediaPreviewLike EdgeMediaPreviewLike { get; set; }

        [JsonProperty("location")]
        public object Location { get; set; }

        [JsonProperty("nft_asset_info")]
        public object NftAssetInfo { get; set; }

        [JsonProperty("thumbnail_src")]
        public string ThumbnailSrc { get; set; }

        [JsonProperty("thumbnail_resources")]
        public List<ThumbnailResource> ThumbnailResources { get; set; }

        [JsonProperty("felix_profile_grid_crop")]
        public object FelixProfileGridCrop { get; set; }

        [JsonProperty("coauthor_producers")]
        public List<object> CoauthorProducers { get; set; }

        [JsonProperty("pinned_for_users")]
        public List<object> PinnedForUsers { get; set; }

        [JsonProperty("viewer_can_reshare")]
        public bool ViewerCanReshare { get; set; }

        [JsonProperty("encoding_status")]
        public object EncodingStatus { get; set; }

        [JsonProperty("is_published")]
        public bool IsPublished { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("video_duration")]
        public double VideoDuration { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("clips_music_attribution_info")]
        public ClipsMusicAttributionInfo ClipsMusicAttributionInfo { get; set; }

        [JsonProperty("edge_sidecar_to_children")]
        public EdgeSidecarToChildren EdgeSidecarToChildren { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("profile_pic_url")]
        public string ProfilePicUrl { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }

    public class EdgeOwnerToTimelineMedia
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("page_info")]
        public PageInfo PageInfo { get; set; }

        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }
    }

    #endregion

    #region Child Classes - Not In Use

    public class BiographyWithEntities
    {
        [JsonProperty("raw_text")]
        public string RawText { get; set; }

        [JsonProperty("entities")]
        public List<object> Entities { get; set; }
    }

    public class BioLink
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("lynx_url")]
        public string LynxUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("link_type")]
        public string LinkType { get; set; }
    }

    public class ClipsMusicAttributionInfo
    {
        [JsonProperty("artist_name")]
        public string ArtistName { get; set; }

        [JsonProperty("song_name")]
        public string SongName { get; set; }

        [JsonProperty("uses_original_audio")]
        public bool UsesOriginalAudio { get; set; }

        [JsonProperty("should_mute_audio")]
        public bool ShouldMuteAudio { get; set; }

        [JsonProperty("should_mute_audio_reason")]
        public string ShouldMuteAudioReason { get; set; }

        [JsonProperty("audio_id")]
        public string AudioId { get; set; }
    }

    public class DashInfo
    {
        [JsonProperty("is_dash_eligible")]
        public bool IsDashEligible { get; set; }

        [JsonProperty("video_dash_manifest")]
        public object VideoDashManifest { get; set; }

        [JsonProperty("number_of_qualities")]
        public int NumberOfQualities { get; set; }
    }

    public class Dimensions
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    public class EdgeFelixVideoTimeline
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("page_info")]
        public PageInfo PageInfo { get; set; }

        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }
    }

    public class EdgeFollow
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class EdgeFollowedBy
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class EdgeLikedBy
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class EdgeMediaCollections
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("page_info")]
        public PageInfo PageInfo { get; set; }

        [JsonProperty("edges")]
        public List<object> Edges { get; set; }
    }

    public class EdgeMediaPreviewLike
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class EdgeMediaToCaption
    {
        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }
    }

    public class EdgeMediaToComment
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class EdgeMediaToTaggedUser
    {
        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }
    }

    public class EdgeMutualFollowedBy
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("edges")]
        public List<object> Edges { get; set; }
    }

    public class EdgeRelatedProfiles
    {
        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }
    }

    public class EdgeSavedMedia
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("page_info")]
        public PageInfo PageInfo { get; set; }

        [JsonProperty("edges")]
        public List<object> Edges { get; set; }
    }

    public class EdgeSidecarToChildren
    {
        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }
    }

    public class Owner
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }

    public class PageInfo
    {
        [JsonProperty("has_next_page")]
        public bool HasNextPage { get; set; }

        [JsonProperty("end_cursor")]
        public string EndCursor { get; set; }
    }

    public class SharingFrictionInfo
    {
        [JsonProperty("should_have_sharing_friction")]
        public bool ShouldHaveSharingFriction { get; set; }

        [JsonProperty("bloks_app_url")]
        public object BloksAppUrl { get; set; }
    }

    public class ThumbnailResource
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("config_width")]
        public int ConfigWidth { get; set; }

        [JsonProperty("config_height")]
        public int ConfigHeight { get; set; }
    }

    #endregion
}