using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Repositories;
using VaskEnTidLib.Models;

namespace VaskEnTidLib.Services
{
    public class MachineService
    {
        private readonly MachineRepo _repository;

        public MachineService(MachineRepo repository)
        {
            _repository = repository;
        }

        public List<Machine> GetMachinesByUserId(int userId)
        {
            return _repository.GetMachinesByUserId(userId);
        }

    }
}
