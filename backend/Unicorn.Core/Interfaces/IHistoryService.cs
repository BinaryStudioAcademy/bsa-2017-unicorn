﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IHistoryService
    {
        Task<IEnumerable<HistoryDTO>> GetAllAsync();
        Task<HistoryDTO> GetById(long id);
    }
}
