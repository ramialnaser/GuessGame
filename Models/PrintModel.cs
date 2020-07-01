using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace GuessGame.Models
{
    public class PrintModel
    {
        public static string Title(string text, int size)
        {
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "container w-75 p-3 text-center");
                writer.RenderBeginTag(HtmlTextWriterTag.Div); // Begin #1
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "h"+size+ " m-2 shadow bg-transparent rounded");
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-family: Merriweather, serif; color: cadetblue; padding: 10px");
                writer.RenderBeginTag(HtmlTextWriterTag.H1); // Begin #2
                writer.Write(text);
                writer.RenderEndTag(); // End #2
                writer.RenderEndTag(); // End #1
            }
            return stringWriter.ToString();           
        }
        
        public static string SubTitle(string text, int size)
        {
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "container p-3");
                writer.RenderBeginTag(HtmlTextWriterTag.Div); // Begin #1
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "h"+size+ " m-2 shadow bg-transparent rounded");
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "font-family: Merriweather, serif; color: cadetblue; padding: 10px");
                writer.RenderBeginTag(HtmlTextWriterTag.H1); // Begin #2
                writer.Write(text);
                writer.RenderEndTag(); // End #2
                writer.RenderEndTag(); // End #1
            }
            return stringWriter.ToString();           
        }


        public static string Paragraph(string text)
        {
            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "text-justify");
                writer.RenderBeginTag(HtmlTextWriterTag.P); // Begin #1               
                writer.Write(text);
                writer.RenderEndTag(); // End #1
            }
            return stringWriter.ToString();
        }

        public static string[] GetAvatars()
        {
            var avatar = Directory.GetFiles("wwwroot/img/avatar");
            for (int i = 0; i < avatar.Length; i++)
            {
                avatar[i] = "/img/avatar/"+avatar[i].Substring(avatar[i].LastIndexOf("\\")+1);
            }

            return avatar;
        }


        public static string UserForm (UserModel userModel)
        {
            var form = $"<div class=\"container\">";
            form += $"<input class=\"tableCell_input\" name=\"userid\" value=\"{userModel.UserId}\" readonly>";
            form += $"<input class=\"tableCell_input\" name=\"username\" value=\"{userModel.Username}\" >";
            form += $"<input class=\"tableCell_input\" name=\"email\" value=\"{userModel.Email}\" >";
            form += $"<input class=\"tableCell_input\" name=\"password\" value=\"{userModel.Password}\" >";
            
            form += $"<input class=\"tableCell_input\" name=\"role\" value=\"{userModel.Role}\" >";
            form += $"<img src=\"{userModel.Avatar}\" width=64 height=64 class=\"tableCell_input\" name=\"avatar\">";
            form += $"<button class=\"tableCell_input\" onclick=\"DeleteUser({userModel.UserId});\">Delete</button>";
            
            form += "</div>";
            return form;
        }
        
        public static string GameForm (GameModel gameModel)
        {
            var form = $"<div class=\"container\">";
            form += $"<input class=\"tableCell_input\" name=\"userid\" value=\"{gameModel.Gameid}\" readonly>";
            form += $"<input class=\"tableCell_input\" name=\"username\" value=\"{gameModel.Userid}\" >";
            form += $"<input class=\"tableCell_input\" name=\"email\" value=\"{gameModel.DrawText}\" >";
            form += $"<input class=\"tableCell_input\" name=\"password\" value=\"{gameModel.Active}\" >";           
            form += $"<img src=\"{gameModel.Img}\" width=64 height=64 class=\"tableCell_input\" name=\"avatar\">";
            form += $"<input class=\"tableCell_input\" name=\"password\" value=\"{gameModel.Active}\" >";
            form += $"<button class=\"tableCell_input\" onclick=\"DeleteGame({gameModel.Gameid});\">Delete</button>";
            
            form += "</div>";
            return form;
        }
    }
}
