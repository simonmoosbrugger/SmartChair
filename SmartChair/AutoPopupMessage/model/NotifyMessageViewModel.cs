using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AutoPopupMessage.BusinessObjects;
using AutoPopupMessage.command;

namespace AutoPopupMessage.model
{
    public class NotifyMessageViewModel
    {
        private readonly NotifyMessage content;

        public NotifyMessage Content
        {
            get { return content; }
        }

        private readonly AnimatedLocation location;

        public AnimatedLocation Location
        {
            get { return location; }
        }

        private readonly Action closedAction;

        public Action ClosedAction
        {
            get { return closedAction; }
        }

        public NotifyMessageViewModel(NotifyMessage content, AnimatedLocation location, Action closedAction)
        {
            this.content = content;
            this.location = location;
            this.closedAction = closedAction;
        }

        private ICommand clickCommand;

        public ICommand ClickCommand
        {
            get { return (clickCommand ?? (clickCommand = new DelegateCommand((_) => content.ClickAction()))); }
           
        }
        private ICommand closeCommand;

        public ICommand CloseCommand
        {
            get { return (closeCommand ?? (closeCommand = new DelegateCommand((_) => closedAction()))); }
          
        }
    }
}
