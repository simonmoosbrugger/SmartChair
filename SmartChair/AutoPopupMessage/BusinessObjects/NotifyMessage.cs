using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace AutoPopupMessage.BusinessObjects
{
    public class NotifyMessage
    {
        private readonly string skinName;

        public string SkinName
        {
            get { return skinName; }
        }

        private readonly string headerText;

        public string HeaderText
        {
            get { return headerText; }
        }

        private readonly string bodyText;

        public string BodyText
        {
            get { return bodyText; }
        }

        private readonly Action clickAction;

        public Action ClickAction
        {
            get { return clickAction; }
        }
        public NotifyMessage(string skinName, string headerText, string bodyText, Action clickAction)
        {
            this.skinName = skinName;
            this.headerText = headerText;
            this.bodyText = bodyText;
            this.clickAction = clickAction;
        }

    }
}
