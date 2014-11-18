using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioStroy
{
    /// <summary>
    /// Предоставляет методы, которые упрощают размещение службы и разрешение зависимостей.
    /// </summary>
    public class CustomDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// Супер-завод, который может создавать объекты всех видов, после привязки, предусмотренных в Ninject.Planning.Bindings.IBindings.
        /// </summary>
        private readonly IKernel _kernel;

        /// <summary>
        /// Инизиализирует экземпляр CustomDependencyResolver
        /// </summary>
        /// <param name="kernel">Ядро</param>
        public CustomDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Возвращает отдельно зарегистрированные службы, обеспечивающие поддержку создания произвольного объекта.
        /// </summary>
        /// <param name="serviceType">Тип запрашиваемой службы или объекта</param>
        /// <returns>Запрашиваемая служба или объект</returns>
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        /// <summary>
        /// Возвращает многократно зарегистрированные службы.
        /// </summary>
        /// <param name="serviceType">Тип запрашиваемых служб</param>
        /// <returns>Запрашиваемые службы.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType).ToList();
        }
    }
}