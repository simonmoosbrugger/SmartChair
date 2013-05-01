using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using AutoPopupMessage.BusinessObjects;
using AutoPopupMessage.model;

namespace AutoPopupMessage.controller
{
    public class NotifyMessageController
    {
        private readonly object syncRoot = new object();
        protected int MaxPopup { get; set; }
        protected List<AnimatedLocation> DisplayLocations { get; set; }
        protected ConcurrentQueue<NotifyMessage> QueuedMessages { get; set; }
        protected NotifyMessageViewModel[] DisplayMessages { get; set; }
        private CancellationTokenSource cts;
        
        
        private bool isStarted;
        public bool IsStarted
        {
            get { return isStarted; }
            set { isStarted = value; }
        }


        private delegate void MethodInvoker();

        public NotifyMessageController(double screenWidth, double screenHeight, double popupWidth, double popupHeight)
        {
            MaxPopup = Convert.ToInt32(screenHeight / popupHeight) - 1;
            DisplayLocations = new List<AnimatedLocation>(MaxPopup);
            DisplayMessages = new NotifyMessageViewModel[MaxPopup];
            QueuedMessages = new ConcurrentQueue<NotifyMessage>();

            double left = screenWidth - popupWidth;
            double top = screenHeight;

            for (int i = 0; i < MaxPopup; i++)
            {
                if (i == 0)
                {
                    DisplayLocations.Add(new AnimatedLocation(left, left, screenHeight, top - popupHeight));
                }
                else
                {
                    var previousLocation = DisplayLocations[i - 1];
                    DisplayLocations.Add(new AnimatedLocation(left, left, previousLocation.ToTop, previousLocation.ToTop - popupHeight));
                }
            }
            this.isStarted = false;
        }

        public void Start()
        {
            lock (syncRoot)
            {
                if (!isStarted)
                {
                    cts = new CancellationTokenSource();
                    StartService(cts.Token);
                    isStarted = true;
                }
            }
        }

        private Task StartService(CancellationToken cancellationToken)
        {
            var dispatcher = Application.Current.MainWindow.Dispatcher;

            return Task.Factory.StartNew(() =>
            {
                do
                {
                    int nextlocation = FindNextLocation();

                    if (nextlocation > -1)
                    {
                        NotifyMessage msg = null;
                        if (QueuedMessages.TryDequeue(out msg))
                        {
                            var viewModel = new NotifyMessageViewModel(msg, DisplayLocations[nextlocation], () => DisplayMessages[nextlocation] = null);
                            DisplayMessages[nextlocation] = viewModel;

                            dispatcher.BeginInvoke(
                                new MethodInvoker(() =>
                                {
                                    var window = new NotifyMessageWindow()
                                    {
                                        Owner = Application.Current.MainWindow,
                                        DataContext = viewModel,
                                        ShowInTaskbar = false
                                        

                                    };
                                    window.Show();
                                }), DispatcherPriority.Background);
                        }
                    }
                    Thread.Sleep(1000);
                } while (QueuedMessages.Count > 0 && !cancellationToken.IsCancellationRequested);
                Stop();

            });
        }

        private int FindNextLocation()
        {
            for (int i = 0; i < DisplayMessages.Length; i++)
            {
                if (DisplayMessages[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Stop()
        {
            lock (syncRoot)
            {
                if (isStarted)
                {
                    StopService();
                    isStarted = false;
                }
            }
        }

        public void StopService()
        {
            cts.Cancel();
            cts.Dispose();
        }

        public void EnqueueMessage(NotifyMessage msg)
        {
            QueuedMessages.Enqueue(msg);
            Start();
        }
    }
}
