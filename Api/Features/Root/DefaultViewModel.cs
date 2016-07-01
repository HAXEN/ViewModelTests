using System;
using System.Web.Http;
using Techsson.Gaming.ControlPanel.Core;

namespace Api.Features.Root
{
    [Route("/")]
    public class DefaultViewModel : ViewModelBase
    {
        public DefaultViewModel() : base("Site integration")
        {
            AddResource("Games", new Uri("/games", UriKind.Relative));
        }

        public string Description { get; set; }
    }
}