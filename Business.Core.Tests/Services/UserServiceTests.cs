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
    public class UserServiceTests
    {
        private Mock<IRoleRepository> roleRepository = new Mock<IRoleRepository>();

        private Mock<IUserRepository> userRepository = new Mock<IUserRepository>();

        private Mock<IUser> user = new Mock<IUser>();

        private Mock<IRole> role = new Mock<IRole>();

        private UserService userService;

        [TestInitialize]
        public void InitaializeTest()
        {
            userService = new UserService(userRepository.Object, roleRepository.Object);
        }

        [TestMethod]
        public void Create_ValidUser_UserCreated()
        {
            roleRepository.Setup(roleRep => roleRep.GetAll(It.IsAny<Func<IRole, bool>>()))
                .Returns(new List<IRole>() { role.Object });

            userService.Create(user.Object);

            userRepository.Verify(userRep => userRep.Create(user.Object));
            roleRepository.Verify(roleRep => roleRep.SetUserRole(user.Object, role.Object));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullUser_ExceptionIsThrown()
        {
            userService.Create(null);
        }

        [TestMethod]
        public void Delete_ValidUser_UserDeleted()
        {
            userService.Delete(user.Object);

            userRepository.Verify(userRep => userRep.Delete(user.Object), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullUser_ExceptionIsThrown()
        {
            userService.Delete(null);
        }

        [TestMethod]
        public void GetById_ValidId_Success()
        {
            string id = "someValidId";
            user.Setup(u => u.Id).Returns(id);
            userRepository.Setup(userRep => userRep.GetById(id)).Returns(user.Object);

            IUser returnedUser = userService.GetById(id);

            Assert.AreEqual(id, returnedUser.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetById_NullId_ExceptionIsThrown()
        {
            userService.GetById(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetById_EmptyId_ExceptionIsThrown()
        {
            userService.GetById(string.Empty);
        }

        [TestMethod]
        public void GetByName_ValidId_Success()
        {
            string userName = "someValidUserName";
            user.Setup(u => u.UserName).Returns(userName);
            userRepository.Setup(userRep => userRep.GetAll(It.IsAny<Func<IUser, bool>>())).Returns(new List<IUser>() { user.Object });

            IUser returnedUser = userService.GetByUserName(userName);

            Assert.AreEqual(userName, returnedUser.UserName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByName_NullId_ExceptionIsThrown()
        {
            userService.GetByUserName(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByName_EmptyId_ExceptionIsThrown()
        {
            userService.GetByUserName(String.Empty);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            List<IUser> users = new List<IUser>() { user.Object };
            userRepository.Setup(userRep => userRep.GetAll()).Returns(users);

            IEnumerable<IUser> returnedUsers = userService.GetAllUsers();

            CollectionAssert.AreEqual(users, returnedUsers.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Update_NullUser_ExceptionIsThrown()
        {
            userService.Update(null);
        }

        [TestMethod]
        public void Update_ValidUser_UserUpdated()
        {
            userService.Update(user.Object);

            userRepository.Verify(userRep => userRep.Update(user.Object), Times.Once);
        }
    }
}
