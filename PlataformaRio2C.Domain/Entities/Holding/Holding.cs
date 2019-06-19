using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Entities
{
    public class Holding : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 100;
        public static readonly double ImageMinMByteSize = 0.0009765625;

        public string Name { get; private set; }

        public int? ImageId { get; private set; }
        public virtual ImageFile Image{ get; private set; }

        public virtual ICollection<HoldingDescription> Descriptions { get; private set; }

        protected Holding()
        {
            
        }

        public Holding(string name)
        {
            Descriptions = new List<HoldingDescription>();
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetImage(ImageFile image)
        {
            ImageId = null;
            Image = image;
            if (image != null)
            {
                ImageId = image.Id;
            }            
        }

        public void SetDescriptions(IEnumerable<HoldingDescription> descriptions)
        {
            Descriptions = descriptions.ToList();
        }

        public void AddDescription(HoldingDescription description)
        {
            Descriptions.Add(description);
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidationResult();

            ValidationResult.Add(new HoldingIsConsistent().Valid(this));         

            if (Image != null)
            {
                ValidationResult.Add(new ImageIsConsistent().Valid(this.Image));
                ValidationResult.Add(new HoldingImageIsConsistent().Valid(this));
            }

            return ValidationResult.IsValid;
        }
    }
}
