using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace XMart.ViewModels
{
    public class Command : ICommand
    {
        private Action methodToExecute = null;
        private Func<bool> methodCanExecute = null;

        public Command(Action methodToExecute, Func<bool> methodCanExecute)
        {
            this.methodToExecute = methodToExecute;
            this.methodCanExecute = methodCanExecute;
        }

        public void Execute(object parameter)
        {
            this.methodToExecute();
        }
        public bool CanExecute(object parameter)
        {
            if (this.methodCanExecute == null)
            {
                return true;
            }
            else
            {
                return this.methodCanExecute();
            }
        }

        public event EventHandler CanExecuteChanged;
        public void RaseCanExecuteChangedEvent()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}