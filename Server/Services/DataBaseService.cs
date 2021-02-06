using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Commons;
using Server.Models;

namespace Server.Services
{
    public class DataBaseService : IDatabaseService
    {
        private readonly Regex regex;
        private readonly ChatContext db;

        public DataBaseService(ChatContext context)
        {
            db = context;
            regex = new Regex("[a-zA-Zа-яА-Я]+(([,. -][a-zA-Zа-яА-Я ])?[a-zA-Zа-яА-Я]*)*");
        }

        public Result AddUser(string login, string password, string firstname, string lastname)
        {
            login ??= "";
            password ??= "";
            firstname ??= "";
            lastname ??= "";
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

        public Result DeleteUserById(int id)
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

        public Result GetUser(string login, string password)
        {
            login ??= "";
            password ??= "";
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

        public Result UserExists(string login)
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

        public Result AddMessage(string content, string login)
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
                var sentMessageId = db.Messages.Where(m => m.Login == login).ToList().Last().Id;
                if (sentMessageId == default)
                {
                    return new Result(true, "Couldn't get an id");
                }
                else
                {
                    return new Result(true, sentMessageId);
                }
            }
            catch (Exception e)
            {
                return new Result(false, e.Message);
            }

        }

        public Result DeleteMessage(int id)
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

        public List<Messages> GetAllMessages() => db.Messages.ToList();
    }
}
