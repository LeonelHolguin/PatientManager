using PatientManager.Core.Application.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManager.Core.Application.Interfaces.Services
{
    public interface ICurrentUserApp
    {
        string GetCurrentUserName();
    }
}
