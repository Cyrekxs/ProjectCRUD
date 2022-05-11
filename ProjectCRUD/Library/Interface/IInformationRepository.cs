using ProjectCRUD.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCRUD.Library.Interface
{
    public interface IInformationRepository
    {
        Task<int> Save(InformationEntity entity);
        Task<int> Update(InformationEntity entity);
        Task<int> Delete(InformationEntity entity);
        Task<IEnumerable<InformationEntity>> GetInformations();
    }
}
