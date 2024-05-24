# EventHubExplorer

EventHubExplorer is a tool designed for interacting with and exploring Azure Event Hub. It provides a user-friendly interface to monitor and manage Event Hub entities and messages.

## Features

- **Connect to Azure Event Hub**: Easily connect to your Azure Event Hub using connection strings.
- **Monitor Messages**: Monitor messages being sent to and received from Event Hubs.

  ![image](https://github.com/can-acar/EventHubExplorer/assets/644199/067162f5-6e4a-4b3f-8d0e-7b3e0f922cf8)


## Installation

To install EventHubExplorer, clone the repository and build the project using the following commands:

```bash
git clone https://github.com/can-acar/EventHubExplorer.git
cd EventHubExplorer
# Build the project (commands depend on the project setup, e.g., using .NET CLI)
dotnet build

## Usage
Event Hub Connection String : start with Endpoint:<addresss>
Event Hub Name: name
Consumer Groupd : $Default /  your counsermer group
Event Model:  Event Data Entity Model.
JSON:
{
 [Key]:value
}

