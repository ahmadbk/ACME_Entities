using System;
using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;


namespace ACME.Maintenance.Domain.Test
{
    [TestClass]
    public class PartServiceTest
    {
        private const string ValidPartId = "VALIDPARTID";
        private const double ValidPartAmount = 50.0;

        private IPartServiceRepository _partRepository;
        private PartService _partService;

        [TestInitialize]
        public void Initialize()
        {
            // All the setup needs to happen here
            // Even the auto mapping needs to be setup here
            // Initialize serves as the "composition route"

            _partRepository = A.Fake<IPartServiceRepository>();
            _partService = new PartService(_partRepository);

            A.CallTo(() => _partRepository.GetById(ValidPartId))
                .Returns(new PartDto
                {
                    PartId = ValidPartId,
                    Price = ValidPartAmount
                });

            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<PartDto, Part>());

        }

        [TestMethod]
        public void GetByPartId_ValidId_ReturnsPart()
        {
            var part = _partService.GetById(ValidPartId);

            Assert.IsInstanceOfType(part, typeof(Part));
            Assert.AreEqual(part.PartId, ValidPartId);
            Assert.AreEqual(part.Price, ValidPartAmount);
        }
    }
}
