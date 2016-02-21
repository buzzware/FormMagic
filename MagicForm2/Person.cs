using System;
using PropertyChanged;
using System.ComponentModel;

namespace MagicForm {

	public class Person : BaseModel {
		[RequiredAttribute]
		public long id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Height { get; set; }
		public double Weight { get; set; }
		public int Age { get; set; }
	}
}

