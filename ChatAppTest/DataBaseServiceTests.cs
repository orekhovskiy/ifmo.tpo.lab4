using System;
using System.DirectoryServices.ActiveDirectory;
using ChatApp.Commons;
using ChatApp.Models;
using NUnit.Framework;
using static ChatApp.Services.DataBaseService;

namespace ChatAppTest
{
    public static class DataBaseServiceTests
    {
        private const string Login = "test";
        private const string Password = "test";
        private const string Firstname = "test";
        private const string Lastname = "test";
        private const string Content = "test";

        [SetUp]
        public static void Setup()
        {
            var userExists = UserExists(Login);
            if (!userExists.Success) return;
            var userId = (int)userExists.Value;
            DeleteUserById(userId);
        }

        #region Auth Part

        [Test]
        public static void ShouldAddUser()
        {
            var result = AddUser(Login, Password, Firstname, Lastname);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public static void ShouldKeepPasswordHashed()
        {
            GetUser(Login, Password);
            var hashedPassword = Hasher.GetHash(Password);
            Assert.IsTrue(Hasher.VerifyHash(Password, hashedPassword));
        }

        [Test]
        public static void ShouldNotAddUserWithDuplicateLogin()
        {
            var result = AddUser(Login, Password, Firstname, Lastname);
            Assert.IsTrue(result.Success);

            result = AddUser(Login, Password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.DuplicateLoginError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithLongLogin()
        {
            var login = new string('a', 16);
            var result = AddUser(login, Password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.LongLoginError(), (string)result.Value);
        }
        [Test]
        public static void ShouldNotAddUserWithShortLogin()
        {
            var login = new string('a', 5);
            var result = AddUser(login, Password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.ShortLoginError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithInvalidLogin()
        {
            var login = "123456";
            var result = AddUser(login, Password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.InvalidLoginError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithLongPassword()
        {
            var password = new string('a', 16);
            var result = AddUser(Login, password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.LongPasswordError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithShortPassword()
        {
            var password = new string('a', 5);
            var result = AddUser(Login, password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.ShortPasswordError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithLongFirstname()
        {
            var firstname = new string('a', 31);
            var result = AddUser(Login, Password, firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.LongFirstnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithEmptyFirstname()
        {
            var result = AddUser(Login, Password, "", Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.EmptyFirstnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithInvalidFirstname()
        {
            var firstname = "123456";
            var result = AddUser(Login, Password, firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.InvalidFirstnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithLongLastname()
        {
            var lastname = new string('a', 31);
            var result = AddUser(Login, Password, Firstname, lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.LongLastnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithEmptyLastname()
        {
            var result = AddUser(Login, Password, Firstname, "");
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.EmptyLastnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithInvalidLastname()
        {
            var lastname = "123456";
            var result = AddUser(Login, Password, Firstname, lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.InvalidLastnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldAuthorizeExistingUser()
        {
            AddUser(Login, Password, Firstname, Lastname);
            var result = GetUser(Login, Password);
            Assert.IsTrue(result.Success);
            Assert.IsInstanceOf(typeof(User), result.Value);
        }

        [Test]
        public static void ShouldNotAuthorizeNonExistingUser()
        {
            AddUser(Login, Password, Firstname, Lastname);
            var result = GetUser(Login, Password);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.AuthError(), (string)result.Value);
        }

        #endregion

        #region Chat Part

        [Test]
        public static void ShouldAddMessage()
        {
            var result = AddMessage(Content, Login);
            Assert.IsTrue(result.Success);

            var id = (int)result.Value;
            DeleteMessage(id);
        }

        [Test]
        public static void ShouldNotAddEmptyMessage()
        {
            var result = AddMessage("", Login);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.EmptyMessageError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddLongMessage()
        {
            var content = new string('a', 257);
            var result = AddMessage(content, Login);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.LongMessageError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddDuplicateMessage()
        {
            var result = AddMessage(Content, Login);
            Assert.IsTrue(result.Success);
            
            result = AddMessage(Content, Login);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.DuplicateMessageError(), (string)result.Value);
        }

        [Test]
        public static void ShouldGetAllMessages()
        {
            var result = GetAllMessages();
            Assert.IsTrue(result.Success);
            Assert.IsInstanceOf(typeof(List), result.Value);
        }

        #endregion

    }
}