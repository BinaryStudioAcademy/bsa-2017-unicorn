using System.Collections;
using System.Collections.Generic;
using Unicorn.Shared.DTOs;
using Unicorn.DataAccess.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Unicorn.Shared.Services
{
    public class VendorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
    }
}

