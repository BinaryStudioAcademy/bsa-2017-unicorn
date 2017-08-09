using System.Collections;
using System.Collections.Generic;
using Unicorn.Core.DTOs;
using Unicorn.DataAccess.Interfaces;
using System.Linq;

namespace Unicorn.Core.Services
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

