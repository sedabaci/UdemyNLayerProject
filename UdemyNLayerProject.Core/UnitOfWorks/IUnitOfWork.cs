using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Repositories;

namespace UdemyNLayerProject.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// nesnelerin DB'ye yansımasını sağlar
        /// Repositorylerimin referanslarını burada vermem, best practice için uygun
        /// </summary>
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        Task CommitAsync();
        void Commit();
    }
}
