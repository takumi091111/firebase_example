using System.Windows.Data;
using System.Collections.ObjectModel;

namespace ChatApp
{
    public class ViewModel
    {
        public ObservableCollection<Message> MessageList { get; set; }

        public ViewModel()
        {
            MessageList = new ObservableCollection<Message>();
            // 複数スレッドからコレクション操作できるようにする
            BindingOperations.EnableCollectionSynchronization(MessageList, new object());
        }
    }
}
