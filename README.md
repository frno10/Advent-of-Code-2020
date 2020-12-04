# Advent-of-Code-2020

## Usage

to run todays AdventOfCode challenge (day is picked up automatically)
```
dotnet run	
```

to run specific AdventOfCode day
```
dotnet run 4	
```

## Logging

All executions are logged locally, so user can review all attempts. Serilog is used for logging.

### Logging verbosity

to change default logging verbosity, specify flag `log:<verbosity level>`. With this approach you need to specify a day explicitly.

```
dotnet run 4 log:debug	
```

Supported logging levels are: verbose, debug, information (default), warning, error, fatal
