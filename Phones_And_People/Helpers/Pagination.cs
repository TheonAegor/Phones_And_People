using Phones_And_People.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Phones_And_People.Helpers
{
    public static class Pagination
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder prev = CreateTagA(pageInfo.Page - 1, pageUrl, 0, "<<");
            TagBuilder first = CreateTagA(1, pageUrl);
            TagBuilder thisPage = CreateTagA(pageInfo.Page, pageUrl, 1);
            TagBuilder last = CreateTagA(pageInfo.TotalPages, pageUrl);
            TagBuilder next = CreateTagA(pageInfo.Page + 1, pageUrl, 0, ">>");

            if (pageInfo.Page != 1)
            {
                result.Append(prev.ToString());
                result.Append(first.ToString());
            }
            result.Append(thisPage.ToString());
            if (pageInfo.Page != pageInfo.TotalPages && pageInfo.TotalPages > 1)
            {
                result.Append(last.ToString());
                result.Append(next.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        private static TagBuilder CreateTagA(int page, Func<int, string> pageUrl, int flagBtnPrime = 0, string symb = "" )
        {
            TagBuilder tag = new TagBuilder("a");

            tag.MergeAttribute("href", pageUrl(page));
            if (symb == "")
            {
                tag.InnerHtml = page.ToString();
            }
            else if (symb == "<<")
            {
                tag.InnerHtml = "<<";
            }
            else
            {
                tag.InnerHtml = ">>";
            }

            if (flagBtnPrime == 0)
                tag.AddCssClass("btn btn-default");
            else
                tag.AddCssClass("btn btn-primary");
            return (tag);
        }

    }
}