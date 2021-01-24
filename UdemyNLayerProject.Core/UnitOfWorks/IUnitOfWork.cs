using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Repositories;

namespace UdemyNLayerProject.Core.UnitOfWorks
{
    interface IUnitOfWork
    {
        /// <summary>
        /// Repositorylerimin referanslarını burada vermem, best practice için uygun
        /// </summary>
        IProductRepository Products { get; }
        ICategoryRepository categories { get; }
        Task CommitAsync();
        void Commit();
    }
}
