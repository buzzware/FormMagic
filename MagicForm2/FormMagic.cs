using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MagicForm {

	[AttributeUsage(AttributeTargets.Property)]
	public class RequiredAttribute : Attribute {
	}

	public class SomePropertiesContractResolver : DefaultContractResolver {
		IEnumerable<string> properties;

     public SomePropertiesContractResolver(IEnumerable<string> aProperties)
     {
         properties = aProperties;
     }
 
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) {
        IList<JsonProperty> plist = base.CreateProperties(type, memberSerialization);
        // only serializer properties that start with the specified character
        plist = plist.Where(p => properties.Contains(p.PropertyName)).ToList();  //.Contailsp.PropertyName   .StartsWith(_startingWithChar.ToString())).ToList();
        return plist;
    }
	}

	public class FormMagic {

		public List<string> PropertiesChanged = new List<string>();

		INotifyPropertyChanged model;

		public object Model {
			get {
				return model;
			}
			set {
				if (model!=null)
					model.PropertyChanged -= modelPropertyChanged;
				model = value as INotifyPropertyChanged; 
				if (model!=null)
					model.PropertyChanged += modelPropertyChanged;
			}
		}

		public FormMagic(INotifyPropertyChanged aModel) {
			//Dynamitey.Dynamic.GetMemberNames(aModel);
			Model = aModel;
		}

		public void modelPropertyChanged(object sender, PropertyChangedEventArgs e) {
			Debug.WriteLine("modelPropertyChanged "+e.PropertyName);
			if (PropertiesChanged.Contains(e.PropertyName))
				return;
			PropertiesChanged.Add(e.PropertyName);
			Debug.WriteLine("modelPropertyChanged "+e.PropertyName);
		}

//		public List<string> ModelProperties {
//			get {
//
//			}
//		}

		public List<string>PropertiesRequired {
			get {
				var result = new List<string>();
				Type objtype = model.GetType();
				foreach (PropertyInfo p in objtype.GetRuntimeProperties()) {
	        foreach (Attribute a in p.GetCustomAttributes(false)){
	        	if (a.GetType()==typeof(RequiredAttribute) && !result.Contains(p.Name))
	        		result.Add(p.Name);
				  }
				}
				result.Sort();
				return result;
			}
		}

		public List<string>PropertiesToSerialize {
			get {
				var result = PropertiesChanged.ToList();
				result.AddRange(PropertiesRequired);
				result.Sort();
				return result;
			}
		}

		public string SerializeModel() {
			string output = JsonConvert.SerializeObject(model,Formatting.None,new JsonSerializerSettings { ContractResolver = new SomePropertiesContractResolver(PropertiesToSerialize)});
			return output;
		}
	}
}

