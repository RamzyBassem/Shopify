using Shopify.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound = new(
            "User.Found",
            "The user with the specified identifier was not found");

        public static Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "The provided credentials were invalid");
        public static Error UserNameExists = new(
            "User.UserNameExists",
            "The provided userName already exists");
        public static Error EmailExists = new(
            "User.EmailExists",
            "The provided email already exists");
    }
}
