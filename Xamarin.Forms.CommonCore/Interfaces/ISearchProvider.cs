using System;
using System.Windows.Input;

namespace Xamarin.Forms.CommonCore
{
	public interface ISearchProvider
	{
		ICommand SearchCommand { get; }
		bool SearchIsDefaultAction { get; }
		string QueryHint { get; }
	}
}
