using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApi
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event handler required for data binding to update on the view when information changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method required for executing the changed event
        /// Updated to use a special C# feature that allows for the property name to be taken from the code rather than hard coded in
        /// </summary>
        /// <param name="name">The name of the property to be updated for data-binding</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private StringBuilder _DisplayText { get; set; } = new StringBuilder("|");

        public String DisplayText
        {
            get
            {
                return _DisplayText.ToString();
            }set
            {
                if (TextIsWaitCursor())
                {
                    _DisplayText = new StringBuilder();
                }
                _DisplayText.Append(value);
                OnPropertyChanged();
            }
        }
        

        /// <summary>
        /// Removes all spaces from the incoming problem statement, etc 1 +2 is 1+2
        /// </summary>
        /// <returns>The problem statement clear of spaces</returns>
        public async Task<String> ClearProblemOfSpaces()
        {
            return await Task.Factory.StartNew(() =>
            {
                var problem = DisplayText;

                var problemCharArray = problem.ToCharArray();
                var cleanProblem = new StringBuilder();
                foreach (var nonSpaces in problemCharArray.Where(x => x != ' ').Where(x=> x != '|'))
                {
                    cleanProblem.Append(nonSpaces);
                }

                return cleanProblem.ToString();

            });
        }

        public void Clear()
        {
            _DisplayText = new StringBuilder("|");
        }

        public void Backup()
        {
            var CurrentCount = _DisplayText.Length;
            _DisplayText = new StringBuilder(_DisplayText.ToString().Substring(0, CurrentCount - 1));
            OnPropertyChanged("DisplayText");
        }

        public bool TextIsWaitCursor()
        {
            return _DisplayText.ToString().Trim() == "|";
        }

        public void SetAnswer(object input)
        {
            _DisplayText = new StringBuilder(input.ToString());

            OnPropertyChanged("DisplayText");
        }


    }
}
