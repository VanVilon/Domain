using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfilesMatcher.Service.Controllers;

namespace Service.Tests
{
    [TestClass]
    public class MatchedProfilesControllerTests
    {
        [TestMethod]
        public void GetAllMatchedServices_ShouldReturn()
        {
            var service = new MatchedProfilesController();
        }
    }
}
