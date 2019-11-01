using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FightCore.Backend.ViewModels.User;
using FightCore.Models;

namespace FightCore.Backend.Configuration.Mapping
{
    /// <summary>
    /// Creates a mapping between the users models and view models.
    /// </summary>
    public class UserMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapperProfile"/> object.
        /// </summary>
        public UserMapperProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(viewModel => viewModel.Name,
                    options => options.MapFrom(user => user.UserName))
                .ForMember(viewModel => viewModel.GravatarMd5,
                    options => options.MapFrom(user => CalculateMd5Hash(user.Email.ToLower())));
        }

        // Source: https://devblogs.microsoft.com/csharpfaq/how-do-i-calculate-a-md5-hash-from-a-string/
        private static string CalculateMd5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hash = md5.ComputeHash(inputBytes);

                // step 2, convert byte array to hex string
                var stringBuilder = new StringBuilder();
                foreach (var hashCharacter in hash)
                {
                    stringBuilder.Append(hashCharacter.ToString("x2"));
                }

                return stringBuilder.ToString();
            }

        }
    }
}
