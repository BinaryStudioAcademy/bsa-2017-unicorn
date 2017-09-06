using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;

        public AvatarService(IAuthService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        public async Task UploadAvatar(string imageUrl, long id)
        {
            Account account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            account.Avatar = imageUrl;
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveAsync();
        }

        public async Task UploadCroppedAvatar(string imageUrl, long id)
        {
            Account account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            account.CroppedAvatar = imageUrl;
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveAsync();
        }

        public async Task UploadBackground(string imageUrl, long id)
        {
            Account account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            account.Background = imageUrl;
            _unitOfWork.AccountRepository.Update(account);
            await _unitOfWork.SaveAsync();
        }
    }
}
