using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brio
{
    /// <summary>
    /// Реализует модуль инициализации и удаляемые события.
    /// </summary>
    public class AuthHttpModule : IHttpModule
    {
        /// <summary>
        /// Инициализирует модуль и подготавливает его для обработки запросов. 
        /// </summary>
        /// <param name="context"> Экземпляр System.Web.HttpApplication, обеспечивает доступ к методам, свойствам, 
        /// и событиям, которые являются общими для всех объектов приложения в приложении ASP.NET</param>
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(this.Authenticate);
        }

        /// <summary>
        /// Производит установку текущего пользователя в текущий контекст
        /// </summary>
        /// <remarks>Вызывается при каждом событии аунтефикации.</remarks>
        private void Authenticate(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;

            var auth = DependencyResolver.Current.GetService<IAuthentication>();
            auth.HttpContext = context;
            context.User = auth.CurrentUser;
        }

        public void Dispose() { }
    }
}