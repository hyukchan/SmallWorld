using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Small_World
{
    public class GameMessages : System.ComponentModel.INotifyPropertyChanged
    {
        private static GameMessages instance;
        private List<String> messages;
        private GameMessages()
        {
            messages = new List<String>();
        }

        public static GameMessages Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameMessages();
                }
                return instance;
            }
        }

        public List<string> Messages
        {
            get
            {
                return messages;
            }
        }

        public void addMessage(String str) {
            messages.Add(str);
            OnPropertyChanged("GameMessages");
        }

        public string getLastMessage()
        {
            return messages[messages.Count() - 1];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
