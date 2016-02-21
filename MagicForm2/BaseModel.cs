using System;
using PropertyChanged;
using System.ComponentModel;

namespace MagicForm {
	[ImplementPropertyChanged]
	public class BaseModel : INotifyPropertyChanged {
		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}
