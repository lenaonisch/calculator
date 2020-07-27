# calculator

First I planned to implement history as a log, so configured Serilog to write operations and arguments into MS SQL database automatically.
But then I reqlized that this tool may be needed to save some other information and it won't be possible to keep it clear. So the history is stored in separate dedicated table.
I decided to left both implemetations*

P.S. It was a "transparent" way to show I know about Serilog and am able to play with it :)
