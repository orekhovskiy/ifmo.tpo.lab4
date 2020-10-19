using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using ChatApp.Commons;
using ChatApp.Models;

namespace ChatApp.Services
{
    public static class DataBaseService
    {
        private static ChatContext db = new ChatContext();
        private static readonly  Regex regex = new Regex("[a-zA-Zа-яА-Я]+(([,. -][a-zA-Zа-яА-Я ])?[a-zA-Zа-яА-Я]*)*");

        public static Result AddUser(string login, string password, string firstname, string lastname)
        {
            if (login.Length > 15) return new Result(false, Errors.LongLoginError());
            if (login.Length < 6) return new Result(false, Errors.ShortLoginError());
            if (!regex.IsMatch(login)) return new Result(false, Errors.InvalidLoginError());

            if (password.Length > 15) return new Result(false, Errors.LongPasswordError());
            if (password.Length < 6) return new Result(false, Errors.ShortPasswordError());

            if (firstname.Length > 30) return new Result(false, Errors.LongFirstnameError());
            if (firstname.Length == 0) return new Result(false, Errors.EmptyFirstnameError());
            if (!regex.IsMatch(firstname)) return new Result(false, Errors.InvalidFirstnameError());

            if (lastname.Length > 30) return new Result(false, Errors.LongLastnameError());
            if (lastname.Length == 0) return new Result(false, Errors.EmptyLastnameError());
            if (!regex.IsMatch(lastname)) return new Result(false, Errors.InvalidLastnameError());

            if (db.User.Any(u => u.Login == login)) return new Result(false, Errors.DuplicateLoginError());

            try
            {
                var user = new User(login, password, firstname, lastname);
                db.User.Add(user);
                db.SaveChanges();

                var id = db.User.FirstOrDefault(u => u.Login == login).Id;
                return new Result(true, id);
            }
            catch (Exception e)
            {
                var result = new Result(false, e.Message);
                return result;
            }
            
        }

        public static Result DeleteUserById(int id)
        {
            try
            {
                db.Remove(db.User.Find(id));
                db.SaveChanges();
                return new Result(true);
            }
            catch (Exception e)
            {
                return new Result(false, e.Message);
            }
        }

        public static Result GetUser(string login, string password)
        {
            var user = db.User.FirstOrDefault(u => u.Login == login && u.Password == Hasher.GetHash(password));
            if (user == default)
            {
                return new Result(false, Errors.AuthError());
            }
            else
            {
                return new Result(true, user);
            }

        }

        public static Result UserExists(string login)
        {
            var user = db.User.FirstOrDefault(u => u.Login == login);
            if (user == default)
            {
                return new Result(false);
            }
            else
            {
                return new Result(true, user.Id);
            }
        }

        public static Result AddMessage(string content, string login)
        {
            if (!content.Any()) return new Result(false, Errors.EmptyMessageError());
            if (content.Length > 256) return new Result(false, Errors.LongMessageError());

            var previousMessage = db.Messages.Where(m => m.Login == login).ToList().LastOrDefault();
            if (previousMessage != default && previousMessage.Content == content) return new Result(false, Errors.DuplicateMessageError());

            try
            {
                var message = new Messages(content, login);
                db.Messages.Add(message);
                db.SaveChanges();
                var sentMessage = db.Messages.Where(m => m.Login == login).ToList().Last().Id;
                if (sentMessage == default)
                {
                    return new Result(true, "Couldn't get an id");
                }
                else
                {
                    return new Result(true, sentMessage);
                }
            }
            catch (Exception e)
            {
                return new Result(false, e.Message);
            }

        }

        public static Result DeleteMessage(int id)
        {
            try
            {
                db.Remove(db.Messages.Find(id));
                db.SaveChanges();
                return new Result(true);
            }
            catch (Exception e)
            {
                return new Result(false, e.Message);
            }
        }

        public static Result GetAllMessages()
        {
            try
            {
                var massages = db.Messages.ToList();
                return new Result(true, massages);
            }
            catch (Exception e)
            {
                return new Result(false, e.Message);
            }
        }
    }
}
