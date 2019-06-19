using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    public class Event : Entity
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 50;

        public string Name { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public virtual Quiz Quiz { get; private set; }

        protected Event()
        {

        }

        public Event(string name)
        {
            Name = name;
        }


        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }

        public void SetEndDate(DateTime endDate)
        {
            EndDate = endDate;
        }


        public override bool IsValid()
        {
            return true;
        }
    }
}
