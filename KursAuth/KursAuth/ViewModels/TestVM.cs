using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace KursAuth.ViewModels
{
    [DataContract]
    public class TestVM : ViewModelBase, IRoutableViewModel
    {
        public string UrlPathSegment => "/test";

        public IScreen HostScreen { get; }
    }
}
