# MStatistics
A simple .NET 6 web api that uses a twitter sample stream  a twitter sample stream to store streamed tweets into an in memory database, and returns some basic statistics.

Note: this is a prototype using an in-memory database. In a real Production environment scaling would be needed. A VMSS would be a recommended start for this.

## How to run
1. Setup your personal twitter application to create your Twitter API Key, Secret and Bearer Token https://developer.twitter.com/en/portal/dashboard
2. Populate the TwitterServiceBearerToken value from MStatistics\appsettings.json with your Bearer Token
2. Run the application
