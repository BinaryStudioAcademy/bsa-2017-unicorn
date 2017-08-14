﻿using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unicorn.Core.DTOs;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var persons = await _unitOfWork.PersonRepository.GetAllAsync();
            List<PersonDTO> dataReturn = new List<PersonDTO>();
            foreach (var person in persons)
            {
                var account = await _unitOfWork.AccountRepository.GetByIdAsync(person.Account.Id);
                var location = await _unitOfWork.LocationRepository.GetByIdAsync(person.Location.Id);
                var persontDto = new PersonDTO()
                {
                    Id = person.Id,
                    Name = person.Name,
                    SurnameName = person.SurnameName,
                    MiddleName = person.MiddleName,
                    Birthday = person.Birthday,
                    Gender = person.Gender,
                    Phone = person.Phone,
                    Account = new AccountDTO()
                     {
                         Id = account.Id,
                         Role = new RoleDTO() { Id = account.Role.Id, Name = account.Role.Name },
                         Avatar = account.Avatar,
                         DateCreated = account.DateCreated,
                         Email = account.Email,
                         EmailConfirmed = account.EmailConfirmed,
                         Rating = account.Rating,
                         SocialAccounts = account.SocialAccounts.Select(x => new SocialAccountDTO { Id = x.Id, Provider = x.Provider, Uid = x.Uid }).ToList()
                     },
                    Location = new LocationDTO()
                    {
                        Id = location.Id,
                        Adress = location.Adress,
                        City = location.City,
                        CoordinateX = location.CoordinateX,
                        CoordinateY = location.CoordinateY,
                        PostIndex = location.PostIndex
                    }
                };
                dataReturn.Add(persontDto);
            }
            return dataReturn;
        }

        public async Task<PersonDTO> GetById(long id)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(person.Account.Id);
            var location = await _unitOfWork.LocationRepository.GetByIdAsync(person.Location.Id);

            var persontDto = new PersonDTO()
            {
                Id = person.Id,
                Name = person.Name,
                SurnameName = person.SurnameName,
                MiddleName = person.MiddleName,
                Birthday = person.Birthday,
                Gender = person.Gender,
                Phone = person.Phone,
                Account = new AccountDTO()
                {
                    Id= account.Id,
                    Role = new RoleDTO() { Id = account.Role.Id, Name = account.Role.Name},
                    Avatar = account.Avatar,
                    DateCreated = account.DateCreated,
                    Email = account.Email,
                    EmailConfirmed = account.EmailConfirmed,
                    Rating = account.Rating,
                    SocialAccounts = account.SocialAccounts.Select(x => new SocialAccountDTO { Id = x.Id, Provider = x.Provider, Uid = x.Uid }).ToList()
                },
                Location = new LocationDTO()
                {
                    Id = location.Id,
                    Adress = location.Adress,
                    City = location.City,
                    CoordinateX = location.CoordinateX,
                    CoordinateY = location.CoordinateY,
                    PostIndex = location.PostIndex
                }
            };
            return persontDto;
        }
    }
}
