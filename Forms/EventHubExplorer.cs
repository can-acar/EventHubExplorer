using System.Dynamic;
using System.Text.Json;
using EventHubExplorer.Helpers;

namespace EventHubExplorer.Forms
{
    public partial class EventHubExplorer:Form
    {
        //private EventHubTestProcessor _processor;
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private CancellationToken _token;

        private readonly ReceiveEventsFromEventHub _processor;

        public EventHubExplorer()
        {
            InitializeComponent();
            var bus = new EventBus();
            _processor = new ReceiveEventsFromEventHub();
            bus.Register<ExpandoObject>(OnProcessed);

            tblStatus.Text = @"Disconnected";
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
            var eventHubName = tbxEventHubName.Text;
            var consumerGroupName = tbxConsumerGroup.Text;
            _processor.SetConfig(eventHubConnectionString,eventHubName,consumerGroupName);


            var options = new JsonSerializerOptions { Converters = { new ExpandoObjectConverter() },PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var eventModel = JsonSerializer.Deserialize<ExpandoObject>(tbxEventModel.Text,options);
            foreach(var column in eventModel.ToList())
            {
                dtvLog.Columns.Add(column.Key,column.Key);
            }


            _token = _cancellationTokenSource.Token;
            tblStatus.Text = @"Connecting";
            await _processor.StartProcessingAsync(_token);
            tblStatus.Text = @"Connected";
        }

        private async void btnDisconnect_Click(object sender,EventArgs e)
        {
            await _processor.StopProcessingAsync(_token);
            dtvLog.DataSource = null;
            tblStatus.Text = @"Disconnected";
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
            tbxEventHubName.Text = string.Empty;
            tbxConsumerGroup.Text = string.Empty;
            tbxEventModel.Text = string.Empty;
            tblStatus.Text = @"Disconnected";
        }
    }
}