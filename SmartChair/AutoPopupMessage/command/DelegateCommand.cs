using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AutoPopupMessage.command
{
    /// <summary>
    /// A command that executes delegates to determine whether the command can execute, and to execute the command.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        public DelegateCommand(Action<object> execute) 
            : this(execute, null)
        {}

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object paramter)
        {
            if (canExecute == null)
            {
                return true;
            }

            return this.canExecute(paramter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
