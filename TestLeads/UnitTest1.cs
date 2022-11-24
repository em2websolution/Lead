using Application;
using Domain.Adapters;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;

namespace TestLeads
{
    public class UnitTest
    {
        private readonly Mock<ILeadsAdapter> _leadsAdapter;
        private readonly LeadsServices _leadsServices;
        private readonly ModelStateDictionary _modelState;

        public UnitTest()
        {
            _leadsAdapter = new Mock<ILeadsAdapter>();
            _leadsServices = new LeadsServices(_leadsAdapter.Object);
            _modelState = new ModelStateDictionary();
        }

        [Fact]
        public void CreateLead()
        {
            //Arragne
            var lead = new Leads()
            {
                FirsName = "Eduardo",
                LastName = "Torres",
                Company = "DevLab",
                Email = "eduardo.torres@DevLabinc.com",
                PhoneNumber = "+00000000000",
                ZipCode = "12345-678",
                Created = DateTime.Now
            };

            _leadsAdapter.Setup(s => s.Insert(lead))
                .Returns(new Guid())
                .Verifiable();


            //Act
            var response = _leadsServices.Insert(lead);

            //Assert
            Assert.IsType<Guid>(response);
            Assert.True(response is Guid);

        }

        [Fact]
        public void CreateLeadRequeredFirstName()
        {
            //Arragne
            var lead = new Leads()
            {
                FirsName = "",
                LastName = "Torres",
                Company = "DevLab",
                Email = "eduardo.torres@DevLabinc.com",
                PhoneNumber = "+00000000000",
                ZipCode = "12328-260",
                Created = DateTime.Now
            };

            //Act
            var response = Assert.ThrowsAny<ArgumentNullException>(
                                () => _leadsServices.Insert(lead));

            //Assert
            Assert.IsType<ArgumentNullException>(response);
            Assert.False(response is Guid);

        }
    }
}