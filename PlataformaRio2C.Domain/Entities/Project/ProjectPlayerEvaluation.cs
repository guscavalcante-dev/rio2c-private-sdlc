using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    public class ProjectPlayerEvaluation: Entity
    {
        public static readonly int ReasonMinLength = 3;
        public static readonly int ReasonMaxLength = 1500;
        public int ProjectPlayerId { get; private set; }
        public virtual ProjectPlayer ProjectPlayer { get; private set; }

        public int StatusId { get; private set; }
        public virtual ProjectStatus Status { get; private set; }       

        public int EvaluationUserId { get; private set; }
        public virtual User EvaluationdUser { get; private set; }

        public string Reason { get; private set; }


        protected ProjectPlayerEvaluation()
        {

        }

        public ProjectPlayerEvaluation(ProjectPlayer projectPlayer, ProjectStatus status, User evaluationdUser)
        {
            SetProjectPlayer(projectPlayer);
            SetProjectStatus(status);
            SetEvaluationdUser(evaluationdUser);
        }

        public void SetProjectPlayer(ProjectPlayer projectPlayer)
        {
            ProjectPlayer = projectPlayer;

            if (projectPlayer != null)
            {
                ProjectPlayerId = projectPlayer.Id;
            }
        }

        public void SetProjectStatus(ProjectStatus status)
        {
            Status = status;

            if (status != null)
            {
                StatusId = status.Id;
            }
        }

        public void SetEvaluationdUser(User evaluationdUser)
        {
            EvaluationdUser = evaluationdUser;

            if (evaluationdUser != null)
            {
                EvaluationUserId = evaluationdUser.Id;
            }
        }

        public void SetReason(string value)
        {
            Reason = value;
        }

        public override bool IsValid()
        {

            ValidationResult = new ValidationResult();

            if (this.Status != null && this.Status.Code  != null && this.Status.Code == StatusProjectCodes.Rejected.ToString())
            {
                ValidationResult.Add(new ProjectPlayerEvaluationRecuseIsConsistent().Valid(this));
            }

            return ValidationResult.IsValid;
        }
    }
}
