using System.Dynamic;
using System.Text.Json;
using Azure.Messaging.EventHubs.Primitives;
using Azure.Storage.Blobs;
using EventHubExplorer.Helpers;
using static System.Int32;

namespace EventHubExplorer.Forms
{
    public partial class EventHubConsumerExplorer:Form
    {
        private readonly CancellationTokenRegistration _cancellationToken;
        private CancellationToken _token;
        private EventBus _bus;
        private EventHubTestProcessor _processor;
        public EventHubConsumerExplorer()
        {
            InitializeComponent();
            _bus = new EventBus();
            _bus.Register<ExpandoObject>(OnProcessed);
            _cancellationToken = new CancellationTokenRegistration();
            tblStatus.Text = "Disconnected";
            dtvLog.AutoGenerateColumns = true;
        }

        private void OnProcessed(ExpandoObject payload)
        {
            var row = new DataGridViewRow();
            foreach(var item in payload.ToList())
            {
                // row.Cells[item.Key].Value = item.Value;
                if(dtvLog.Columns.Contains(item.Key))
                {
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Value });
                }
            }

            dtvLog.Rows.Add(row);
        }

        private async void btnRun_Click(object sender,EventArgs e)
        {
            var eventHubConnectionString = tbxEventHubConnectionString.Text;
            var consumerConnectionString = tbxConsumerConnectionString.Text;
            var eventHubName = tbxEventHubName.Text;
            var consumerGroupName = tbxConsumerGroup.Text;
            var options = new JsonSerializerOptions { Converters = { new ExpandoObjectConverter() },PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var eventModel = JsonSerializer.Deserialize<ExpandoObject>(tbxEventModel.Text,options);
            foreach(var column in eventModel.ToList())
            {
                dtvLog.Columns.Add(column.Key,column.Key);
            }

            TryParse(tbxBatchSize.Text,out var batchSize);
            const string blobContainerName = "eventhubs-checkpoints";
            var storageClient = new BlobContainerClient(consumerConnectionString,blobContainerName);
            var checkpointStore = new BlobCheckpointStore(storageClient);

            _processor = new EventHubTestProcessor(checkpointStore, batchSize, 300, consumerGroupName, eventHubConnectionString, eventHubName);
            _token = _cancellationToken.Token;

            tblStatus.Text = "Connecting";
            await _processor.StartProcessingAsync(_token);
            tblStatus.Text = "Connected";
        }

        private async void btnDisconnect_Click(object sender,EventArgs e)
        {
            await _processor.StopProcessingAsync(_token);
            dtvLog.DataSource = null;
            tblStatus.Text = "Disconnected";
            dtvLog.Rows.Clear();
            dtvLog.Columns.Clear();
            dtvLog.Update();
            dtvLog.Refresh();

            dtvLog.DataBindings.Clear();
        }

        private async void btnClear_Click(object sender,EventArgs e)
        {
            await _processor.StopProcessingAsync(_token);
            tbxEventHubConnectionString.Text = string.Empty;
            tbxConsumerConnectionString.Text = string.Empty;
            tbxEventHubName.Text = string.Empty;
            tbxConsumerGroup.Text = string.Empty;
            tbxEventModel.Text = string.Empty;
            tbxBatchSize.Text = string.Empty;
            tblStatus.Text = "Disconnected";
        }
    }
}
