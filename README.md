# Exchange Rates Service
Exchange rates service is a simple free REST API service extension of api.exchangerate.host and api.exchangeratesapi.io APIs.
This service exposes a pair of endpoints for each service extension. 
Responses are similar but not exact due to API restrictions:
1. Api.exchangeratesapi.io timeseries is not a free service so HistoricalRates was used - a date per request with fixed base currency so relative rates had to be calculated aterwards.
2. Api.exchangerate.host timeseries is a free service so it was used as alternative althoug this API was not a Take-Home Assignement requirement.

## Testing instructions:

1. Download and open solution in Visual Studio 2019.
2. Build the solution and run the project.
3. Use in browser with links: `https://localhost:5001/swagger/index.html` for Kestrel or `https://localhost:44385/swagger/index.html` for IIS Express.
