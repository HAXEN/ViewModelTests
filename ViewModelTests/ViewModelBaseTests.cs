using System;
using System.Linq;
using Techsson.Gaming.ControlPanel.Core;
using Xunit;

namespace CoreTests
{
    public class ViewModelBaseTests
    {
        [Fact]
        public void Should_serialize_Resources_as_collection()
        {
            var model = new TestViewModelWithResources();
            model.AddResource("Kalle", new Uri("/Kalle", UriKind.Relative));

            var serialized = model.Serialize();

            Assert.Contains("\"name\":\"Kalle\"", serialized);
            Assert.Contains("\"uri\":\"/Kalle\"", serialized);
            Assert.Contains("\"verb\":\"Get\"", serialized);
        }

        [Fact]
        public void Should_Serialize_properties_as_collection()
        {
            var model = new TestViewModel();

            model.TestValue = "TheValue";
            model.Id = 1234;
            model.RelationId = 456789;

            var serialized = model.Serialize();

            Assert.Contains("\"title\":\"Test\"", serialized);
            Assert.Contains("{\"name\":\"TestValue\"", serialized);
            Assert.Contains("{\"name\":\"Id\"", serialized);
            Assert.Contains("{\"name\":\"RelationId\"", serialized);
            Assert.Contains("\"value\":\"TheValue\"", serialized);
            Assert.Contains("\"value\":1234", serialized);
            Assert.Contains("\"value\":456789", serialized);
            Assert.Contains("\"type\":\"String\"", serialized);
            Assert.Contains("\"type\":\"Integer\"", serialized);
        }

        [Fact]
        public void Should_not_Serialize_properties()
        {
            var model = new TestViewModel();

            model.TestValue = "TheValue";
            model.Id = 1234;
            model.RelationId = 456789;

            var serialized = model.Serialize();

            Assert.Contains("\"title\":\"Test\"", serialized);
            Assert.DoesNotContain("\"testValue\":", serialized);
            Assert.DoesNotContain("\"id\":", serialized);
            Assert.DoesNotContain("\"realtionId\":", serialized);
        }

        [Fact]
        public void Should_be_able_to_Set_PropertyValue()
        {
            var model = new TestViewModel();

            model.TestValue = "TheValue";
            model.Id = 1234;
            model.RelationId = 456789;

            Assert.Equal("TheValue", model.Properties.First(x => x.Name == "TestValue").Value);
            Assert.Equal(1234, model.Properties.First(x => x.Name == "Id").Value);
            Assert.Equal(456789L, model.Properties.First(x => x.Name == "RelationId").Value);
        }

        [Fact]
        public void Should_have_property_TestValue()
        {
            var model = new TestViewModel();

            Assert.NotEmpty(model.Properties);
            Assert.Equal(1, model.Properties.Count(x => x.Name == "TestValue"));
        }

        [Fact]
        public void Should_have_list_with_Properties()
        {
            var model = new TestViewModel();

            Assert.NotNull(model.Properties);
        }

        [Fact]
        public void Should_have_Title()
        {
            var model = new TestViewModel();

            Assert.False(string.IsNullOrEmpty(model.Title));
        }

        [Fact]
        public void Should_have_list_of_available_links()
        {
            var model = new TestViewModel();
            Assert.NotNull(model.Resources);
        }

        [Fact]
        public void Should_be_able_to_add_available_link()
        {
            var model = new TestViewModel();

            model.AddResource("Kalle", new Uri("/Kalle", UriKind.Relative));

            Assert.NotEmpty(model.Resources);
            Assert.True(model.Resources.Any(x => x.Name == "Kalle"));
        }

        [Fact]
        public void Should_not_be_able_to_add_available_link_twice()
        {
            var model = new TestViewModel();

            model.AddResource("Kalle", new Uri("/Kalle", UriKind.Relative));
            model.AddResource("Kalle", new Uri("/Kalle2", UriKind.Relative));

            Assert.Equal(1, model.Resources.Count(x => x.Name == "Kalle"));
            Assert.Equal("/Kalle", model.Resources.First(x => x.Name == "Kalle").Uri.ToString());
        }

        [Fact]
        public void Should_be_able_to_add_resouces_in_the_Constructor()
        {
            var model = new TestViewModelWithResources();

            Assert.NotEmpty(model.Resources);
        }
    }

    public class TestViewModelWithResources : ViewModelBase
    {
        public TestViewModelWithResources()
        {
            AddResource("Olle", new Uri("/olle", UriKind.Relative));
        }
    }

    public class TestViewModel : ViewModelBase
    {
        public TestViewModel() : base("Test")
        {
        }

        public int Id { get; set; }
        public string TestValue { get; set; }
        public long RelationId { get; set; }
    }
}