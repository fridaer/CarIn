using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using CarIn.Controllers;
using CarIn.DAL.Repositories;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using CarIn.Models.ViewModels;
using Moq;
using NUnit.Framework;

namespace CarIn.Tests.Controllers
{   
    [TestFixture]
    class CarInRESTfulControllerTest
    {
        //TODO Skriva om eller dumpa testen då metoderna retunerar HttpResponseMessage för mer kontroll så kommer Det bli problem med att det inte skickas med ett giltligt request till kontrollern
        private CarInRESTfulController _carInRESTfulController;
        private Mock<CarInRESTfulController> _mockController;
        [SetUp]
        public void Setup()
        {

            var trafficFakeRepo= new FakeRepository<TrafficIncident>();
            var wheatherFakeRepo = new FakeRepository<WheatherPeriod>();
            var vasttrafikFakeRepo = new FakeRepository<VasttrafikIncident>();
            
            
            _carInRESTfulController = new CarInRESTfulController(trafficFakeRepo, wheatherFakeRepo, vasttrafikFakeRepo);
        }
        [Test]
        [Category("WebApiTest")]
        public void GetAllInfo_Request_Returns_Not_Null()
        {
            //Arrange
            //Act
            var result = _carInRESTfulController.GetAllInfo();
            
            //Assert
            Assert.NotNull(result, "Should not be null");
        }
        [Test]
        [Category("WebApiTest")]
        public void GetAllInfo_Request_Returns_TypeOf_HttpResponseMessage()
        {
            //Arrange
            //Act
            var result = _carInRESTfulController.GetAllInfo();
            //Assert
            Assert.IsInstanceOf<HttpResponseMessage>(result, "Should be empty");
        }
    }
}
