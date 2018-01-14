using System;
using System.ComponentModel;

namespace AdditionalTasks
{
    public delegate void SaveCompleteEventHandler(object sender, SaveCompleteEventArgs e);

    public class SaveCompleteEventArgs : AsyncCompletedEventArgs
    {
        public string Filename { get; }

        public SaveCompleteEventArgs(string name,
            Exception e, bool cancelled, object state)
            : base(e, cancelled, state)
        {
            Filename = name;
        }
    }
}
