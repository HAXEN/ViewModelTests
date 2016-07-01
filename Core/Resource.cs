using System;

namespace Techsson.Gaming.ControlPanel.Core
{
    public class Resource
    {
        private Resource(HttpVerb verb, string name, Uri uri)
        {
            Verb = verb;
            Name = name;
            Uri = uri;
        }

        public string Name { get; }
        public Uri Uri { get; }
        public HttpVerb Verb { get; }

        public static Resource CreateGet(string name, Uri uri)
        {
            if(string.IsNullOrEmpty(name))
                return null;

            if (uri == null)
                return null;

            return new Resource(HttpVerb.Get, name, uri);
        }
    }
}