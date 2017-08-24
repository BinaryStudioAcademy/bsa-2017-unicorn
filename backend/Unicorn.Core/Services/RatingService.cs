using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Services
{
    public class RatingService: IRatingService
    {
        private IUnitOfWork _unitOfWork;
        public RatingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RatingDTO> GetByIdAsync(long id)
        {
            var rating = await _unitOfWork.RatingRepository.GetByIdAsync(id);

            return new RatingDTO()
            {
                Id = rating.Id,
                Grade = rating.Grade,
                Reciever = new AccountDTO()
                {
                    Id = rating.Reciever.Id,
                    Avatar = rating.Reciever.Avatar,
                    DateCreated = rating.Reciever.DateCreated,
                    Email = rating.Reciever.Email,
                    EmailConfirmed = rating.Reciever.EmailConfirmed,
                    Role = new RoleDTO()
                    {
                        Id = rating.Reciever.Role.Id,
                        Name = rating.Reciever.Role.Name
                    }
                },
                Sender = new AccountDTO()
                {
                    Id = rating.Sender.Id,
                    Avatar = rating.Sender.Avatar,
                    DateCreated = rating.Sender.DateCreated,
                    Email = rating.Sender.Email,
                    EmailConfirmed = rating.Sender.EmailConfirmed,
                    Role = new RoleDTO
                    {
                        Id = rating.Sender.Role.Id,
                        Name = rating.Sender.Role.Name
                    }
                }
            };
        }

        public async Task<IEnumerable<RatingDTO>> GetByReceiverIdAsync(long id)
        {
            var ratings = await _unitOfWork.RatingRepository.Query
                .Include(v => v.Reciever)
                .Include(v => v.Sender)
                .Where(x => x.Reciever.Id == id).ToListAsync();
            return ratings.Select(RatingToDTO);

        }

        public Task<IEnumerable<RatingDTO>> GetBySenderIdAsync(long id)
        {
            return null;
        }

        public async Task<double> GetAvarageByRecieverId(long id)
        {
            var ratings = await GetByReceiverIdAsync(id);

            return ratings.Any() ? ratings.Average(x => x.Grade) : 0;

        }

        private RatingDTO RatingToDTO(Rating rating)
        {
            return new RatingDTO
            {
                Id = rating.Id,
                Grade = rating.Grade,
                Reciever = new AccountDTO
                {
                  Id = rating.Reciever.Id,
                  Avatar = rating.Reciever.Avatar,
                  DateCreated = rating.Reciever.DateCreated,
                  Email = rating.Reciever.Email,
                  EmailConfirmed = rating.Reciever.EmailConfirmed
                }
            };
        }
    }
}
