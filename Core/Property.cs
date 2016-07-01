namespace Techsson.Gaming.ControlPanel.Core
{
    public class Property
    {
        private Property(string name, PropertyValueType type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public string Name { get; }
        public PropertyValueType Type { get; }
        public object Value { get; }

        public static Property Create(string name, PropertyValueType type, object value)
        {
            var property = new Property(name, type, value);
            return property;
        }
    }
}