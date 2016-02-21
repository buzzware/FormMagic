using System;
using PropertyChanged;
using System.ComponentModel;

namespace MagicForm2 {
	[ImplementPropertyChanged]
	public class BaseModel : INotifyPropertyChanged {
		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}
