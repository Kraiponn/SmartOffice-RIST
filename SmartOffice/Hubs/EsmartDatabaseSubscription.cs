using System;
using TableDependency.Enums;
using TableDependency.EventArgs;
using TableDependency.SqlClient;
using Microsoft.AspNetCore.SignalR;
using SmartOffice.IResponsitory;
using SmartOffice.Models;
using SmartOffice.ModelsDocControl;

namespace SmartOffice.Hubs
{
    public class EsmartDatabaseSubscription : IDatabaseSubscription
    {
        private  bool disposedValue = false;
        private readonly IESmartNotiRepository _repository;
        private readonly IHubContext<NotiHub> _hubContext;
        private  SqlTableDependency<DocItem> _tableDependency;
        public EsmartDatabaseSubscription(IESmartNotiRepository repository, IHubContext<NotiHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public void Configure(string connectionString)
        {
            _tableDependency = new SqlTableDependency<DocItem>(connectionString, null, null, null, null, DmlTriggerType.All);
            _tableDependency.OnChanged += Changed;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();

            //Console.WriteLine("Waiting for receiving notifications...");
        }

        private void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            //Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
        }

        private void Changed(object sender, RecordChangedEventArgs<DocItem> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                // TODO: manage the changed entity
                var changedEntity = e.Entity;
               
                // var return_data = new { data = e.Entity};
                _repository.Boardcastnotify(e.Entity.DocumentNo,e.Entity.UserName);
                //_hubContext.Clients.All.SendAsync("ShowMessage", return_data);
            }
        }

        #region IDisposable

        ~EsmartDatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
