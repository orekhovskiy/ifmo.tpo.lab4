using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using Server.Commons;
using Server.Models;
using NUnit.Framework;
using Server.Services;

namespace ChatAppTest
{
    public static class DataBaseServiceTests
    {
        private const string Login = "loginTest";
        private const string Password = "passwordTest";
        private const string Firstname = "test";
        private const string Lastname = "test";
        private const string Content = "test";

        private static ChatContext context = new ChatContext();
        private static DataBaseService db = new DataBaseService(context);

        [SetUp]
        public static void Setup()
        {
            
            var userExists = db.UserExists(Login);
            if (!userExists.Success) return;
            var userId = (int)userExists.Value;
            db.DeleteUserById(userId);
        }

        #region Auth Part

        [Test]
        public static void ShouldAddUser()
        {
            var result = db.AddUser(Login, Password, Firstname, Lastname);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public static void ShouldKeepPasswordHashed()
        {
            db.GetUser(Login, Password);
            var hashedPassword = Hasher.GetHash(Password);
            Assert.IsTrue(Hasher.VerifyHash(Password, hashedPassword));
        }

        [Test]
        public static void ShouldNotAddUserWithDuplicateLogin()
        {
            var result = db.AddUser(Login, Password, Firstname, Lastname);
            Assert.IsTrue(result.Success);

            result = db.AddUser(Login, Password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.DuplicateLoginError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithLongLogin()
        {
            var login = new string('a', 16);
            var result = db.AddUser(login, Password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.LongLoginError(), (string)result.Value);
        }
        [Test]
        public static void ShouldNotAddUserWithShortLogin()
        {
            var login = new string('a', 5);
            var result = db.AddUser(login, Password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.ShortLoginError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithInvalidLogin()
        {
            var login = "123456";
            var result = db.AddUser(login, Password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.InvalidLoginError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithLongPassword()
        {
            var password = new string('a', 16);
            var result = db.AddUser(Login, password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.LongPasswordError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithShortPassword()
        {
            var password = new string('a', 5);
            var result = db.AddUser(Login, password, Firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.ShortPasswordError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithLongFirstname()
        {
            var firstname = new string('a', 31);
            var result = db.AddUser(Login, Password, firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.LongFirstnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithEmptyFirstname()
        {
            var result = db.AddUser(Login, Password, "", Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.EmptyFirstnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithInvalidFirstname()
        {
            var firstname = "123456";
            var result = db.AddUser(Login, Password, firstname, Lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.InvalidFirstnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithLongLastname()
        {
            var lastname = new string('a', 31);
            var result = db.AddUser(Login, Password, Firstname, lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.LongLastnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithEmptyLastname()
        {
            var result = db.AddUser(Login, Password, Firstname, "");
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.EmptyLastnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddUserWithInvalidLastname()
        {
            var lastname = "123456";
            var result = db.AddUser(Login, Password, Firstname, lastname);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.InvalidLastnameError(), (string)result.Value);
        }

        [Test]
        public static void ShouldAuthorizeExistingUser()
        {
            db.AddUser(Login, Password, Firstname, Lastname);
            var result = db.GetUser(Login, Password);
            Assert.IsTrue(result.Success);
            Assert.IsInstanceOf(typeof(User), result.Value);
        }

        [Test]
        public static void ShouldNotAuthorizeNonExistingUser()
        {
            var result = db.GetUser(Login, Password);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.AuthError(), (string)result.Value);
        }

        #endregion

        #region Chat Part

        [Test]
        public static void ShouldAddMessage()
        {
            var result = db.AddMessage(Content, Login);
            Assert.IsTrue(result.Success);

            var id = (int)result.Value;
            db.DeleteMessage(id);
        }

        [Test]
        public static void ShouldNotAddEmptyMessage()
        {
            var result = db.AddMessage("", Login);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.EmptyMessageError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddLongMessage()
        {
            var content = new string('a', 257);
            var result = db.AddMessage(content, Login);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.LongMessageError(), (string)result.Value);
        }

        [Test]
        public static void ShouldNotAddDuplicateMessage()
        {
            var result = db.AddMessage(Content, Login);
            Assert.IsTrue(result.Success);
            var id = (int)result.Value;
            
            result = db.AddMessage(Content, Login);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(Errors.DuplicateMessageError(), (string)result.Value);

            db.DeleteMessage(id);
        }

        [Test]
        public static void ShouldGetAllMessages()
        {
            var result = db.GetAllMessages();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(List<Messages>), result);
        }

        #endregion

    }
}