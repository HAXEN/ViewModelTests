using System;
using System.Collections.Generic;
using System.Linq;

namespace Techsson.Gaming.ControlPanel.Core
{
    public abstract class ViewModelBase
    {
        private readonly List<Resource> _resources = new List<Resource>();
        private readonly List<Property> _properties = new List<Property>();

        protected ViewModelBase() { }

        protected ViewModelBase(string title)
        {
            Title = title;
        }

        public virtual string Title { get; }
        public IEnumerable<Resource> Resources => _resources;
        public IEnumerable<Property> Properties => ResolveProperties();

        private IEnumerable<Property> ResolveProperties()
        {
            var list = new List<Property>();

            var publicProperties = GetType().GetProperties().Where(x => IsKnownPropertyType(x.PropertyType));
            foreach (var propertyInfo in publicProperties)
            {
                list.Add(Property.Create(propertyInfo.Name, Convert(propertyInfo.PropertyType), propertyInfo.GetValue(this)));
            }

            return list;
        }

        private PropertyValueType Convert(Type type)
        {
            switch (type.Name)
            {
                case "String":
                    return PropertyValueType.String;

                case "Int64":
                case "Int32":
                    return PropertyValueType.Integer;

                default:
                    return PropertyValueType.unknown;
            }
        }

        private bool IsKnownPropertyType(Type propertyType)
        {
            return Convert(propertyType) != PropertyValueType.unknown;
        }

        public void AddResource(string name, Uri uri)
        {
            if(_resources.Any(x => x.Name == name))
                return;

            _resources.Add(Resource.CreateGet(name, uri));
        }
    }
}