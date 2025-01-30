using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class MusicBusinessRoundProjectExpectationsForMeeting : Entity
    {
        public static readonly int ValueMaxLength = 3000;
        public static readonly int ValueMinLength = 1;


        public int MusicBusinessRoundProjectId { get; private set; }
        public int LanguageId { get; private set; }
        public string Value { get; private set; }

        public virtual MusicBusinessRoundProject MusicBusinessRoundProject { get; private set; }
        public virtual Language Language { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectExpectationsForMeeting"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="language">The language.</param>
        /// <param name="userId">The user identifier.</param>
        public MusicBusinessRoundProjectExpectationsForMeeting(string value, Language language, int userId)
        {
            this.Value = value?.Trim();
            this.Language = language;
            this.LanguageId = language?.Id ?? 0;

            base.SetCreateDate(userId);
        }

        public MusicBusinessRoundProjectExpectationsForMeeting()
        {
        }

        /// <summary>Updates the expectation for meeting.</summary>
        /// <param name="expectationForMeeting">The expectation for meeting.</param>
        public void Update(MusicBusinessRoundProjectExpectationsForMeeting expectationForMeeting)
        {
            this.Value = expectationForMeeting.Value?.Trim();
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = expectationForMeeting.UpdateUserId;
        }

        #region Validations

        public override bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateValue();

            return this.ValidationResult.IsValid;
        }

        private void ValidateValue()
        {
            if (this.Value?.Trim().Length < ValueMinLength || this.Value?.Trim().Length > ValueMaxLength)
            {
                this.ValidationResult.Add(new ValidationError(
                    string.Format(Messages.PropertyBetweenLengths, Labels.ExpectationsForMeeting, ValueMaxLength, ValueMinLength),
                    new string[] { "Value" }));
            }
        }

        #endregion
    }
}
