﻿using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TIK
{
    public static class HtmlExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            string cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static MvcHtmlString RenderDocumentUnit(this HtmlHelper html, IEnumerable<Document> docs)
        {
            var i = 1;
            StringBuilder sb = new StringBuilder();
            int documentsCount = docs.Count();
            foreach (var item in docs)
            {
                if (i % 4 == 0 || i == 1)
                {
                    sb.Append("<div class='pure-g'>");
                }

                sb.Append(GetDocumentUnitWithoutContainer(html, item, i));

                if (i % 3 == 0 || documentsCount == i)
                {
                    sb.Append("</div>");
                }
                i++;
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        private static string GetDocumentUnitWithoutContainer(HtmlHelper html, Document doc, int index)
        {
            TagBuilder title = new TagBuilder("div");
            title.AddCssClass("title");
            title.SetInnerText(doc.DocumentTitle);

            TagBuilder titleContainer = new TagBuilder("div");
            titleContainer.AddCssClass("title-container");
            titleContainer.InnerHtml = title.ToString();

            TagBuilder adminButtons = new TagBuilder("div");
            adminButtons.AddCssClass("admin-buttons");
            adminButtons.InnerHtml = "<a href='/Document/Edit/" + doc.ID + "'><i class='fa fa-pencil-square-o'></i></a>" +
                                     "<a class='delete' href='/Document/Delete/" + doc.ID + "'><i class='fa fa-times'></i></a>";

            TagBuilder buttonContainer = new TagBuilder("div");
            buttonContainer.AddCssClass("button-container");
            buttonContainer.InnerHtml = "<button class='button-secondary pure-button show'>" + LinkExtensions.ActionLink(html, "ПРОСМОТРЕТЬ ФАЙЛ", "Show", new { id = doc.ID }, new { target = "_blank" }) + "</button>" +
                                        "<button class='button-pink pure-button download'>" + LinkExtensions.ActionLink(html, "СКАЧАТЬ ФАЙЛЫ В PDF", "Download", new { id = doc.ID }) + "</button>";

            TagBuilder article = new TagBuilder("article");
            article.AddCssClass("doc-container");
            article.AddCssClass("relative");
            article.AddCssClass("pure-u-1");
            article.AddCssClass("pure-u-sm-1-3");
            if (index % 3 == 0)
            {
                article.AddCssClass("last");
            }

            TagBuilder docBckg = new TagBuilder("div");
            docBckg.AddCssClass("doc-bckg");
            docBckg.InnerHtml += adminButtons.ToString();
            docBckg.InnerHtml += titleContainer.ToString();
            docBckg.InnerHtml += buttonContainer.ToString();

            article.InnerHtml = docBckg.ToString();

            return article.ToString();
        }

        public static string DisplayNameFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);
            var displayName = metadata.DisplayName;

            return displayName;
        }
    }
}