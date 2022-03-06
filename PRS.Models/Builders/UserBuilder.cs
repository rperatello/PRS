using PRS.Models.Interfaces;
using PRS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Models.Builders
{
    public class UserBuilder : IBuilder<Models.User>
    {

        private readonly User user;

        public UserBuilder()
        {
           user = new User();
        }

        public UserBuilder Name(string name)
        {
            user.name = name;
            return this;
        }

        public UserBuilder Email(string email)
        {
            user.email = email;
            return this;
        }

        public UserBuilder Dn(string dn)
        {
            user.dn = dn;
            return this;
        }

        public User Build()
        {
            return user;
        }
    }
}
