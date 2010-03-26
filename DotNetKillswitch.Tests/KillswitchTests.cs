using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetKillswitch.Core;
using DotNetKillswitch.Core.Client;
using NUnit.Framework;

namespace DotNetKillswitch.Tests
{
    [TestFixture]
    public class KillswitchTests
    {
        [Test]
        public void Css_Generates_Correct_Link()
        {
            var link = Killswitch.Css();

            Assert.IsEmpty(link);

            var guid = Guid.NewGuid();

            Killswitch.Set()
                      .WithServer(new Uri("http://localhost.com"))
                      .WithClientId(guid);

            link = Killswitch.Css();

            var expected = string.Format( "<link href=\"http://localhost.com/{0}.kss\" type=\"text/css\" rel=\"stylesheet\" />", guid);

            Assert.IsNotEmpty(link);
            Assert.AreEqual(link, expected);

        }
    }
}
