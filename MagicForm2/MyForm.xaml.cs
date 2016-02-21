using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.ComponentModel;
using PropertyChanged;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace MagicForm2 {

	[ImplementPropertyChanged]
	public partial class MyForm : ContentPage, INotifyPropertyChanged {

//		object _model;
//		public object model { 
//			get {
//				return _model;
//			} 
//			set {
//				_model = value;
//				OnPropertyChanged("model");
//				OnPropertyChanged("Model");
//			} 
//		}
//		public Person Model { 
//			get { 
//				return model as Person;
//			}
//			set {
//				model = value;
//			}
//		}

		public Person Model { get; set; }

		FormMagic formMagic;

		public MyForm() {
			InitializeComponent();
			BindingContext = this;

			var p = new Person() {
				id = 23,
				FirstName = "Fred",
				LastName = "Bloggs",
				Height = 178,
				Weight = 82,
				Age = 35
			};
			Model = p;
			formMagic = new FormMagic(p);
		}

		public void Dump(object sender, EventArgs e) {
			Debug.WriteLine(formMagic.SerializeModel());
		}
	}
}

