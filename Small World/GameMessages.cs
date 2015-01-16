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

        /// <summary>
        /// Constructeur d'un GameMessage
        /// </summary>
        private GameMessages()
        {
            messages = new List<String>();
        }

        /// <summary>
        /// Getter du singleton
        /// </summary>
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

        /// <summary>
        /// Ajoute un message au singleton
        /// </summary>
        /// <param name="str">Le message à ajouter</param>
        public void addMessage(String str) {
            messages.Add(str);
            OnPropertyChanged("GameMessages");
        }

        /// <summary>
        /// Accesseur du dernier message ajouté
        /// </summary>
        /// <returns>Le dernier message ajouté au singleton</returns>
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
