using System.Collections.Generic;
using VaskEnTidLib.Models;
using VaskEnTidLib.Repositories;

namespace VaskEnTidLib.Services
{
    public class ErrorReportService
    {
        private readonly ErrorReportRepo _errorReportRepo;

        public ErrorReportService(string connectionString)
        {
            _errorReportRepo = new ErrorReportRepo(connectionString);
        }

        /// <summary>
        /// Gets all error reports from the database.
        /// </summary>
        public List<ErrorReport> GetAllErrorReports()
        {
            return _errorReportRepo.GetAllErrorReports();
        }

        /// <summary>
        /// Example: Filters error reports by machine ID.
        /// </summary>
        public List<ErrorReport> GetErrorReportsByMachine(int machineId)
        {
            var allReports = _errorReportRepo.GetAllErrorReports();
            return allReports.FindAll(r => r.MachineID == machineId);
        }

        /// <summary>
        /// Example: Gets all reports that match a given status.
        /// </summary>
        public List<ErrorReport> GetReportsByStatus(int statusId)
        {
            var allReports = _errorReportRepo.GetAllErrorReports();
            return allReports.FindAll(r => r.StatusID == statusId);
        }

        /// <summary>
        /// Example: Gets a single report by its ID.
        /// </summary>
        public ErrorReport? GetErrorReportById(int errorId)
        {
            var allReports = _errorReportRepo.GetAllErrorReports();
            return allReports.Find(r => r.ErrorID == errorId);
        }
    }
}