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
            var reports = await _unitOfWork.ReportRepository.Query
                    .Include(r => r.Customer)
                    .Include(r => r.Vendor)
                    .Include(r => r.Company)
                    .ToListAsync();

            return reports.Select(r => ReportToDTO(r));
        }

        public async Task<ReportDTO> GetByIdAsync(long id)
        {
            var report = await _unitOfWork.ReportRepository.GetByIdAsync(id);

            return ReportToDTO(report);
        }

        public async Task<Report> CreateAsync(ReportDTO reportDto)
        {
            var report = await CreateReportAsync(reportDto);

            _unitOfWork.ReportRepository.Create(report);
            await _unitOfWork.SaveAsync();

            return report;
        }

        public async Task UpdateAsync(ReportDTO reportDto)
        {
            var report = await CreateReportAsync(reportDto);

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
            var customerId = report.Customer?.Id;
            var vendorId = report.Vendor?.Id;
            var companyId = report.Company?.Id;

            return new ReportDTO()
            {
                Id = report.Id,
                Date = report.Date,
                Type = report.Type,
                Message = report.Message,
                CustomerId = customerId,
                VendorId = vendorId,
                CompanyId = companyId
            };            
        }

        private async Task<Report> CreateReportAsync(ReportDTO reportDto)
        {
            var customerId = reportDto.CustomerId == null ? 0 : (long)reportDto.CustomerId;
            var vendorId = reportDto.VendorId == null ? 0 : (long)reportDto.VendorId;
            var companyId = reportDto.CompanyId == null ? 0 : (long)reportDto.CompanyId;
            var report = new Report
            {
                Id = reportDto.Id,
                Date = DateTime.Now,
                Type = reportDto.Type,
                Message = reportDto.Message,
                Customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId),
                Vendor = await _unitOfWork.VendorRepository.GetByIdAsync(vendorId),
                Company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId)
            };

            return report;
        }
    }
}
