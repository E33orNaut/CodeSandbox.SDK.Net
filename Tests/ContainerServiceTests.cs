using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyProject.Services;
using MyProject.Models;
using MyProject.Repositories;

namespace MyProject.Tests.Services
{
    [TestClass]
    public class ContainerServiceTests
    {
        private Mock<IContainerRepository> _containerRepoMock;
        private ContainerService _service;

        [TestInitialize]
        public void Setup()
        {
            _containerRepoMock = new Mock<IContainerRepository>();
            _service = new ContainerService(_containerRepoMock.Object);
        }

        [TestMethod]
        public void GetContainer_ReturnsContainer_WhenExists()
        {
            var container = new Container { Id = 1, Name = "Test" };
            _containerRepoMock.Setup(r => r.GetById(1)).Returns(container);

            var result = _service.GetContainer(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Name);
        }

        [TestMethod]
        public void GetContainer_ReturnsNull_WhenNotExists()
        {
            _containerRepoMock.Setup(r => r.GetById(2)).Returns((Container)null);

            var result = _service.GetContainer(2);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateContainer_CallsRepositoryAdd()
        {
            var container = new Container { Name = "New" };

            _service.CreateContainer(container);

            _containerRepoMock.Verify(r => r.Add(container), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateContainer_Throws_WhenNull()
        {
            _service.CreateContainer(null);
        }

        [TestMethod]
        public void UpdateContainer_CallsRepositoryUpdate()
        {
            var container = new Container { Id = 1, Name = "Updated" };

            _service.UpdateContainer(container);

            _containerRepoMock.Verify(r => r.Update(container), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateContainer_Throws_WhenNull()
        {
            _service.UpdateContainer(null);
        }

        [TestMethod]
        public void DeleteContainer_CallsRepositoryDelete()
        {
            _service.DeleteContainer(1);

            _containerRepoMock.Verify(r => r.Delete(1), Times.Once);
        }
    }
}