using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Threading;

namespace AdditionalTasks
{
    public class ByteArraySave
    {
        private delegate void SaveComplete(byte[] array,string filename, AsyncOperation asyncOperation);

        public event SaveCompleteEventHandler SaveCompleteEventHandler;

        public HybridDictionary HybridDictionary;

        public SendOrPostCallback OnCompletedCallback;

        public ByteArraySave()
        {
            HybridDictionary = new HybridDictionary();
            OnCompletedCallback = new SendOrPostCallback(WriteCompleted);
        }

        public void WriteCompleted(object operationState)
        {
            SaveCompleteEventArgs e = operationState as SaveCompleteEventArgs;

            SaveCompleteEventHandler?.Invoke(this,e);
        }

        private void Completion(string result, Exception ex, bool canceled, AsyncOperation asyncOperation)
        {
            if (!canceled)
            {
                lock (HybridDictionary.SyncRoot)
                {
                    HybridDictionary.Remove(asyncOperation.UserSuppliedState);
                }
            }

            SaveCompleteEventArgs e = new SaveCompleteEventArgs(result,ex,canceled,asyncOperation.UserSuppliedState);

            asyncOperation.PostOperationCompleted(OnCompletedCallback,e);
        }

        private bool TaskCancelled(object taskId)
        {
            return(HybridDictionary[taskId] == null);
        }

        private void SaveWorker(byte[] data,string filename, AsyncOperation asyncOperation)
        {
            Exception e = null;
            string result = null;

            if (!TaskCancelled(asyncOperation.UserSuppliedState))
            {
                try
                {
                    result = SaveFile(data, filename);
                }
                catch (Exception ex)
                {
                    e = ex;
                }
            }

            this.Completion(result,e,TaskCancelled(asyncOperation.UserSuppliedState),asyncOperation);
        }

        private string SaveFile(byte[] data, string filename)
        {
            try
            {
                File.WriteAllBytes(filename, data);
                return filename;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public virtual void SaveFileAsync(byte[] data, string filename, object taskId)
        {
            AsyncOperation asyncOperation = 
                AsyncOperationManager.CreateOperation(taskId);

            lock (HybridDictionary.SyncRoot)
            {
                if (HybridDictionary.Contains(taskId))
                {
                    throw new ArgumentException("TaskID parameter must be unique","taskID");
                }
            }

            HybridDictionary[taskId] = asyncOperation;

            SaveComplete workerDelegate = new SaveComplete(SaveWorker);
            workerDelegate.BeginInvoke(
                data,
                filename,
                asyncOperation,
                null,
                null);
        }

        public void CancelAsync(object taskId)
        {
            if (HybridDictionary[taskId] is AsyncOperation asyncOperation)
            {
                lock (HybridDictionary.SyncRoot)
                {
                    HybridDictionary.Remove(taskId);
                }
            }
        }
    }


    class III
    {
        private static AutoResetEvent wh = new AutoResetEvent(false);

        private readonly ByteArraySave _byteArraySave = new ByteArraySave();

        public III()
        {
            _byteArraySave.SaveCompleteEventHandler += new SaveCompleteEventHandler(SaveFile_Completed);

            StartAsync();

            wh.WaitOne();
        }

        private void StartAsync()
        {
            string filename = "Saved.pdf";
            //Pobranie przykladowego pliku który został umieszczony w plikach projektu.
            var data = File.ReadAllBytes("pocorgtfo00.pdf");

            Guid taskId = Guid.NewGuid();
            
            this._byteArraySave.SaveFileAsync(data,filename,taskId);
        }

        private static void SaveFile_Completed(
            object sender, SaveCompleteEventArgs e)
        {
            Guid taskid = (Guid) e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("Operation Cancelled");
            }
            else if (e.Error != null)
            {
                Console.WriteLine("Error!");
            }
            else
            {
                Console.WriteLine($"Task: {taskid} saved to {e.Filename}");
            }

            wh.Set();
        }

    }
}
