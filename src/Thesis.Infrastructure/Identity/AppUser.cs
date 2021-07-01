using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Thesis.Domain.Entities;
using Thesis.Domain.Exceptions;

namespace Thesis.Infrastructure.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string Pseudonym
        {
            get => pseudonym; set
            {
                if (value.Length > PSEUDONYM_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(AppUser)}.{nameof(Pseudonym)} cannot be bigger than {PSEUDONYM_MAX_LENGTH}.");
                }
                if (value.Length < PSEUDONYM_MIN_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(AppUser)}.{nameof(Pseudonym)} cannot be less than {PSEUDONYM_MIN_LENGTH}.");
                }
                pseudonym = value;
            }
        }

        public DateTime CreatedDate { get; set; }
        public DateTime? CloseAccountDate { get; set; }

        public virtual IList<Route> CreatedRoutes { get; set; }
        public virtual IList<Route> ModifiedRoutes { get; set; }
        public virtual IList<Run> Runs { get; set; }
        public virtual IList<UserAgent> UserAgents { get; set; }

        public static readonly int PSEUDONYM_MIN_LENGTH = 6;
        public static readonly int PSEUDONYM_MAX_LENGTH = 20;

        private string pseudonym;

        public AppUser()
        {
        }

        public AppUser(string email, string pseudonym, DateTime creationDate)
        {
            Pseudonym = pseudonym;
            CreatedDate = creationDate;

            UserName = email;
            NormalizedUserName = email.ToUpper();
            Email = email;
            NormalizedEmail = email.ToUpper();
        }
    }
}
