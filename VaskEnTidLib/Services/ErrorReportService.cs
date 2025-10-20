using System.Collections.Generic;
using VaskEnTidLib.Models;
using VaskEnTidLib.Repositories;

namespace VaskEnTidLib.Services
{
    public class ErrorReportService
    {
        private readonly ErrorReportRepo _errorReportRepo;

        public ErrorReportService(ErrorReportRepo errorReportRepo)
        {
            _errorReportRepo = errorReportRepo;
        }

        public List<ErrorReport> GetErrorReportByUserId(int userId)
        {
            return _errorReportRepo.GetErrorReportByUserId(userId);
        }

    }
}