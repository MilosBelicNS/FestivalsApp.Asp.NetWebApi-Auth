using FestivalApp.Asp.Net.WebApi.Controllers;
using FestivalApp.Asp.Net.WebApi.Interfaces;
using FestivalApp.Asp.Net.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace FestivalApp.Asp.Net.WebApi.Tests.Controllers
{
    [TestClass]
    public class FestivalsControllerTest
    {
        [TestMethod]
        public void GetReturnsProductWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IFestivalRepository>();
            mockRepository.Setup(x => x.GetById(42)).Returns(new Festival { Id = 42 });

            var controller = new FestivalsController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(42);
            var contentResult = actionResult as OkNegotiatedContentResult<Festival>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IFestivalRepository>();
            var controller = new FestivalsController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<IFestivalRepository>();
            var controller = new FestivalsController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IFestivalRepository>();
            mockRepository.Setup(x => x.GetById(10)).Returns(new Festival { Id = 10 });
            var controller = new FestivalsController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IFestivalRepository>();
            var controller = new FestivalsController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new Festival { Id = 9, Name = "Festival2" });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<IFestivalRepository>();
            var controller = new FestivalsController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new Festival { Id = 10, Name = "Festival2" });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Festival>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(10, createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<Festival> festivals = new List<Festival>();
            festivals.Add(new Festival { Id = 1, Name = "Festival1" });
            festivals.Add(new Festival { Id = 2, Name = "Festival2" });

            var mockRepository = new Mock<IFestivalRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(festivals.AsEnumerable());
            var controller = new FestivalsController(mockRepository.Object);

            // Act
            IEnumerable<Festival> result = controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(festivals.Count, result.ToList().Count);
            Assert.AreEqual(festivals.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(festivals.ElementAt(1), result.ElementAt(1));
        }
    }
}
