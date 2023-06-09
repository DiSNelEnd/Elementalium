using System;
using System.Collections.Generic;
using System.Text;
using Elementalium.Domain.Model;

namespace Elementalium.Domain.Repositories
{
    public interface IBunchRepository
    {
        List<Bunch> GetBunches();
        Bunch FindBunchById(string id);
        void Save(Bunch bunch);
        void Delete(Bunch bunch);
    }
}
