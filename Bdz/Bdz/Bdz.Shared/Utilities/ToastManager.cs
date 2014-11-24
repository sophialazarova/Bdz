using System;
using System.Collections.Generic;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Bdz.Utilities
{
   public class ToastManager
    {
       private ToastTemplateType toastTemplate;
       private XmlDocument toastTemplateXml;

       public ToastManager()
       {
           this.toastTemplate = ToastTemplateType.ToastText02;
           this.toastTemplateXml = ToastNotificationManager.GetTemplateContent(this.toastTemplate);
       }

       public void SendToast(string heading, string content)
       {
           var toast = this.FillToast(heading, content);
           toast.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(3600);
           ToastNotificationManager.CreateToastNotifier().Show(toast);
       }

       private ToastNotification FillToast(string heading, string content)
       {
           XmlNodeList toastTextElements = this.toastTemplateXml.GetElementsByTagName("text");
           toastTextElements[0].AppendChild(this.toastTemplateXml.CreateTextNode(heading));
           toastTextElements[1].AppendChild(this.toastTemplateXml.CreateTextNode(content));

           IXmlNode toastNode = this.toastTemplateXml.SelectSingleNode("/toast");
           ((XmlElement)toastNode).SetAttribute("duration", "long");

           ToastNotification toast = new ToastNotification(this.toastTemplateXml);
           return toast;
       }
    }
}
