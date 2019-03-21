using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BenefitCsBdd;
using Moq;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions;
using System.Configuration;
using System.Collections.Specialized;

namespace BenefitCsBdd.Tests
{
    [TestClass]
    public class MiscUnitTests
    {
        [TestMethod]
        public void TestConfigSectionGroup()
        {
            var _wellknownSites = new List<string>();
            var settings = (NameValueCollection)ConfigurationManager.GetSection("wellknownSite/sites");
            foreach (string key in settings.AllKeys)
                _wellknownSites.Add(settings[key].ToString().Trim());

            _wellknownSites[0].Should().Be("https://www.google.com");
            _wellknownSites[1].Should().Be("https://www.yahoo.com");
            _wellknownSites[2].Should().Be("https://www.bing.com");
        }
    }
}
