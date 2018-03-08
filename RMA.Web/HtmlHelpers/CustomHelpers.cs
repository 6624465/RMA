using EZY.RMAS.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RMA.Web.HtmlHelpers
{
    public static class CustomHelpers
    {
        public static bool checkHasRight(string roleRightKey)
        {
            var securables = (List<Securables>)HttpContext.Current.Session[UTILITY.SSN_USER_SECURABLES];

            var securableItem = securables
                                    .Where(x => x.SecurableItem == roleRightKey)
                                    .FirstOrDefault();

            if (securableItem != null)
                return true;
            else
                return false;            
        }        

        public static HtmlString SubmitButton(
            this HtmlHelper htmlHelper,
            string buttonText,
            object htmlAttributes,
            string roleRightKey)
        {
            var tt = checkHasRight(roleRightKey);
            string htmlAttributesString = string.Empty;
            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=\"" + d.Values.ElementAt(i) + "\"";
                }
            }

            StringBuilder submitBtn = new StringBuilder();
            submitBtn.Append("<button type=\"submit\" ");
            if (htmlAttributesString != string.Empty)
            {
                submitBtn.Append(htmlAttributesString);
            }
            submitBtn.AppendFormat(">{0}</button>", buttonText);

            return new HtmlString(submitBtn.ToString());
        }

        public static HtmlString Button(
            this HtmlHelper htmlHelper,
            string buttonText,
            object htmlAttributes,
            string roleRightKey)
        {
            StringBuilder button = new StringBuilder();
            var hasRight = checkHasRight(roleRightKey);

            if (hasRight)
            {
                string htmlAttributesString = string.Empty;
                if (htmlAttributes != null)
                {
                    RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                    for (int i = 0; i < d.Keys.Count; i++)
                    {
                        htmlAttributesString += " " + d.Keys.ElementAt(i) + "=\"" + d.Values.ElementAt(i) + "\"";
                    }
                }

                button.Append("<button type=\"button\" ");
                if (htmlAttributesString != string.Empty)
                {
                    button.Append(htmlAttributesString);
                }
                button.AppendFormat(">{0}</button>", buttonText);
            }

            return new HtmlString(button.ToString());
        }

        public static HtmlString RmaActionLink(
            this HtmlHelper htmlHelper,
            string url,
            string anchorText,
            object htmlAttributes,
            string roleRightKey
            )
        {
            string htmlAttributesString = string.Empty;
            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=\"" + d.Values.ElementAt(i) + "\"";
                }
            }

            StringBuilder anchor = new StringBuilder();
            anchor.Append($"<a href=\"{url}\" ");
            if (htmlAttributesString != string.Empty)
            {
                anchor.Append(htmlAttributesString);
            }
            anchor.AppendFormat(">{0}</a>", anchorText);

            return new HtmlString(anchor.ToString());
        }

        public static IDisposable RmaBeginForm(
            this HtmlHelper htmlHelper,
            string url,
            FormMethod method,
            object htmlAttributes,
            string roleRightKey)
        {
            if (roleRightKey == "ORDERENTRY.SAVE")
            {
                string htmlAttributesString = string.Empty;
                if (htmlAttributes != null)
                {
                    RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                    for (int i = 0; i < d.Keys.Count; i++)
                    {
                        htmlAttributesString += " " + d.Keys.ElementAt(i) + "=\"" + d.Values.ElementAt(i) + "\"";
                    }
                }

                var writer = htmlHelper.ViewContext.Writer;
                writer.WriteLine(string.Format($"<form action=\"{url}\" method=\"{method}\" {htmlAttributesString}>"));
                return new FormContainer(writer);
            }
            else
            {
                var writer = htmlHelper.ViewContext.Writer;
                return new FormContainer(writer);
            }
        }

        public class FormContainer : IDisposable
        {
            private readonly TextWriter _writer;

            public FormContainer(TextWriter writer)
            {
                _writer = writer;
            }

            public void Dispose()
            {
                _writer.Write("</form>");
            }
        }

        public class DivContainer : IDisposable
        {
            private readonly TextWriter _writer;

            public DivContainer(TextWriter writer)
            {
                _writer = writer;
            }

            public void Dispose()
            {
                _writer.Write("</div>");
            }
        }
    }
}