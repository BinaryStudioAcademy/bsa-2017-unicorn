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
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReportDTO>> GetAllAsync()
        {
            var reports = await _unitOfWork.ReportRepository.Query.ToListAsync();

            return reports.Select(r => ReportToDTO(r));
        }

        public async Task<ReportDTO> GetByIdAsync(long id)
        {
            var report = await _unitOfWork.ReportRepository.GetByIdAsync(id);

            return ReportToDTO(report);
        }

        public async Task<Report> CreateAsync(ReportDTO reportDto)
        {
            var report = CreateReport(reportDto);

            _unitOfWork.ReportRepository.Create(report);
            await _unitOfWork.SaveAsync();

            return report;
        }

        public async Task UpdateAsync(ReportDTO reportDto)
        {
            var report = CreateReport(reportDto);

            _unitOfWork.ReportRepository.Update(report);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var report = await _unitOfWork.ReportRepository.GetByIdAsync(id);
            report.IsDeleted = true;
            _unitOfWork.ReportRepository.Update(report);
            await _unitOfWork.SaveAsync();
        }

        private ReportDTO ReportToDTO(Report report)
        {
            return new ReportDTO()
            {
                Id = report.Id,
                Date = report.Date,
                Type = report.Type,
                Message = report.Message,
                Email = report.Email,
                ProfileId = report.ProfileId,
                ProfileType = report.ProfileType,
            };            
        }

        private Report CreateReport(ReportDTO reportDto)
        {
            return new Report
            {
                Id = reportDto.Id,
                Date = DateTime.Now,
                Type = reportDto.Type,
                Message = reportDto.Message,
                Email = reportDto.Email,
                ProfileId = reportDto.ProfileId,
                ProfileType = reportDto.ProfileType
            };
        }
    }
}
