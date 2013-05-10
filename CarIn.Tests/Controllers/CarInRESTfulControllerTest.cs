using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarIn.Controllers;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using Moq;
using NUnit.Framework;

namespace CarIn.Tests.Controllers
{   
    [TestFixture]
    class CarInRESTfulControllerTest
    {
        private CarInRESTfulController _carInRESTfulController;
        [SetUp]
        public void Setup()
        {
            var trafficIncidents = new List<TrafficIncident>();
            var mock = new Mock<IRepository<TrafficIncident>>();
            //mock.Setup(x => x.FindAll()).Returns(trafficIncidents.AsQueryable);
            _carInRESTfulController = new CarInRESTfulController(mock.Object);
        }
        [Test]
        public void GetAllInfo_Request_Returns_Collection_With_TrafficIncidents_That_Is_Not_Null()
        {
            //Arrange
            //Act
            var result = _carInRESTfulController.GetAllInfo();
            //Assert
            Assert.NotNull(result, "Should not be null");
        }
        [Test]
        public void GetAllInfo_Request_Returns_Collection_With_TrafficIncidents_That_Is_Empty()
        {
            //Arrange
            //Act
            var result = _carInRESTfulController.GetAllInfo();
            //Assert
            Assert.IsEmpty(result as IEnumerable, "Should be empty");
        }
    }
}
