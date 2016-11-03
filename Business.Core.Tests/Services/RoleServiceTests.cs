using System;
using System.Collections.Generic;
using System.Linq;
using Business.Core.Services;
using Infrastructure.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interface.Repositories;

namespace Business.Core.Tests.Services
{
    [TestClass]
    public class RoleServiceTests
    {
        private readonly Mock<IRoleRepository> roleRepositopry = new Mock<IRoleRepository>();

        private Mock<IUser> user = new Mock<IUser>();

        private Mock<IRole> role = new Mock<IRole>();

        private RoleService roleService;

        [TestInitialize]
        public void InitializeTest()
        {
            roleService = new RoleService(roleRepositopry.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRoles_NullUser_ExceptionThrown()
        {
            roleService.GetRoles(null);
        }

        [TestMethod]
        public void GetRoles_ValidUser_ReturnedRoles()
        {
            List<IRole> roles = new List<IRole>{ role.Object };
            roleRepositopry.Setup(roleRep => roleRep.GetRoles(user.Object)).Returns(new List<IRole> {role.Object});

            IEnumerable<IRole> returnedRoles = roleService.GetRoles(user.Object);

            CollectionAssert.AreEqual(roles, returnedRoles.ToList());
        }

        [TestMethod]
        public void GetAllRoles()
        {
            List<IRole> roles = new List<IRole> { role.Object };
            roleRepositopry.Setup(roleRep => roleRep.GetAll()).Returns(new List<IRole> { role.Object });
            
            IEnumerable<IRole> returnedRoles = roleService.GetAllRoles();

            CollectionAssert.AreEqual(roles, returnedRoles.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetUserRoles_NullUserId_ExceptionThrown()
        {
            roleService.SetUserRoles(new IRole[3], null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SetUserRoles_NullRoles_ExceptionThrown()
        {
            roleService.SetUserRoles(null, "someValidId");
        }

        [TestMethod]
        public void SetUserRoles_ValidParams_RolesSetted()
        {
            List<IRole> roles = new List<IRole> {role.Object};
            string userId = "someValidUserId";
            roleService.SetUserRoles(roles, userId);

            roleRepositopry.Verify(roleRep => roleRep.SetUserRoles(roles, userId), Times.Once);
        }
    }
}
