using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class Interest : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 150;

        public int InterestGroupId { get; private set; }
        public virtual Guid InterestGroupUid { get; private set; }
        public virtual InterestGroup InterestGroup { get; private set; }

        public string Name { get; private set; }

        protected Interest()
        {

        }
        
        public Interest(string name, InterestGroup interestGroup)
        {
            SetName(name);
            SetInterestGroup(interestGroup);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetInterestGroup(InterestGroup interestGroup)
        {
            InterestGroup = interestGroup;
            if (interestGroup != null)
            {
                InterestGroupId = interestGroup.Id;
                InterestGroupUid = interestGroup.Uid;
            }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
