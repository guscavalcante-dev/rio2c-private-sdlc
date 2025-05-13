// ***********************************************************************
// Assembly         : Jack.Core
// Author           : Rafael Dantas Ruiz
// Created          : 01-19-2014
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-19-2014
// ***********************************************************************
// <copyright file="Identity.cs" company="Jack Sistemas">
//     Copyright (c) Jack Sistemas. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Infra.CrossCutting.Tools.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    public class InvalidIdentityValueException : Exception
    {
        public InvalidIdentityValueException(string value, string identityTypeName)
            : base(string.Format("The value '{0}' isn't valid for creating an identity of type {1}", value, identityTypeName))
        {
        }
    }

    public interface IIdentity
    {
        object Value { get; }
    }

    public interface IIdentity<out TId> : IIdentity
    {
        TId Value { get; }
    }

    public abstract class Identity<TId> : IIdentity<TId>, IEquatable<Identity<TId>>
    {
        public abstract TId Value { get; }

        object IIdentity.Value
        {
            get
            {
                return Value;
            }
        }

        public static bool operator ==(Identity<TId> left, Identity<TId> right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.Equals(right);
        }

        public static bool operator !=(Identity<TId> left, Identity<TId> right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return EqualityComparer<string>.Default.GetHashCode(this.GetType().Name) ^ EqualityComparer<TId>.Default.GetHashCode(this.Value);
            }
        }

        /// <summary>
        /// Equalses the specified candidate.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="candidate"/> parameter; otherwise, false.
        /// </returns>
        public override bool Equals(object candidate)
        {
            if (ReferenceEquals(null, candidate))
            {
                return false;
            }

            if (ReferenceEquals(this, candidate))
            {
                return true;
            }

            if (candidate.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Identity<TId>)candidate);
        }

        public bool Equals(Identity<TId> other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return EqualityComparer<TId>.Default.Equals(this.Value, other.Value);
        }
    }

    public class GuidIdentity : Identity<Guid>
    {
        private readonly Guid value;

        public override Guid Value
        {
            get
            {
                return value;
            }
        }

        public GuidIdentity() : this(Guid.NewGuid())
        {
        }

        public GuidIdentity(string guidString)
        {
            Guid guid = Guid.Empty;

            if (!Guid.TryParse(guidString, out guid))
                throw new InvalidIdentityValueException(guidString, "GuidIdentity");

            this.value = guid;
        }

        public GuidIdentity(Guid guid)
        {
            this.value = guid;
        }

        public override string ToString()
        {
            return value.ToString().ToUpper();
        }

        public static bool operator ==(GuidIdentity identity, string idString)
        {
            if (ReferenceEquals(identity, null) && idString == null)
                return true;

            if (idString == null || ReferenceEquals(identity, null) || identity.Value == Guid.Empty)
                return false;

            return idString.ToUpper() == identity.value.ToString().ToUpper();
        }

        public static bool operator !=(GuidIdentity identity, string idString)
        {
            return !(identity == idString);
        }

        protected bool Equals(GuidIdentity other)
        {
            return this.value == other.value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((GuidIdentity)obj);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }
    }

    public class GuidIdentityConverter<TIdentity> : TypeConverter where TIdentity : GuidIdentity
    {
        private bool IsTypeAllowed(Type type)
        {
            var types = new List<Type> { typeof(Guid), typeof(string) };
            return types.Contains(type);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return this.IsTypeAllowed(sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is Guid || value is string)
            {
                var activator = Activator.CreateInstance(typeof(TIdentity), value);
                return activator;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return this.IsTypeAllowed(destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(Guid))
            {
                return ((GuidIdentity)value).Value;
            }
            if (destinationType == typeof(string))
            {
                return ((GuidIdentity)value).Value.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }


    }

    public class IntIdentity : Identity<int>
    {
        private readonly int value;

        public override int Value
        {
            get
            {
                return value;
            }
        }

        public IntIdentity()
        {
            this.value = 0;
        }

        public IntIdentity(int value)
        {
            this.value = value;
        }
    }

    public class IntIdentityConverter<TIdentity> : TypeConverter where TIdentity : IntIdentity
    {
        private bool IsTypeAllowed(Type type)
        {
            var types = new List<Type> { typeof(int), typeof(string) };
            return types.Contains(type);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return this.IsTypeAllowed(sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is int)
            {
                return Activator.CreateInstance(typeof(TIdentity), value);
            }
            int n;
            if (value is string && int.TryParse(value.ToString(), out n))
            {
                return Activator.CreateInstance(typeof(TIdentity), n);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return this.IsTypeAllowed(destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(int))
            {
                return ((IntIdentity)value).Value;
            }
            if (destinationType == typeof(string))
            {
                return ((IntIdentity)value).Value.ToString(CultureInfo.InvariantCulture);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }


    }
}